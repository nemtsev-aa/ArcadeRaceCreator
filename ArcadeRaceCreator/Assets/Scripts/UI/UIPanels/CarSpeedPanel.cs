using System;
using TMPro;
using UnityEngine;

public class CarSpeedPanel : UIPanel {
    public event Action CarSelected;

    [SerializeField] private TextMeshProUGUI _speedLabel;

    private Car _car;

    public void Init() {

    }

    public void SetCar(Car car) {
        if (_car != null)
            RemoveListeners();

        _car = car;
        AddListeners();

        CarSelected?.Invoke();
    }

    private void OnAbsoluteCarSpeedChaned(int speed) {
        _speedLabel.text = $"{speed}";
    }

    public override void AddListeners() {
        base.AddListeners();

        _car.CarController.AbsoluteCarSpeedChaned += OnAbsoluteCarSpeedChaned;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _car.CarController.AbsoluteCarSpeedChaned -= OnAbsoluteCarSpeedChaned;
    }
}
