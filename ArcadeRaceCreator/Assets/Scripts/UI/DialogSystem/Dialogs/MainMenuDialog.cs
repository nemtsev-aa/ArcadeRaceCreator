using System;
using UnityEngine;

public class MainMenuDialog : Dialog {
    public static event Action SettingsDialogShowed;
    public static event Action AboutDialogShowed;

    public static event Action LearningModeSelected;
    public static event Action LevelsModeSelected;
    public static event Action ArcadeModeSelected;

    private MenuCategoryPanel _categoryPanel;
    private GameModePanel _gameModesPanel;

    public override void Show(bool value) {
        base.Show(value);

        if (true) {
            _categoryPanel.Show(true);
            _gameModesPanel.Show(false);
        }
    }

    public override void InitializationPanels() {
        _categoryPanel = GetPanelByType<MenuCategoryPanel>();
        _categoryPanel.Init();

        _gameModesPanel = GetPanelByType<GameModePanel>();
        _gameModesPanel.Init();
        _gameModesPanel.Show(false);
    }

    public void ShowGameModesPanel() {
        _categoryPanel.Show(false);
        _gameModesPanel.Show(true);
    }

    public override void AddListeners() {
        base.AddListeners();

        _categoryPanel.GameModesSelected += ShowGameModesPanel;
        _categoryPanel.SettingsDialogSelected += OnSettingsDialogSelected;
        _categoryPanel.AboutDialogSelected += OnAboutDialogSelected;
        _categoryPanel.QuitButtonSelected += OnQuitButtonSelected;

        _gameModesPanel.LearningModeButtonClicked += OnLearningModeButtonClicked;
        _gameModesPanel.LevelsModeButtonClicked += OnLevelsModeButtonClicked;
        _gameModesPanel.ArcadeModeButtonClicked += OnArcadeModeButtonClicked;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _categoryPanel.GameModesSelected -= ShowGameModesPanel;
        _categoryPanel.SettingsDialogSelected -= OnSettingsDialogSelected;
        _categoryPanel.AboutDialogSelected -= OnAboutDialogSelected;
        _categoryPanel.QuitButtonSelected -= OnQuitButtonSelected;

        _gameModesPanel.LearningModeButtonClicked -= OnLearningModeButtonClicked;
        _gameModesPanel.LevelsModeButtonClicked -= OnLevelsModeButtonClicked;
        _gameModesPanel.ArcadeModeButtonClicked -= OnArcadeModeButtonClicked;
    }

    private void OnSettingsDialogSelected() => SettingsDialogShowed?.Invoke();

    private void OnAboutDialogSelected() => AboutDialogShowed?.Invoke();

    private void OnQuitButtonSelected() => Application.Quit();

    private void OnLearningModeButtonClicked() => LearningModeSelected?.Invoke();

    private void OnLevelsModeButtonClicked() => LevelsModeSelected?.Invoke();

    private void OnArcadeModeButtonClicked() => ArcadeModeSelected?.Invoke();
}
