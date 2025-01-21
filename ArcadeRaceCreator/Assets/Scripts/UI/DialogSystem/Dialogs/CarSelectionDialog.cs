using System;
using System.Collections.Generic;

public class CarSelectionDialog : Dialog {
    public event Action<List<CarConfig>> CarConfigsSelected;

    private CarsTypePanel _carsTypePanel;

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

        _carsTypePanel.CarsListChanged += OnCarsListChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _carsTypePanel.CarsListChanged -= OnCarsListChanged;
    }

    private void OnCarsListChanged(List<CarConfig> config) {
        CarConfigsSelected?.Invoke(config);
    }
}
