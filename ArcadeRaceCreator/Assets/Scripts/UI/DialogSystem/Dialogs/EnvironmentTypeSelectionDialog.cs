using System;

public class EnvironmentTypeSelectionDialog : Dialog {
    public static event Action ApplyClicked;
    public static event Action<EnvironmentTypes> EnvironmentTypeSelected;

    private EnvironmentTypePanel _environmentTypePanel;

    public EnvironmentConfig Config { get; private set; }

    public override void Show(bool value) {
        base.Show(value);

        if (true) {
            _environmentTypePanel.Show(true);
            _environmentTypePanel.SetEnvironmentType(EnvironmentTypes.Parking);
        }
    }

    public override void InitializationPanels() {
        _environmentTypePanel = GetPanelByType<EnvironmentTypePanel>();
        _environmentTypePanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        ApplyButton.onClick.AddListener(ApplyButtonButtonClick);
        _environmentTypePanel.EnvironmentTypeChanged += OnEnvironmentTypeChanged;

    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        ApplyButton.onClick.RemoveListener(ApplyButtonButtonClick);
        _environmentTypePanel.EnvironmentTypeChanged -= OnEnvironmentTypeChanged;
    }

    private void ApplyButtonButtonClick() => ApplyClicked?.Invoke();

    private void OnEnvironmentTypeChanged(EnvironmentConfig config) {
        if (ApplyButton.gameObject.activeInHierarchy == false)
            ApplyButton.gameObject.SetActive(true);

        Config = config;

        EnvironmentTypeSelected?.Invoke(config.Type);
    }
}
