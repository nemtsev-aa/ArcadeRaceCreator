using System;

public class EnvironmentEditorDialog : Dialog {
    public event Action<BuildingFunctionTypes> BuildingFunctionTypesChanged;

    private BuildingMenuPanel _buildingMenuPanel;

    public override void Show(bool value) {
        base.Show(value);

        if (true) {
            _buildingMenuPanel.Show(true);
        }
    }

    public override void InitializationPanels() {
        _buildingMenuPanel = GetPanelByType<BuildingMenuPanel>();
    }

    public override void AddListeners() {
        base.AddListeners();

        _buildingMenuPanel.ActivateBuildingFunctionTypeChanged += OnActivateBuildingFunctionTypeChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _buildingMenuPanel.ActivateBuildingFunctionTypeChanged -= OnActivateBuildingFunctionTypeChanged;
    }

    private void OnActivateBuildingFunctionTypeChanged(BuildingFunctionTypes type) {
        BuildingFunctionTypesChanged?.Invoke(type);
    }
}
