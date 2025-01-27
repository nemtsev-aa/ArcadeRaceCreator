using System;
using System.Collections.Generic;
using Zenject;

public class CodingMiniGameDialog : Dialog {
    private const float DelaySwitchingEquation = 0.5f;

    public static event Action ApplyClicked;
    public event Action<float, float> EquationsCountChanged;

    private EquationConfigs _configs;
    private AccordancePanel _accordancePanel;
    private EquationCountPanel _equationCountPanel;

    private List<EquationConfig> Equations = new List<EquationConfig>();
    private List<EquationConfig> PassedEquation = new List<EquationConfig>();

    private float _maxEquationCount;

    public CodingMiniGameResult Result { get; private set; }


    [Inject]
    public void Construct(EquationConfigs configs) {
        _configs = configs;

        Equations.AddRange(_configs.Configs.ToArray());
        _maxEquationCount = Equations.Count;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true)
            _equationCountPanel.Show(true);
    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _accordancePanel = GetPanelByType<AccordancePanel>();
        _accordancePanel.Init(_configs);

        _equationCountPanel = GetPanelByType<EquationCountPanel>();
        _equationCountPanel.Init(this, _configs.Configs.Count);

    }

    public override void AddListeners() {
        base.AddListeners();

        ApplyButton.onClick.AddListener(ApplyButtonButtonClick);
        _accordancePanel.EquationVerificatedChanged += OnEquationVerificatedChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        ApplyButton.onClick.RemoveListener(ApplyButtonButtonClick);
        _accordancePanel.EquationVerificatedChanged -= OnEquationVerificatedChanged;
    }

    public override void PreparingForClosure() {
        bool gameResult = (Equations.Count > 0) ? true : false;

        Result = new CodingMiniGameResult(PassedEquation, gameResult);
    }

    private void ApplyButtonButtonClick() => ApplyClicked?.Invoke();

    private void OnEquationVerificatedChanged(EquationConfig config, bool result) {
        SetVerificationResult(config, result);

        if (result == false)
            return;

        Equations.Remove(config);
        EquationsCountChanged?.Invoke(Equations.Count, _maxEquationCount);

        if (Equations.Count == 0) {
            ApplyButton.gameObject.SetActive(true);
            Invoke(nameof(PreparingForClosure), DelaySwitchingEquation);
        }   
    }

    private void SetVerificationResult(EquationConfig data, bool result) {
        var equation = new EquationConfig(
            data.Description,
            data.Name,
            result);

        PassedEquation.Add(equation);
    }

}
