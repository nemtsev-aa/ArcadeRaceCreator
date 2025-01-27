using System;

public class GameplayDialog : Dialog {
    public static event Action ApplyButtonClicked;

    private CarControllsPanel _carControllsPanel;
    private CarSpeedPanel _carSpeedPanel;

    public GameplayResult Result { get; private set; }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true) {
            ShowAllPanels(false);

            ApplyButton.gameObject.SetActive(true);
        }
    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _carControllsPanel = GetPanelByType<CarControllsPanel>();
        //_carControllsPanel.Init();

        _carSpeedPanel = GetPanelByType<CarSpeedPanel>();
        _carSpeedPanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _carSpeedPanel.CarSelected += OnCarSelected;
        ApplyButton.onClick.AddListener(ApplyButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _carSpeedPanel.CarSelected -= OnCarSelected;
        ApplyButton.onClick.RemoveListener(ApplyButtonClick);
    }

    public override void PreparingForClosure() {
        Result = new GameplayResult(true);
    }

    public void ShowAllPanels(bool status) {
        _carControllsPanel.Show(status);
        _carSpeedPanel.Show(status);
    }

    private void OnCarSelected() {
        ShowAllPanels(true);
    }

    private void ApplyButtonClick() {
        PreparingForClosure();
        ApplyButtonClicked?.Invoke();
    }
}
