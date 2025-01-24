using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CarsManager : Manager {
    public event Action<Car> CurrentCarChanged;

    private CarConfigs _carConfigs;

    private List<Car> _arcadeSpawnedCars = new List<Car>();
    private List<Car> _rallySpawnedCars = new List<Car>();
    private List<Car> _sovietSpawnedCars = new List<Car>();

    private Car _currentCar;
    private CarTypes _currentCarType = CarTypes.None;
    List<Car> _currentSpawnedList = new List<Car>();

    private List<Transform> _spawnPoints => ApplicationManager.EnvironmentManager.SpawnPoints;
    
    [Inject]
    public void Construct(CarConfigs carConfigs) {
        _carConfigs = carConfigs;
    }

    public void ShowCarsByType(CarTypes type) {
        if (_currentCarType == type)
            return;

        if (_currentCarType != CarTypes.None)
            ShowAllCars(false);

        _currentCarType = type;

        switch (type) {
            case CarTypes.Arcade:
                _currentSpawnedList = _arcadeSpawnedCars;
                break;

            case CarTypes.Rally:
                _currentSpawnedList = _rallySpawnedCars;
                break;

            case CarTypes.Soviet:
                _currentSpawnedList = _sovietSpawnedCars;
                break;

            default:
                break;
        }

        if (_currentSpawnedList.Count == 0)
            SpawnCars();
        else
            ShowAllCars(true);
    }
    
    public void PrepareCars(bool status) {
        foreach (Car iCar in _currentSpawnedList) {
            iCar.Prepare(status);
        }
    }

    public void ShowAllCars(bool status) {
        foreach (var iCar in _currentSpawnedList) {
            iCar.Show(status);
        }

        if (_currentCar != null) {
            _currentCar.Activate(false);
            _currentCar = null;
        }
    }

    private void SpawnCars() {
        List<CarConfig> configs = _carConfigs.GetCarConfigsByType(_currentCarType);
        
        for (int i = 0; i < configs.Count; i++) {
            
            Transform iPoint = _spawnPoints[i];
            Car car = Instantiate(configs[i].Prefab, iPoint.position, iPoint.rotation);
            car.transform.SetParent(transform);

            car.Init();
            car.Selected += OnCarSelected;

            AddCarToSpawnList(car);  
        }
    }

    private void OnCarSelected(Car car) {
        foreach (var iCar in _currentSpawnedList) {
            iCar.Show(false);
        }

        _currentCar = car;
        ApplicationManager.ActivateCar(car);
    }

    private void AddCarToSpawnList(Car car) {
        switch (_currentCarType) {
            case CarTypes.Arcade:
                _arcadeSpawnedCars.Add(car);
                break;

            case CarTypes.Rally:
                _rallySpawnedCars.Add(car);
                break;

            case CarTypes.Soviet:
                _sovietSpawnedCars.Add(car);
                break;

            default:
                break;
        }
    }

}
