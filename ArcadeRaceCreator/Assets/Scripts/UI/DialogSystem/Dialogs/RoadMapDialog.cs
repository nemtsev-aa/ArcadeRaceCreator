using System;

public class RoadMapDialog : Dialog {
    public static event Action<ProjectStageTypes> ProjectStageSelected;
    private ApplicationManager _applicationManager;
    private ProjectStagesPanel _projectStagesPanel;

    private ProjectStageTypes CurrentProjectStageType => _applicationManager.CurrentProjectStageType;

    public void SetApplicationManager(ApplicationManager applicationManager) {
        _applicationManager = applicationManager;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true) {
            _projectStagesPanel.Show(true);
            _projectStagesPanel.ShowProjectStageViewByType(CurrentProjectStageType);
        }
            
    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _projectStagesPanel = GetPanelByType<ProjectStagesPanel>();
        _projectStagesPanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _projectStagesPanel.ProjectStageTypesChanged += OnProjectStageTypesChanged;
    }

    private void OnProjectStageTypesChanged(ProjectStageTypes type) {
        ProjectStageSelected?.Invoke(type);
    }


}
