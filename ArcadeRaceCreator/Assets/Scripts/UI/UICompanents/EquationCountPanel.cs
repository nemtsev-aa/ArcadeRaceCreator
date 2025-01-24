using TMPro;
using UnityEngine;

public class EquationCountPanel : Bar {
    [SerializeField] private TextMeshProUGUI _countValue;
    private CodingMiniGameDialog _codingMiniGameDialog;
    
    private int _maxValue;
    private int _currentValue = 0;

    public void Init(CodingMiniGameDialog codingGameDialog, int equationCount) {
        _maxValue = equationCount;
        _codingMiniGameDialog = codingGameDialog;
        
        OnValueChanged(_maxValue, _maxValue);

        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _codingMiniGameDialog.EquationsCountChanged += OnValueChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _codingMiniGameDialog.EquationsCountChanged -= OnValueChanged;
    }

    public override void Reset() {
        base.Reset();
        
        _maxValue = _currentValue = 0;
    }

    protected override void OnValueChanged(float currentValue, float maxValue) {

        if (currentValue == 0)
            _currentValue = _maxValue;
        else
            _currentValue = _maxValue - (int)currentValue;

        _countValue.text = $"{_currentValue}/{_maxValue}";
        Filler.fillAmount = (float)_currentValue / _maxValue;
    }
}
