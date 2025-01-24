using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ApplicationManager : MonoBehaviour {
    //[SerializeField] private LearningGameMode _learningMode;
    //[SerializeField] private LevelsGameMode _levelsMode;
    //[SerializeField] private ArcadeGameMode _arcadeMode;

    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private CameraMove _cameraMove;
    [SerializeField] private EnvironmentDemo _environmentDemo;
    [SerializeField] private EnvironmentEditor _environmentEditor;
    [SerializeField] private CarsManager _carsManager;

    private Logger _logger;
    private SoundsLoader _soundsLoader;
    private EnvironmentSoundManager _environmentSoundManager;
    private UIManager _uIManager;
    private CarSFXManager _sheepSFXManager;

    private List<Manager> _managers = new List<Manager>();
    


    #region Public Propertis
    public UIManager UIManager => _uIManager;
    public SoundsLoader SoundsLoader => _soundsLoader;
    public EnvironmentSoundManager EnvironmentSound => _environmentSoundManager;
    public CarSFXManager SheepSFXManager => _sheepSFXManager;

    public Manager CurrentActiveManager { get; private set; }
    public AttemptData AttemptData { get; private set; }
    public ProjectStageTypes CurrentProjectStageType { get; private set; }
    public EnvironmentManager EnvironmentManager { get; private set; }
    public Car CurrentCar { get; private set; }

    #endregion

    [Inject]
    public void Construct(Logger logger, SoundsLoader soundsLoader) {
        _logger = logger;
        _soundsLoader = soundsLoader;
    }

    public void Init(UIManager uIManager, EnvironmentSoundManager environmentSoundManager, CarSFXManager sheepSFXManager) {
        _uIManager = uIManager;
        _environmentSoundManager = environmentSoundManager;
        _sheepSFXManager = sheepSFXManager;

        //_managers.AddRange(new IGameMode[] { 
        //    //_learningMode,
        //    //_levelsMode,
        //    //_arcadeMode
        //});

        _logger.Log("UIManager Init");
        _uIManager.Init(this);

        _uIManager.ShowMainMenuDialog();

        AttemptData = new AttemptData();
    }

    public void Init(UIManager uIManager) {
        _uIManager = uIManager;
        _uIManager.Init(this);
        _uIManager.ShowMainMenuDialog();

        _uIManager.GetDialogByType<RoadMapDialog>().SetApplicationManager(this);

        AttemptData = new AttemptData();

        InitManagers();

        _logger.Log("ApplicationManager Init");
    }

    public T GetManagerByType<T>() where T : Manager {
        return (T)_managers.FirstOrDefault(mode => mode is T);
    }

    public void SwitchActiveManager(Manager manager) {
        if (CurrentActiveManager != null)
            CurrentActiveManager.Activate(false);
        
        CurrentActiveManager = manager;
        CurrentActiveManager.Activate(true);
    }

    public void SwitchToStage(ProjectStageTypes type = ProjectStageTypes.None) {
        CurrentProjectStageType = type;

        switch (type) {
            case ProjectStageTypes.EnvironmentTypeSelection:
                UIManager.DialogSwitcher.ShowDialog<EnvironmentTypeSelectionDialog>();

                SwitchActiveManager(_environmentDemo);
                _environmentDemo.ShowEnvironmentByType(EnvironmentTypes.Parking);

                break;

            case ProjectStageTypes.EnvironmentEditing:
                SwitchActiveManager(_environmentEditor);
                _cameraMove.SetTargetPosition(_environmentEditor.gameObject.transform.position);
                _cameraMove.Activate(true);

                UIManager.DialogSwitcher.ShowDialog<EnvironmentEditorDialog>();

                break;

            case ProjectStageTypes.CarsTypeSelection:
                SwitchActiveManager(_carsManager);

                _cameraMove.SetTargetPosition(_carsManager.transform.position);
                _cameraMove.Activate(true);

                UIManager.DialogSwitcher.ShowDialog<CarSelectionDialog>();

                break;

            case ProjectStageTypes.CodingMiniGame:
                UIManager.DialogSwitcher.ShowDialog<CodingMiniGameDialog>();

                break;

            case ProjectStageTypes.Gameplay:
                SwitchActiveManager(_carsManager);
                
                _cameraMove.Activate(false);

                PrepareCars();

                UIManager.DialogSwitcher.ShowDialog<GameplayDialog>();

                break;

            case ProjectStageTypes.None:

                break;

            default:
                break;
        }
    }

    public void ShowEnvironment(EnvironmentTypes type) {
        _environmentDemo.ShowEnvironmentByType(type);
    }

    public void SetEnvironmentType(EnvironmentTypes type) {
        EnvironmentManager = _environmentDemo.CurrentEnvironmentManager;
        _environmentDemo.Activate(false);

        AttemptData.SetEnvironmentType(type);
    }

    public void SetEnvironmentEditorData() {
        EnvironmentEditorData data = EnvironmentManager.GetEnvironmentEditorData();
        AttemptData.SetEnvironmentEditorData(data);

        _cameraMove.Activate(false);
    }

    public void SetCarsType(CarTypes type) {
        AttemptData.SetCarType(type);
        
        _carsManager.ShowCarsByType(type);
        _carsManager.PrepareCars(false);
    }

    public void PrepareCars() {
        _carsManager.PrepareCars(true);
    }

    public void SetCarsConfig() {
        
    }

    public void SetCodingMiniGameResult(CodingMiniGameResult result) {
        AttemptData.SetCodingMiniGameResult(result);
    }

    public void ActivateCar(Car car) {
        CurrentCar = car;

        CurrentCar.Show(true);
        CurrentCar.Activate(true);

        _cameraFollow.SetTarget(CurrentCar.transform);
        _cameraFollow.Activate(true);
    }

    private void InitManagers() {
        _managers.AddRange(new Manager[] {
            _cameraFollow,
            _environmentDemo,
            _environmentEditor,
            _carsManager
        });

        foreach (var item in _managers) {
            item.Init(this);
            item.Activate(false);
        }
    }


}

