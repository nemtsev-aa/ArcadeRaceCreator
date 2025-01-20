using System;
using System.Collections.Generic;
using UnityEngine;

public class CarsManager : MonoBehaviour {
    public event Action<Car> CurrentCarChanged;

    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Car> _carPrefabs;

    private List<Car> _spawnedCars = new List<Car>();
    private Car _currentCar;

    public void Init() {
        SpawnCars();
        ShowAllCars();
    }

    private void SpawnCars() {
        for (int i = 0; i < _carPrefabs.Count; i++) {
            Transform iPoint = _spawnPoints[i];
            Car car = Instantiate(_carPrefabs[i], iPoint.position, iPoint.rotation);
            car.Init();
            car.Selected += OnCarSelected;

            _spawnedCars.Add(car);
        }
    }

    public void ShowAllCars() {
        foreach (var iCar in _spawnedCars) {
            iCar.Show(true);
        }

        if (_currentCar != null) {
            _currentCar.Activate(false);
            _currentCar = null;
        }
    }

    private void OnCarSelected(Car car) {
        foreach (var iCar in _spawnedCars) {
            iCar.Show(false);
        }

        _currentCar = car;
        _currentCar.Show(true);
        _currentCar.Activate(true);
        CurrentCarChanged?.Invoke(_currentCar);
    }
}
