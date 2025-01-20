using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingMenuView : MonoBehaviour {
    public event Action<BuildingFunctionType> ActivateBuildingFunctionTypeChanged;

    [SerializeField] private BuildingMenuItemConfigs _configs;
    [SerializeField] private RectTransform _menuItemsParent;
    [SerializeField] private BuildingMenuItem _buildingMenuItemPrefab;

    private List<BuildingMenuItem> _menuItems = new List<BuildingMenuItem>();
    private BuildingMenuItemConfig _currentConfig;
    private BuildingMenuItem _currentBuildingMenuItem;

    public void Activate(bool status) {
        gameObject.SetActive(status);

        if (status == true && _menuItems.Count == 0)
            Create();
    }

    public void SetBuildingFunctionType(BuildingFunctionType type) {
        _currentConfig = _configs.GetConfigByBuildingFunctionType(type);
        BuildingMenuItem item = _menuItems.FirstOrDefault(i => i.Config.Type == type);

        OnBuildingFunctionTypeSelected(item);
    }

    private void Create() {

        foreach (BuildingMenuItemConfig iConfig in _configs.Configs) {
            BuildingMenuItem item = Instantiate(_buildingMenuItemPrefab, _menuItemsParent);

            item.Init(iConfig);
            item.BuildingFunctionTypeSelected += OnBuildingFunctionTypeSelected;

            _menuItems.Add(item);
        }
    }

    private void OnBuildingFunctionTypeSelected(BuildingMenuItem item) {
        if (_currentBuildingMenuItem != null && _currentBuildingMenuItem.Equals(item) != true) {
            _currentBuildingMenuItem.Activate(false);
        }

        _currentBuildingMenuItem = item;
        _currentConfig = _configs.GetConfigByBuildingFunctionType(_currentBuildingMenuItem.Config.Type);

        ActivateBuildingFunctionTypeChanged?.Invoke(_currentConfig.Type);
    }
}


