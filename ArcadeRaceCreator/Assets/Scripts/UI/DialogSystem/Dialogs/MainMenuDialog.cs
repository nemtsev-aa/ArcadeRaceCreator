using System;
using UnityEngine;

public class MainMenuDialog : Dialog {
    public static event Action RoadMapShowed;
    public static event Action SettingsDialogShowed;
    public static event Action AboutDialogShowed;

    public static event Action LearningModeSelected;
    public static event Action LevelsModeSelected;
    public static event Action ArcadeModeSelected;

    private MenuCategoryPanel _categoryPanel;

    public override void Show(bool value) {
        base.Show(value);

        if (true) {
            _categoryPanel.Show(true);
        }
    }

    public override void InitializationPanels() {
        _categoryPanel = GetPanelByType<MenuCategoryPanel>();
        _categoryPanel.Init();

    }

    public override void AddListeners() {
        base.AddListeners();

        _categoryPanel.GameModesSelected += OnGameModesSelected;
        _categoryPanel.SettingsDialogSelected += OnSettingsDialogSelected;
        _categoryPanel.AboutDialogSelected += OnAboutDialogSelected;
        _categoryPanel.QuitButtonSelected += OnQuitButtonSelected;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _categoryPanel.GameModesSelected -= OnGameModesSelected;
        _categoryPanel.SettingsDialogSelected -= OnSettingsDialogSelected;
        _categoryPanel.AboutDialogSelected -= OnAboutDialogSelected;
        _categoryPanel.QuitButtonSelected -= OnQuitButtonSelected;

    }

    private void OnGameModesSelected() => RoadMapShowed?.Invoke();

    private void OnSettingsDialogSelected() => SettingsDialogShowed?.Invoke();

    private void OnAboutDialogSelected() => AboutDialogShowed?.Invoke();

    private void OnQuitButtonSelected() => Application.Quit();
}
