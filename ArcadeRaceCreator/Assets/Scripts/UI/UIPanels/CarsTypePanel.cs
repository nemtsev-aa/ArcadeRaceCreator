using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CarsTypePanel : UIPanel {
    public event Action<CarTypes, List<CarConfig>> CarsListChanged;

    [SerializeField] private RectTransform _viewParent;

    private CarConfigs _configs;
    private UICompanentsFactory _factory;

    private List<CarsTypeView> _viewItems = new List<CarsTypeView>();
    private List<CarConfig> _currentConfig;

    private CarsTypeView _currentCarsTypeView;

    [Inject]
    public void Construct(CarConfigs configs, UICompanentsFactory factory) {
        _configs = configs;
        _factory = factory;
    }

    public void Init() {
        AddListeners();
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _viewItems.Count == 0)
            Create();
    }

    public void SetCarsType(CarTypes type) {
        _currentConfig = _configs.GetCarConfigsByType(type);
        CarsTypeView item = _viewItems.FirstOrDefault(i => i.Config.Type == type);

        OnCarsTypeViewSelected(item);
    }

    private void Create() {
        foreach (CarClassConfig iConfig in _configs.Configs) {
            CarsTypeView item = _factory.Get<CarsTypeView>(iConfig, _viewParent);

            item.Init(iConfig);
            item.CarsTypeViewSelected += OnCarsTypeViewSelected;

            _viewItems.Add(item);
        }
    }

    private void OnCarsTypeViewSelected(CarsTypeView item) {
        if (_currentCarsTypeView != null && _currentCarsTypeView.Equals(item) != true) {
            _currentCarsTypeView.Activate(false);
        }

        _currentCarsTypeView = item;
        _currentConfig = _configs.GetCarConfigsByType(_currentCarsTypeView.Config.Type);

        CarsListChanged?.Invoke(_currentCarsTypeView.Config.Type, _currentConfig);
    }
}
