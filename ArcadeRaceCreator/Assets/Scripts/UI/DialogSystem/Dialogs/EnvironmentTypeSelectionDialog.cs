using System;

public class EnvironmentTypeSelectionDialog : Dialog {
    public event Action<EnvironmentConfig> EnvironmentConfigSelected;

    private EnvironmentTypePanel _environmentTypePanel;

    public override void Show(bool value) {
        base.Show(value);

        if (true) {
            _environmentTypePanel.Show(true);
        }
    }

    public override void InitializationPanels() {
        _environmentTypePanel = GetPanelByType<EnvironmentTypePanel>();
        _environmentTypePanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _environmentTypePanel.EnvironmentTypeChanged += OnEnvironmentTypeChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _environmentTypePanel.EnvironmentTypeChanged -= OnEnvironmentTypeChanged;
    }

    private void OnEnvironmentTypeChanged(EnvironmentConfig config) {
        EnvironmentConfigSelected?.Invoke(config);
    }
}
