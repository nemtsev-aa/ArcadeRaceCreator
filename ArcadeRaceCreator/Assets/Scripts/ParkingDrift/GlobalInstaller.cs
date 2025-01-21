using System;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    public const float InitDelay = 0.1f;

    [SerializeField] private DialogPrefabs _dialogPrefabs;
    [SerializeField] private UICompanentPrefabs _uiCompanentPrefabs;
    [SerializeField] private EnvironmentConfigs _environmentConfigs;
    [SerializeField] private CarConfigs _carConfigs;

    private Logger _logger;

    public override void InstallBindings() {
        BindTimeCounter();
        BindServices();
        BuildConfigs();

        BindUIPrefabs();      
        BindFactories();
        BindInput();

        _logger.Log("Global Installing Complited");
    }

    private void BindTimeCounter() {
        TimeCounter timeCounter = new TimeCounter();

        Container.BindInstance(timeCounter).AsSingle();
        Container.BindInterfacesAndSelfTo<ITickable>().FromInstance(timeCounter).AsSingle();
    }

    private void BindServices() {
        _logger = new Logger();
        Container.Bind<Logger>().FromInstance(_logger).AsSingle();

        Container.Bind<PauseHandler>().AsSingle();
        Container.Bind<SoundsLoader>().AsSingle();
    }

    private void BuildConfigs() {
        if (_environmentConfigs.Configs.Count == 0)
            _logger.Log($"List of EnvironmentConfigs is empty");

        Container.Bind<EnvironmentConfigs>().FromInstance(_environmentConfigs).AsSingle();

        if (_carConfigs.Configs.Count == 0)
            _logger.Log($"List of CarConfigs is empty");

        Container.Bind<CarConfigs>().FromInstance(_carConfigs).AsSingle();
    }

    private void BindUIPrefabs() {
        if (_dialogPrefabs.Prefabs.Count == 0)
            _logger.Log($"List of DialogPrefabs is empty");

        Container.Bind<DialogPrefabs>().FromInstance(_dialogPrefabs).AsSingle();

        if (_uiCompanentPrefabs.Prefabs.Count == 0)
            _logger.Log($"List of UICompanentPrefabs is empty");

        Container.Bind<UICompanentPrefabs>().FromInstance(_uiCompanentPrefabs).AsSingle();
    }

    private void BindFactories() {
        Container.Bind<DialogFactory>().AsSingle();
        Container.Bind<UICompanentsFactory>().AsSingle();
    }

    private void BindInput() {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
        else
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();

        Container.Bind<SwipeHandler>().AsSingle().NonLazy();
    }
}