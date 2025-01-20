using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using System;

public class UIManager : MonoBehaviour, IDisposable {
    private DialogFactory _factory;
    private List<Dialog> _dialogs;

    private ApplicationManager _applicationManager;

    //private LevelsGameMode _levelsMode;
    //private LearningGameMode _learningMode;
    //private ArcadeGameMode _arcadeMode;

    public DialogSwitcher DialogSwitcher { get; private set; }
    private Dialog ActiveDialog => DialogSwitcher.ActiveDialog;
    private IGameMode CurrentMode => _applicationManager.CurrentMode;

    [Inject]
    public void Constuct(DialogFactory factory) {
        _factory = factory;
        _factory.SetDialogsParent(GetComponent<RectTransform>());
        _dialogs = new List<Dialog>();
    }

    public void Init(ApplicationManager applicationManager) {
        _applicationManager = applicationManager; 

        //_learningMode = applicationManager.GetGameModeByType<LearningGameMode>();
        //_levelsMode = applicationManager.GetGameModeByType<LevelsGameMode>(); 
        //_arcadeMode = applicationManager.GetGameModeByType<ArcadeGameMode>(); 

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
        //MainMenuDialog.SettingsDialogShowed += OnShowSettingsDialog;

        //GameModeDialog.ResetClicked += OnGameDialogResetClicked;
        //GameModeDialog.MainMenuClicked += OnMainMenuClicked;
        //GameModeDialog.PauseClicked += OnPauseClicked;
        //GameModeDialog.HintClicked += OnHintClicked;
        //GameModeDialog.CountdownPanelHided += OnGameDialogCountdownPanelHided;
        
        //LevelsModeDialog.NextLevelClicked += OnLevelDialogNextLevelClicked;
       
        //LearningModeDialog.NextStepClicked += OnNextLearningStep;
        //LearningModeDialog.MainMenuClicked += OnShowMainMenuDialog;
        //LearningModeDialog.LearningFinished += OnLearningFinished;
    }

    private void RemoveLisener() {
        //MainMenuDialog.SettingsDialogShowed -= OnShowSettingsDialog;

        //LevelSelectionDialog.LevelStarted -= OnLevelStarted;

        //GameModeDialog.ResetClicked -= OnGameDialogResetClicked;
        //GameModeDialog.MainMenuClicked -= OnMainMenuClicked;
        //GameModeDialog.PauseClicked -= OnPauseClicked;
        //GameModeDialog.HintClicked -= OnHintClicked;
        //GameModeDialog.CountdownPanelHided -= OnGameDialogCountdownPanelHided;

        //LevelsModeDialog.NextLevelClicked -= OnLevelDialogNextLevelClicked;

        //LearningModeDialog.MainMenuClicked -= OnShowMainMenuDialog;
        //LearningModeDialog.NextStepClicked -= OnNextLearningStep;
        //LearningModeDialog.LearningFinished -= OnLearningFinished;

    }

    #endregion

    private void OnShowMainMenuDialog() =>
        DialogSwitcher.ShowDialog<MainMenuDialog>();

    private void OnShowSettingsDialog()  =>
        DialogSwitcher.ShowDialog<SettingsDialog>();

    private void OnSettingsDialogBackClicked() =>
        OnShowMainMenuDialog();

    //private void OnSettingsDialogResetClicked() => 
    //    _levelsMode.ResetPlayerProgress();

    #region GameDialog Events

    //private void OnHintClicked(bool value) =>
    //    _levelsMode.SetPause(value);
    
    //private void OnPauseClicked(bool value) =>
    //    _levelsMode.SetPause(value);

    private void OnMainMenuClicked() {
        CurrentMode.FinishGameplay(Switchovers.MainMenu);

        OnShowMainMenuDialog();
    }

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
