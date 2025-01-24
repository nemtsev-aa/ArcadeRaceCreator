using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using System;

public class UIManager : MonoBehaviour, IDisposable {
    private DialogFactory _factory;
    private List<Dialog> _dialogs;

    private ApplicationManager _applicationManager;

    public DialogSwitcher DialogSwitcher { get; private set; }
    
    private Dialog ActiveDialog => DialogSwitcher.ActiveDialog;

    [Inject]
    public void Constuct(DialogFactory factory) {
        _factory = factory;
        _factory.SetDialogsParent(GetComponent<RectTransform>());
        _dialogs = new List<Dialog>();
    }

    public void Init(ApplicationManager applicationManager) {
        _applicationManager = applicationManager; 
        DialogSwitcher = new DialogSwitcher(this);

        AddListener();
    }

    public void ShowMainMenuDialog() => DialogSwitcher.ShowDialog<MainMenuDialog>();

   #region Creating Dialogs

    public T GetDialogByType<T>() where T : Dialog {
        var dialog = GetDialogFromList<T>();

        if (dialog != null)
            return dialog;
        else
            return CreateNewDialog<T>();
    }

    private T GetDialogFromList<T>() where T : Dialog {
        if (_dialogs.Count == 0)
            return null;

        return (T)_dialogs.FirstOrDefault(dialog => dialog is T);
    }

    private T CreateNewDialog<T>() where T : Dialog {
        var dialog = _factory.GetDialog<T>();
        dialog.Init();

        _dialogs.Add(dialog);

        return dialog;
    }
    #endregion

   #region Dialogs Events

    private void AddListener() {
        MainMenuDialog.RoadMapShowed += OnShowRoadMapDialog;
        MainMenuDialog.SettingsDialogShowed += OnShowSettingsDialog;

        RoadMapDialog.ProjectStageSelected += OnRoadMapDialog_ProjectStageSelected;

        EnvironmentTypeSelectionDialog.BackClicked += OnEnvironmentTypeSelectionDialog_BackClicked;
        EnvironmentTypeSelectionDialog.EnvironmentTypeSelected += OnEnvironmentTypeSelectionDialog_EnvironmentTypeSelected;
        EnvironmentTypeSelectionDialog.ApplyClicked += OnEnvironmentTypeSelectionDialog_ApplyClicked;

        EnvironmentEditorDialog.BackClicked += OnEnvironmentEditorDialog_BackClicked;
        EnvironmentEditorDialog.ApplyClicked += OnEnvironmentEditorDialog_ApplyClicked;

        CarSelectionDialog.BackClicked += OnCarSelectionDialog_BackClicked;
        CarSelectionDialog.CarConfigsSelected += OnCarSelectionDialog_CarConfigsSelected;
        CarSelectionDialog.ApplyClicked += OnCarSelectionDialog_ApplyClicked;

        CodingMiniGameDialog.BackClicked += OnCodingMiniGameDialog_BackClicked;
        CodingMiniGameDialog.ApplyClicked += OnCodingMiniGameDialog_ApplyClicked;

    }

    private void RemoveLisener() {
        MainMenuDialog.RoadMapShowed -= OnShowRoadMapDialog;
        MainMenuDialog.SettingsDialogShowed -= OnShowSettingsDialog;

        EnvironmentTypeSelectionDialog.BackClicked -= OnEnvironmentTypeSelectionDialog_BackClicked;
        EnvironmentTypeSelectionDialog.EnvironmentTypeSelected -= OnEnvironmentTypeSelectionDialog_EnvironmentTypeSelected;
        EnvironmentTypeSelectionDialog.ApplyClicked -= OnEnvironmentTypeSelectionDialog_ApplyClicked;

        EnvironmentEditorDialog.BackClicked -= OnEnvironmentEditorDialog_BackClicked;
        EnvironmentEditorDialog.ApplyClicked -= OnEnvironmentEditorDialog_ApplyClicked;

        CarSelectionDialog.BackClicked -= OnCarSelectionDialog_BackClicked;
        CarSelectionDialog.CarConfigsSelected -= OnCarSelectionDialog_CarConfigsSelected;
        CarSelectionDialog.ApplyClicked -= OnCarSelectionDialog_ApplyClicked;

        CodingMiniGameDialog.BackClicked -= OnCodingMiniGameDialog_BackClicked;
        CodingMiniGameDialog.ApplyClicked -= OnCodingMiniGameDialog_ApplyClicked;

    }

    #endregion

    private void OnShowMainMenuDialog() =>
        DialogSwitcher.ShowDialog<MainMenuDialog>();

    private void OnShowSettingsDialog()  =>
        DialogSwitcher.ShowDialog<SettingsDialog>();

    private void OnSettingsDialogBackClicked() =>
        OnShowMainMenuDialog();

    private void OnShowRoadMapDialog() => DialogSwitcher.ShowDialog<RoadMapDialog>();

    //private void OnSettingsDialogResetClicked() => 
    //    _levelsMode.ResetPlayerProgress();

    #region RoadMap
    private void OnRoadMapDialog_ProjectStageSelected(ProjectStageTypes type) {
        _applicationManager.SwitchToStage(type);
    }

    #endregion


    #region EnvironmentTypeSelectionDialog

    private void OnEnvironmentTypeSelectionDialog_BackClicked() {
        OnShowRoadMapDialog();
    }

    private void OnEnvironmentTypeSelectionDialog_EnvironmentTypeSelected(EnvironmentTypes type) {
        _applicationManager.ShowEnvironment(type);
    }

    private void OnEnvironmentTypeSelectionDialog_ApplyClicked() {
        EnvironmentTypeSelectionDialog dialog = GetDialogByType<EnvironmentTypeSelectionDialog>();
        _applicationManager.SetEnvironmentType(dialog.Config.Type);

        OnShowRoadMapDialog();
    }

    #endregion

    #region EnvironmentEditorDialog

    private void OnEnvironmentEditorDialog_BackClicked() {
        OnShowRoadMapDialog();
    }

    private void OnEnvironmentEditorDialog_ApplyClicked() {
        _applicationManager.SetEnvironmentEditorData();

        OnShowRoadMapDialog();
    }

    #endregion

    #region CarSelectionDialog

    private void OnCarSelectionDialog_BackClicked() {
        OnShowRoadMapDialog();
    }

    private void OnCarSelectionDialog_CarConfigsSelected(CarTypes type) {
        _applicationManager.SetCarsType(type);
    }

    private void OnCarSelectionDialog_ApplyClicked() {
        var dialog = GetDialogByType<CarSelectionDialog>();
        _applicationManager.SetCarsConfig();

        OnShowRoadMapDialog();
    }

    #endregion

    #region CodingMiniGameDialog
    private void OnCodingMiniGameDialog_BackClicked() {
        OnShowRoadMapDialog();
    }

    private void OnCodingMiniGameDialog_ApplyClicked() {
        CodingMiniGameDialog dialog = GetDialogByType<CodingMiniGameDialog>();
        _applicationManager.SetCodingMiniGameResult(dialog.Result);
        
        OnShowRoadMapDialog();
    }

    #endregion

    #region GameDialog Events

    //private void OnHintClicked(bool value) =>
    //    _levelsMode.SetPause(value);

    //private void OnPauseClicked(bool value) =>
    //    _levelsMode.SetPause(value);

    private void OnMainMenuClicked() => OnShowMainMenuDialog();

    private void OnGameDialogResetClicked() {
        //CurrentMode.FinishGameplay(Switchovers.CurrentLevel);

        //if (ActiveDialog is LearningModeDialog) {
        //    OnLearningModeSelected();
        //    return;
        //}

        //if (ActiveDialog is ArcadeModeDialog) {
        //    OnArcadeModeSelected();
        //    return;
        //}
    }
        

    //private void OnLevelDialogNextLevelClicked() =>
    //    _levelsMode.FinishGameplay(Switchovers.NextLevel);

    //private void OnGameDialogCountdownPanelHided() =>
    //    _levelsMode.ShowRewarded();

    private void OnArcadeModeSelected() {
        //_applicationManager.SwitchToMode<ArcadeGameMode>();

        //DialogSwitcher.ShowDialog<ArcadeModeDialog>();
    }

    #endregion

    #region LearningDialog Events

    //private void OnNextLearningStep() =>
    //    _learningMode.ShowDescriptionInPanel();

    //private void OnLearningFinished() {
    //    _learningMode.FinishGameplay(Switchovers.MainMenu);

    //    OnShowMainMenuDialog();
    //    GetDialogByType<MainMenuDialog>().ShowGameModesPanel();
    //}

    #endregion

    public void Dispose() {
        RemoveLisener();
    }
}
