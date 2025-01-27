using System;
using System.Collections.Generic;

public class RoadMapDialog : Dialog {
    public static event Action<ProjectStageTypes> ProjectStageSelected;

    private ApplicationManager _applicationManager;
    private ProjectStagesPanel _projectStagesPanel;
    private InvitationToCooperationPanel _invitationToCooperationPanel;

    private ProjectStageTypes CurrentProjectStageType => _applicationManager.CurrentProjectStageType;
    private Dictionary<ProjectStageTypes, bool> ProjectStages => _applicationManager.ProjectStages;
    private Logger Logger => _applicationManager.Logger;

    public void SetApplicationManager(ApplicationManager applicationManager) {
        _applicationManager = applicationManager;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true) {
            Logger.Log($"RoadMapDialog show true!");

            _projectStagesPanel.Show(true);
            _invitationToCooperationPanel.Show(false);

            UpdateProjectStagePanel();
        }
    }

    public void UpdateProjectStagePanel() {
        foreach (ProjectStageTypes iStage in ProjectStages.Keys) {
            _projectStagesPanel.UpdateProjectStateView(iStage, ProjectStages[iStage]);
        }

        if (CurrentProjectStageType == ProjectStageTypes.InvitationToCooperation) {
            ApplyButton.gameObject.SetActive(true);
            ApplyButton.onClick.AddListener(ApplyButtonClick);
        }

        if (CurrentProjectStageType != ProjectStageTypes.InvitationToCooperation) {
            _projectStagesPanel.ShowProjectStageViewByType(CurrentProjectStageType);
        }
    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _projectStagesPanel = GetPanelByType<ProjectStagesPanel>();
        _projectStagesPanel.Init();

        _invitationToCooperationPanel = GetPanelByType<InvitationToCooperationPanel>();
        _invitationToCooperationPanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _projectStagesPanel.ProjectStageTypesChanged += OnProjectStageTypesChanged;
    }

    public override void BackButtonClick() {
        ResetPanels();
        ProjectStageSelected?.Invoke(ProjectStageTypes.None);
    }

    private void OnProjectStageTypesChanged(ProjectStageTypes type) {
        ProjectStageSelected?.Invoke(type);
    }

    private void ApplyButtonClick() {
        _projectStagesPanel.Show(false);
        _invitationToCooperationPanel.Show(true);
    }
}
