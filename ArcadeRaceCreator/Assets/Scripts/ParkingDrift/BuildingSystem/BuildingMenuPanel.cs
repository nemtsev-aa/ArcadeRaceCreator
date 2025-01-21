using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BuildingMenuPanel : UIPanel {
    public event Action<BuildingFunctionTypes> ActivateBuildingFunctionTypeChanged;

    [SerializeField] private RectTransform _menuItemsParent;

    private BuildingMenuItemConfigs _configs;
    private UICompanentsFactory _factory;

    private List<BuildingMenuView> _menuItems = new List<BuildingMenuView>();
    private BuildingMenuViewConfig _currentConfig;
    private BuildingMenuView _currentBuildingMenuItem;

    [Inject]
    public void Construct(BuildingMenuItemConfigs configs, UICompanentsFactory factory) {
        _configs = configs;
        _factory = factory;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _menuItems.Count == 0)
            Create();
    }

    public void SetBuildingFunctionType(BuildingFunctionTypes type) {
        _currentConfig = _configs.GetConfigByBuildingFunctionType(type);
        BuildingMenuView item = _menuItems.FirstOrDefault(i => i.Config.Type == type);

        OnBuildingFunctionTypeSelected(item);
    }

    private void Create() {

        foreach (BuildingMenuViewConfig iConfig in _configs.Configs) {
            BuildingMenuView item = _factory.Get<BuildingMenuView>(iConfig, _menuItemsParent);
                
            item.Init(iConfig);
            item.BuildingFunctionTypeSelected += OnBuildingFunctionTypeSelected;

            _menuItems.Add(item);
        }
    }

    private void OnBuildingFunctionTypeSelected(BuildingMenuView item) {
        if (_currentBuildingMenuItem != null && _currentBuildingMenuItem.Equals(item) != true) {
            _currentBuildingMenuItem.Activate(false);
        }

        _currentBuildingMenuItem = item;
        _currentConfig = _configs.GetConfigByBuildingFunctionType(_currentBuildingMenuItem.Config.Type);

        ActivateBuildingFunctionTypeChanged?.Invoke(_currentConfig.Type);
    }
}


