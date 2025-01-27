using System;

public class EnvironmentEditorDialog : Dialog {
    public static event Action ApplyClicked;

    private EnvironmentEditorMenuPanel _buildingMenuPanel;
    private CameraControllsPanel _controllsPanel;
    private ApplicationManager _applicationManager;

    public override void Show(bool value) {
        base.Show(value);

        if (true) {
            _buildingMenuPanel.Show(true);
        }
    }

    public override void InitializationPanels() {
        _buildingMenuPanel = GetPanelByType<EnvironmentEditorMenuPanel>();
        _controllsPanel = GetPanelByType<CameraControllsPanel>();
    }

    public override void AddListeners() {
        base.AddListeners();

        ApplyButton.onClick.AddListener(ApplyButtonButtonClick);
        _buildingMenuPanel.ActivateBuildingFunctionTypeChanged += OnActivateBuildingFunctionTypeChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        ApplyButton.onClick.RemoveListener(ApplyButtonButtonClick);
        _buildingMenuPanel.ActivateBuildingFunctionTypeChanged -= OnActivateBuildingFunctionTypeChanged;
    }

    private void ApplyButtonButtonClick() => ApplyClicked?.Invoke();

    private void OnActivateBuildingFunctionTypeChanged(BuildingFunctionTypes type) {
        if (type == BuildingFunctionTypes.Save)
            ApplyButton.gameObject.SetActive(true);
    }

 
}
