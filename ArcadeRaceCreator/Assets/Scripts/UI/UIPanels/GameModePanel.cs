using System;
using UnityEngine;
using UnityEngine.UI;

public class GameModePanel : UIPanel {
    public event Action LearningModeButtonClicked;
    public event Action LevelsModeButtonClicked;
    public event Action ArcadeModeButtonClicked;

    [SerializeField] private Button _learningButton;
    [SerializeField] private Button _levelsButton;
    [SerializeField] private Button _arcadeButton;

    public void Init() {
        AddListeners();
    }

    public override void Show(bool value) {
        base.Show(value);

    }

    public override void AddListeners() {
        base.AddListeners();

        _learningButton.onClick.AddListener(LearningModeButtonClick);
        _levelsButton.onClick.AddListener(LevelsModeButtonClick);
        _arcadeButton.onClick.AddListener(ArcadeModeButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _learningButton.onClick.RemoveListener(LearningModeButtonClick);
        _levelsButton.onClick.RemoveListener(LevelsModeButtonClick);
        _arcadeButton.onClick.RemoveListener(ArcadeModeButtonClick);
    }

    private void LearningModeButtonClick() => LearningModeButtonClicked?.Invoke();

    private void LevelsModeButtonClick() => LevelsModeButtonClicked?.Invoke();

    private void ArcadeModeButtonClick() => ArcadeModeButtonClicked?.Invoke();

}
