using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AccordancePanel : UIPanel {
    public event Action<EquationConfig, bool> EquationVerificatedChanged;

    [SerializeField] private RectTransform _compositionParent;
    [SerializeField] private RectTransform _resultParent;

    private UICompanentsFactory _factory;
    private SwipeHandler _swipeHandler;
    private LineSpawner _lineSpawner;

    private List<MultipliersCompositionView> _compositionViews;
    private List<MultipliersResultView> _resultViews;

    private List<EquationConfig> _equations;
    private bool _hideAfterSelection;
    private Line _currentLine;

    private MultipliersCompositionView _compositionView;
    private MultipliersResultView _resultView;

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory, SwipeHandler swipeHandler, LineSpawner lineSpawner) {
        _factory = companentsFactory;
        _swipeHandler = swipeHandler;
        _lineSpawner = lineSpawner;
    }

    public void Init(EquationConfigs equations, bool hideAfterSelection = true) {
        _equations = equations.Configs;
        _hideAfterSelection = hideAfterSelection;

        CreateCompositionViews();
        CreateResultViews();

        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

    }

    public override void Reset() {
        base.Reset();

        foreach (var iView in _compositionViews) {
            DestroyLine(iView.ConnectPointPosition);
            Destroy(iView.gameObject);
        }
        _compositionViews.Clear();

        foreach (var iView in _resultViews) {
            Destroy(iView.gameObject);
        }
        _resultViews.Clear();

        _lineSpawner.Reset();
        _equations = null;
    }
    
    private void CreateCompositionViews() {
        _compositionViews = new List<MultipliersCompositionView>();

        foreach (var iData in _equations) {
            MultipliersCompositionViewConfig config = new MultipliersCompositionViewConfig(iData);

            MultipliersCompositionView newView = _factory.Get<MultipliersCompositionView>(config, _compositionParent);
            newView.Init(config);
            newView.CompositionViewSelected += OnCompositionViewSelected;

            _compositionViews.Add(newView);
        }
    }

    private void CreateResultViews() {
        var shuffledEquation = Shuffle(_equations);
        _resultViews = new List<MultipliersResultView>();

        foreach (var iData in shuffledEquation) {
            MultipliersResultConfig config = new MultipliersResultConfig(iData.Name);

            MultipliersResultView newView = _factory.Get<MultipliersResultView>(config, _resultParent);
            newView.Init(config);
            newView.ResultViewSelcted += OnResultViewSelected;

            _resultViews.Add(newView);
        }
    }

    private List<EquationConfig> Shuffle(List<EquationConfig> list) {
        EquationConfig[] array = list.ToArray();

        for (int i = array.Length - 1; i > 0; i--) {
            int rnd = UnityEngine.Random.Range(0, i);
            EquationConfig temp = array[i];

            array[i] = array[rnd];
            array[rnd] = temp;
        }

        return array.ToList();
    }
    
    private void OnCompositionViewSelected(MultipliersCompositionView view) {
        _compositionView = view;
        _compositionView.SetState(AccordanceCompanentState.Select);

        _lineSpawner.SpawnLine(view.ConnectPointPosition, out Line line);
        _currentLine = line;
    }

    private void OnResultViewSelected(MultipliersResultView view) {
        if (view == null && _compositionView == null)
            return;

        if (view == null && _compositionView != null) {
            _compositionView.SetState(AccordanceCompanentState.Unselect);
            _compositionView = null;

            DestroyCurrentLine();
        } else {
            _resultView = view;
            view.SetState(AccordanceCompanentState.Select);

            _currentLine.EndLine(_resultView.ConnectPointPosition);

            EquationVerification();
        } 
    }

    private void DestroyCurrentLine() {
        Line line = _lineSpawner.GetLineByStartPoint(_currentLine.StartPoint);
        _lineSpawner.RemoveLine(line);

        Destroy(line.gameObject);
    }

    private void DestroyLine(Vector3 startPoint) {
        Line line = _lineSpawner.GetLineByStartPoint(startPoint);

        if (line != null) {
            _lineSpawner.RemoveLine(line);
            Destroy(line.gameObject);
        }
    }

    private void EquationVerification() {
        if (_compositionView == null || _resultView == null)
            return;

        var description = _compositionView.Description;
        var name = _resultView.Name;

        var data = _equations.FirstOrDefault(data => data.Description == description && data.Name == name);

        data.Result = (name == data.Name);

        ShowEquationVerificationResult(data.Result);
        EquationVerificatedChanged?.Invoke(data, data.Result);
    }

    private void ShowEquationVerificationResult(bool result) {
        if (result) {
            _compositionView.SetState(AccordanceCompanentState.TrueVerification);
            _resultView.SetState(AccordanceCompanentState.TrueVerification);
            _currentLine.SetState(AccordanceCompanentState.TrueVerification);
        }
        else 
        {
            _compositionView.SetState(AccordanceCompanentState.FalseVerification);
            _resultView.SetState(AccordanceCompanentState.FalseVerification);
            _currentLine.SetState(AccordanceCompanentState.FalseVerification);
        }

        _compositionView = null;
        _resultView = null;
        _currentLine = null;
    }
}
