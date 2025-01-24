using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour {
    public const int InitDelay = 1;

    [SerializeField] private ApplicationManager _applicationManager;
    [SerializeField] private UIManager _uIManager;
    //[SerializeField] 
    private EnvironmentSoundManager _environmentSoundManager;
    //[SerializeField] 
    private CarSFXManager _carSFXManager;

    private Logger _logger;
    private SoundsLoader _soundsLoader;

    [Inject]
    public void Construct(Logger logger, SoundsLoader soundsLoader) {
        _logger = logger;
        _soundsLoader = soundsLoader;
    }

    private async void Start() {
        await Init();
    }

    private async UniTask Init() {
        _logger.Log("Bootstrap Init");

        //await SoundsLoading();

        _logger.Log("ApplicationManager Init");
        //_applicationManager.Init(_uIManager, _environmentSoundManager, _carSFXManager);
        _applicationManager.Init(_uIManager);

        _logger.Log("Bootstrap Complited");
    }

    private async UniTask<bool> SoundsLoading() {
        _logger.Log("Sounds Loading...");

        bool envSound = await TryEnvironmentSoundManagerInit();
        bool sfx = await TrySheepSFXManagerInit();

        if (envSound == true && sfx == true) {
            _logger.Log("Sounds Loading Complited");
            return true;
        }
        else {
            _logger.Log("Sounds Loading Not Complited");
            return false;
        }
    }

    private async UniTask<bool> TryEnvironmentSoundManagerInit() {
        await _soundsLoader.LoadAsset(_environmentSoundManager.SoundConfig.ClipUrl[0], OnUIAudioClipLoaded);

        List<AudioClip> sounds = await _soundsLoader.LoadAssets(new List<string>() {
            _environmentSoundManager.SoundConfig.ClipUrl[1],
            _environmentSoundManager.SoundConfig.ClipUrl[2],
            }
        );

        if (sounds != null) {
            _environmentSoundManager.SoundConfig.SetAudioClips(sounds[0], sounds[1]);
            _logger.Log($"EnvironmentSoundManagerInit Complited: {sounds.Count}");

            return true;
        }

        _logger.Log($"EnvironmentSoundManagerInit Not Complited");
        return false;
    }

    private void OnUIAudioClipLoaded(AudioClip clip) {
        _environmentSoundManager.SoundConfig.SetUIAudioClip(clip);
        _environmentSoundManager.Init();
        _environmentSoundManager.PlaySound(MusicType.UI);
    }

    private async UniTask<bool> TrySheepSFXManagerInit() {
        List<AudioClip> sounds = await _soundsLoader.LoadAssets(_carSFXManager.SoundConfig.ClipUrl);

        if (sounds != null) {
            _carSFXManager.SoundConfig.SetAudioClips(sounds[0], sounds[1], sounds[2], sounds[3]);

            _logger.Log($"SheepSFXManagerInit Complited: {sounds.Count}");
            return true;
        }

        _logger.Log($"SheepSFXManagerInit Not Complited");
        return false;
    }

}

