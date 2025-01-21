using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnvironmentTypePanel : UIPanel {
    public event Action<EnvironmentConfig> EnvironmentTypeChanged;

    [SerializeField] private RectTransform _viewParent;

    private EnvironmentConfigs _configs;
    private UICompanentsFactory _factory;

    private List<EnvironmentTypeView> _viewItems = new List<EnvironmentTypeView>();
    private EnvironmentConfig _currentConfig;

    private EnvironmentTypeView _currentEnvironmentTypeView;

    [Inject]
    public void Construct(EnvironmentConfigs configs, UICompanentsFactory factory) {
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

    public void SetEnvironmentType(EnvironmentTypes type) {
        _currentConfig = _configs.GetEnvironmentConfigByType(type);
        EnvironmentTypeView item = _viewItems.FirstOrDefault(i => i.Config.Type == type);

        OnEnvironmentTypeViewSelected(item);
    }

    private void Create() {
        foreach (EnvironmentConfig iConfig in _configs.Configs) {
            EnvironmentTypeView item = _factory.Get<EnvironmentTypeView>(iConfig, _viewParent);

            item.Init(iConfig);
            item.EnvironmentTypeViewSelected += OnEnvironmentTypeViewSelected;

            _viewItems.Add(item);
        }
    }

    private void OnEnvironmentTypeViewSelected(EnvironmentTypeView item) {
        if (_currentEnvironmentTypeView != null && _currentEnvironmentTypeView.Equals(item) != true) {
            _currentEnvironmentTypeView.Activate(false);
        }

        _currentEnvironmentTypeView = item;
        _currentConfig = _configs.GetEnvironmentConfigByType(_currentEnvironmentTypeView.Config.Type);

        EnvironmentTypeChanged?.Invoke(_currentConfig);
    }
}
