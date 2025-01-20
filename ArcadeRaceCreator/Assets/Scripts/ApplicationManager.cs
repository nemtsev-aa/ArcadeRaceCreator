using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ApplicationManager : MonoBehaviour {
    //[SerializeField] private LearningGameMode _learningMode;
    //[SerializeField] private LevelsGameMode _levelsMode;
    //[SerializeField] private ArcadeGameMode _arcadeMode;

    private Logger _logger;
    private SoundsLoader _soundsLoader;
    private UIManager _uIManager;
    private EnvironmentSoundManager _environmentSoundManager;
    private CarSFXManager _sheepSFXManager;

    private List<IGameMode> _modes = new List<IGameMode>();
    private IGameMode _currentMode;

    public UIManager UIManager => _uIManager;
    public SoundsLoader SoundsLoader => _soundsLoader;
    public EnvironmentSoundManager EnvironmentSound => _environmentSoundManager;
    public CarSFXManager SheepSFXManager => _sheepSFXManager;
    public IGameMode CurrentMode => _currentMode;

    [Inject]
    public void Construct(Logger logger, SoundsLoader soundsLoader) {
        _logger = logger;
        _soundsLoader = soundsLoader;
    }

    public void Init(UIManager uIManager, EnvironmentSoundManager environmentSoundManager, CarSFXManager sheepSFXManager) {
        _uIManager = uIManager;
        _environmentSoundManager = environmentSoundManager;
        _sheepSFXManager = sheepSFXManager;

        _modes.AddRange(new IGameMode[] { 
            //_learningMode,
            //_levelsMode,
            //_arcadeMode
        });

        _logger.Log("UIManager Init");
        _uIManager.Init(this);

        _uIManager.ShowMainMenuDialog();
    }

    public void SwitchToMode<T>() {
        if (_currentMode != null) 
            _currentMode.FinishGameplay(Switchovers.AnotherGameMod);

        _currentMode = (IGameMode)GetGameModeByType<T>();
        _currentMode.Init(this);
        _currentMode.StartGamePlay();
    }

    public T GetGameModeByType<T>() {
        return (T)_modes.FirstOrDefault(mode => mode is T);
    }
}

