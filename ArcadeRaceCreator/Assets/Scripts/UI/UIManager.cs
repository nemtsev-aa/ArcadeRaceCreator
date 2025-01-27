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
        RoadMapDialog.BackClicked += OnRoadMapDialog_BackClicked;

        EnvironmentTypeSelectionDialog.BackClicked += OnEnvironmentTypeSelectionDialog_BackClicked;
        EnvironmentTypeSelectionDialog.EnvironmentTypeSelected += OnEnvironmentTypeSelectionDialog_EnvironmentTypeSelected;
        EnvironmentTypeSelectionDialog.ApplyClicked += OnEnvironmentTypeSelectionDialog_ApplyClicked;
        EnvironmentTypeSelectionDialog.MainMenuClicked += OnEnvironmentTypeSelectionDialog_MainMenuClicked;

        EnvironmentEditorDialog.BackClicked += OnEnvironmentEditorDialog_BackClicked;
        EnvironmentEditorDialog.ApplyClicked += OnEnvironmentEditorDialog_ApplyClicked;

        CarSelectionDialog.BackClicked += OnCarSelectionDialog_BackClicked;
        CarSelectionDialog.CarConfigsSelected += OnCarSelectionDialog_CarConfigsSelected;
        CarSelectionDialog.ApplyClicked += OnCarSelectionDialog_ApplyClicked;

        CodingMiniGameDialog.BackClicked += OnCodingMiniGameDialog_BackClicked;
        CodingMiniGameDialog.ApplyClicked += OnCodingMiniGameDialog_ApplyClicked;

        GameplayDialog.BackClicked += OnGameplayDialog_BackClicked;
        GameplayDialog.ApplyButtonClicked += OnGameplayDialog_ApplyButtonClicked;
        GameplayDialog.MainMenuClicked += OnGameplayDialog_ManMenuClicked;
    }


    private void RemoveLisener() {
        MainMenuDialog.RoadMapShowed -= OnShowRoadMapDialog;
        MainMenuDialog.SettingsDialogShowed -= OnShowSettingsDialog;

        RoadMapDialog.ProjectStageSelected -= OnRoadMapDialog_ProjectStageSelected;
        RoadMapDialog.BackClicked -= OnRoadMapDialog_BackClicked;

        EnvironmentTypeSelectionDialog.BackClicked -= OnEnvironmentTypeSelectionDialog_BackClicked;
        EnvironmentTypeSelectionDialog.EnvironmentTypeSelected -= OnEnvironmentTypeSelectionDialog_EnvironmentTypeSelected;
        EnvironmentTypeSelectionDialog.ApplyClicked -= OnEnvironmentTypeSelectionDialog_ApplyClicked;
        EnvironmentTypeSelectionDialog.MainMenuClicked -= OnEnvironmentTypeSelectionDialog_MainMenuClicked;

        EnvironmentEditorDialog.BackClicked -= OnEnvironmentEditorDialog_BackClicked;
        EnvironmentEditorDialog.ApplyClicked -= OnEnvironmentEditorDialog_ApplyClicked;

        CarSelectionDialog.BackClicked -= OnCarSelectionDialog_BackClicked;
        CarSelectionDialog.CarConfigsSelected -= OnCarSelectionDialog_CarConfigsSelected;
        CarSelectionDialog.ApplyClicked -= OnCarSelectionDialog_ApplyClicked;

        CodingMiniGameDialog.BackClicked -= OnCodingMiniGameDialog_BackClicked;
        CodingMiniGameDialog.ApplyClicked -= OnCodingMiniGameDialog_ApplyClicked;

        GameplayDialog.BackClicked -= OnGameplayDialog_BackClicked;
        GameplayDialog.ApplyButtonClicked -= OnGameplayDialog_ApplyButtonClicked;
        GameplayDialog.MainMenuClicked -= OnGameplayDialog_ManMenuClicked;
    }

    #endregion

    private void OnShowMainMenuDialog() =>
        DialogSwitcher.ShowDialog<MainMenuDialog>();

    private void OnShowSettingsDialog()  =>
        DialogSwitcher.ShowDialog<SettingsDialog>();

    private void OnSettingsDialogBackClicked() =>
        OnShowMainMenuDialog();

    private void OnShowRoadMapDialog() => DialogSwitcher.ShowDialog<RoadMapDialog>();

    private void OnMainMenuClicked() => OnShowMainMenuDialog();

    #region RoadMap

    private void OnRoadMapDialog_BackClicked() {
        ShowMainMenuDialog();
    }

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
        _applicationManager.SetEnvironmentType();

        OnShowRoadMapDialog();
    }

    private void OnEnvironmentTypeSelectionDialog_MainMenuClicked() {
        OnMainMenuClicked();
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
        _applicationManager.SetCarsConfig(dialog.Configs);

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

    #region GameplayDialog

    private void OnGameplayDialog_BackClicked() {
        _applicationManager.SetGameplayResult(new GameplayResult(false));

        OnShowRoadMapDialog();
    }

    private void OnGameplayDialog_ApplyButtonClicked() {
        GameplayDialog dialog = GetDialogByType<GameplayDialog>();
        _applicationManager.SetGameplayResult(dialog.Result);

        OnShowRoadMapDialog();
    }

    private void OnGameplayDialog_ManMenuClicked() {
        OnMainMenuClicked();
    }

    #endregion

    public void Dispose() {
        RemoveLisener();
    }
}
