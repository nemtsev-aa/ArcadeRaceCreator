using System;
using System.Collections.Generic;

public class CarSelectionDialog : Dialog {
    public static event Action ApplyClicked;
    public static event Action<CarTypes> CarConfigsSelected;

    private CarsTypePanel _carsTypePanel;

    public List<CarConfig> Configs { get; private set; }

    public override void Show(bool value) {
        base.Show(value);

        if (true) {
            _carsTypePanel.Show(true);
        }
    }

    public override void InitializationPanels() {
        _carsTypePanel = GetPanelByType<CarsTypePanel>();
        _carsTypePanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        ApplyButton.onClick.AddListener(ApplyButtonButtonClick);
        _carsTypePanel.CarsListChanged += OnCarsListChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        ApplyButton.onClick.RemoveListener(ApplyButtonButtonClick);
        _carsTypePanel.CarsListChanged -= OnCarsListChanged;
    }

    private void ApplyButtonButtonClick() => ApplyClicked?.Invoke();
    
    private void OnCarsListChanged(CarTypes types, List<CarConfig> config) {
        if (ApplyButton.gameObject.activeInHierarchy == false)
            ApplyButton.gameObject.SetActive(true);

        Configs = config;
        CarConfigsSelected?.Invoke(types);
    }

}
