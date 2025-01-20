using UnityEngine;

public class CarSFXManager : SoundManager {
    [SerializeField] private CarSoundConfig _config;

    public CarSoundConfig SoundConfig => _config;

    private AnimatorEventsHandler _eventsHandler;

    public void Init(Car car) {
        _eventsHandler = car.EventsHandler;
        AudioSource = GetComponent<AudioSource>();
        AudioSource.volume = Volume.Volume;

        AddListener();
        OnCarMoved();
    }

    public override void AddListener() {
        _eventsHandler.DriftStarted += OnDriftStarted;
        _eventsHandler.StrikStarted += OnStrikStarted;
        _eventsHandler.DriftProgressed += OnDriftProgressed;
    }

    public override void RemoveLisener() {
        _eventsHandler.DriftStarted -= OnDriftStarted;
        _eventsHandler.StrikStarted -= OnStrikStarted;
        _eventsHandler.DriftProgressed -= OnDriftProgressed;
    }

    private void OnCarMoved() => PlayOneShot(_config.Move);

    private void OnDriftStarted() => PlayOneShot(_config.DriftStart);

    private void OnStrikStarted() => PlayOneShot(_config.Strike);

    private void OnDriftProgressed() => PlayOneShot(_config.DriftProgress);

    private void PlayOneShot(AudioClip clip) {

        if (clip != null)
            AudioSource.PlayOneShot(clip);

    }
}
