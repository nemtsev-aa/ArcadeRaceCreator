using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ApplicationManager : MonoBehaviour {
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private CameraMoveController _cameraMove;
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
    public Logger Logger => _logger;
    public UIManager UIManager => _uIManager;
    public SoundsLoader SoundsLoader => _soundsLoader;
    public EnvironmentSoundManager EnvironmentSound => _environmentSoundManager;
    public CarSFXManager SheepSFXManager => _sheepSFXManager;

    public Manager CurrentActiveManager { get; private set; }
    public AttemptData AttemptData { get; private set; }
    public ProjectStageTypes CurrentProjectStageType { get; private set; }
    public EnvironmentManager EnvironmentManager { get; private set; }
    public CameraMoveController CameraMoveController => _cameraMove;
    
    public Dictionary<ProjectStageTypes, bool> ProjectStages = new Dictionary<ProjectStageTypes, bool>();

    public Car CurrentCar { get; private set; }

    #endregion

    [Inject]
    public void Construct(Logger logger, SoundsLoader soundsLoader) {
        _logger = logger;
        _soundsLoader = soundsLoader;
    }

    public void Init(UIManager uIManager) {
        _uIManager = uIManager;
        _uIManager.Init(this);
        
        _uIManager.GetDialogByType<RoadMapDialog>().SetApplicationManager(this);
        _uIManager.ShowMainMenuDialog();

        AttemptData = new AttemptData();
        InitManagers();

        CurrentProjectStageType = ProjectStageTypes.EnvironmentTypeSelection;

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

        if (type == ProjectStageTypes.None) {
            UIManager.DialogSwitcher.ShowDialog<MainMenuDialog>();
            return;
        }
        
        if (ProjectStages.ContainsKey(type) == false) 
            ProjectStages.Add(type, false);

        CurrentProjectStageType = type;

        switch (type) {

            case ProjectStageTypes.None:
                UIManager.DialogSwitcher.ShowDialog<MainMenuDialog>();

                break;

            case ProjectStageTypes.EnvironmentTypeSelection:
                SwitchActiveManager(_environmentDemo);
                ShowEnvironment(EnvironmentTypes.Parking);

                UIManager.DialogSwitcher.ShowDialog<EnvironmentTypeSelectionDialog>();
                break;

            case ProjectStageTypes.EnvironmentEditing:
                ShowEnvironment(EnvironmentManager.Type);

                EnvironmentManager.ActvatePhysicForInteractionObjects(false);

                SwitchActiveManager(_environmentEditor);

                _cameraMove.SetTargetPosition(_environmentEditor.gameObject.transform.position);
                _cameraMove.Activate(true);

                UIManager.DialogSwitcher.ShowDialog<EnvironmentEditorDialog>();

                break;

            case ProjectStageTypes.CarsTypeSelection:
                SwitchActiveManager(_carsManager);

                _cameraFollow.SetTarget(EnvironmentManager.SpawnPoints[1].transform);
                _cameraFollow.Activate(true);

                UIManager.DialogSwitcher.ShowDialog<CarSelectionDialog>();
                _carsManager.ShowCarsByType(CarTypes.Arcade);

                break;

            case ProjectStageTypes.CodingMiniGame:
                UIManager.DialogSwitcher.ShowDialog<CodingMiniGameDialog>();
                
                _cameraMove.Activate(false);

                break;

            case ProjectStageTypes.Gameplay:
                SwitchActiveManager(_carsManager);
                _cameraMove.Activate(false);

                EnvironmentManager.ActvatePhysicForInteractionObjects(true);
                PrepareCars();

                UIManager.DialogSwitcher.ShowDialog<GameplayDialog>();
                _cameraFollow.SetTarget(EnvironmentManager.SpawnPoints[1].transform);
                _cameraFollow.Activate(true);

                break;

            default:
                break;
        }
    }

    public void ShowEnvironment(EnvironmentTypes type) {
        _environmentDemo.ShowEnvironmentByType(type);
    }

    public void SetEnvironmentType() {
        _environmentDemo.Activate(false);

        EnvironmentManager = _environmentDemo.CurrentEnvironmentManager;
        AttemptData.SetEnvironmentType(EnvironmentManager.Type);

        ProjectStages[ProjectStageTypes.EnvironmentTypeSelection] = true;
        CurrentProjectStageType = ProjectStageTypes.EnvironmentEditing;
    }

    public void SetEnvironmentEditorData() {
        EnvironmentEditorData data = EnvironmentManager.GetEnvironmentEditorData();
        AttemptData.SetEnvironmentEditorData(data);

        _cameraMove.MoveWhileEdgeScreen = false;
        _cameraMove.Activate(false);

        ProjectStages[ProjectStageTypes.EnvironmentEditing] = true;
        CurrentProjectStageType = ProjectStageTypes.CarsTypeSelection;
    }

    public void SetCarsType(CarTypes type) {
        AttemptData.SetCarType(type);
        
        _carsManager.ShowCarsByType(type);
        _carsManager.PrepareCars(false);

        ProjectStages[ProjectStageTypes.CarsTypeSelection] = true;
        CurrentProjectStageType = ProjectStageTypes.CodingMiniGame;
    }

    public void SetCodingMiniGameResult(CodingMiniGameResult result) {
        AttemptData.SetCodingMiniGameResult(result);

        ProjectStages[ProjectStageTypes.CodingMiniGame] = true;
        CurrentProjectStageType = ProjectStageTypes.Gameplay;
    }

    public void SetGameplayResult(GameplayResult result) {
        _carsManager.ResetCar();

        if (result.Status == false)
            return;

        AttemptData.SetGameplayResult(result);
        ProjectStages[ProjectStageTypes.Gameplay] = true;
        CurrentProjectStageType = ProjectStageTypes.InvitationToCooperation;
    }

    public void PrepareCars() {
        _carsManager.PrepareCars(true);
    }

    public void ActivateCar(Car car) {
        CurrentCar = car;

        CurrentCar.Show(true);
        CurrentCar.Activate(true);

        _cameraFollow.SetTarget(CurrentCar.transform);
        _cameraFollow.Activate(true);

        UIManager.GetDialogByType<GameplayDialog>()
                 .GetPanelByType<CarSpeedPanel>()
                 .SetCar(CurrentCar);

        _carsManager.PrepareCars(false);
    }

    public void SetCarsConfig(List<CarConfig> configs) {

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

