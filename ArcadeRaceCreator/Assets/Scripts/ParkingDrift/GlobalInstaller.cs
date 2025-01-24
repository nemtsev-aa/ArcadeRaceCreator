using System;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    public const float InitDelay = 0.1f;

    [SerializeField] private DialogPrefabs _dialogPrefabs;
    [SerializeField] private UICompanentPrefabs _uiCompanentPrefabs;
    [SerializeField] private ProjectStageConfigs _projectStageConfigs;
    [SerializeField] private EnvironmentConfigs _environmentConfigs;
    [SerializeField] private BuildingMenuItemConfigs _buildingMenuItemConfigs;
    [SerializeField] private EquationConfigs _equationConfigs;
    [SerializeField] private AccordanceCompanentConfig _accordanceCompanentConfig;
    [SerializeField] private CarConfigs _carConfigs;
    

    private Logger _logger;

    public override void InstallBindings() {
        BindTimeCounter();
        BindServices();
        BuildConfigs();

        BindUIPrefabs();
        BindLineSpawner();
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
        if (_projectStageConfigs.Configs.Count == 0)
            _logger.Log($"List of ProjectStageConfigs is empty");

        Container.Bind<ProjectStageConfigs>().FromInstance(_projectStageConfigs).AsSingle();

        if (_environmentConfigs.Configs.Count == 0)
            _logger.Log($"List of EnvironmentConfigs is empty");

        Container.Bind<EnvironmentConfigs>().FromInstance(_environmentConfigs).AsSingle();
        
        if (_buildingMenuItemConfigs.Configs.Count == 0)
            _logger.Log($"List of BuildingMenuItemConfigs is empty");

        Container.Bind<BuildingMenuItemConfigs>().FromInstance(_buildingMenuItemConfigs).AsSingle();

        if (_carConfigs.Configs.Count == 0)
            _logger.Log($"List of CarConfigs is empty");

        Container.Bind<CarConfigs>().FromInstance(_carConfigs).AsSingle();

        if (_equationConfigs.Configs.Count == 0)
            _logger.Log($"List of EquationConfigs is empty");

        Container.Bind<EquationConfigs>().FromInstance(_equationConfigs).AsSingle();

        if (_accordanceCompanentConfig.FalseVerificationColor == null)
            _logger.Log($"FalseVerificationColor is empty");

        Container.Bind<AccordanceCompanentConfig>().FromInstance(_accordanceCompanentConfig).AsSingle();

        
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

    private void BindLineSpawner() {
        LineFactory factory = new LineFactory(Container);
        Container.Bind<LineFactory>().FromInstance(factory).AsSingle();

        LineSpawner lineSpawner = new LineSpawner(factory, _accordanceCompanentConfig);
        Container.Bind<LineSpawner>().AsSingle().NonLazy();
    }

    private void BindInput() {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
        else
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();

        Container.Bind<SwipeHandler>().AsSingle().NonLazy();
        Container.Bind<MovementHandler>().AsSingle().NonLazy();
    }
}