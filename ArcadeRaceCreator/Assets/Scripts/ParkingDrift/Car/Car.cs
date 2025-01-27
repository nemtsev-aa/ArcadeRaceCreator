using System;
using UnityEngine;


public class Car : MonoBehaviour, IDisposable {
    private const float DefoultcarEngineSoundVolume = 0.1f;

    public event Action<Car> Selected;

    [SerializeField] private PrometeoCarController _carController;
    
    [field: SerializeField] public Transform CameraAnker { get; private set; }
    [field: SerializeField] public AnimatorEventsHandler EventsHandler { get; private set; }
    
    public Rigidbody Rigidbody { get; private set; }
    public PrometeoCarController CarController => _carController;
    public SpawnPoint SpawnPoint { get; private set; }

    public void Init(SpawnPoint spawnPoint) {
        Rigidbody ??= GetComponent<Rigidbody>();

        _carController.enabled = false;
        SpawnPoint = spawnPoint;

        SetDefaultVolume();
        Prepare(false);     
    }

    public void Show(bool status) {
        gameObject.SetActive(status);

        if (status == false)
            Activate(false);
    }

    public void Prepare(bool status) {
        SpawnPoint.gameObject.SetActive(status);

        if (status)
            SpawnPoint.Selected += OnSelected;
        else
            SpawnPoint.Selected -= OnSelected;
    }

    public void Activate(bool status) {
        SpawnPoint.gameObject.SetActive(!status);
        Rigidbody.isKinematic = !status;

        _carController.enabled = status;

        if (status == true)
            _carController.carEngineSound.Play();
        else
            _carController.carEngineSound.Stop();
    }

    private void OnSelected() => Selected?.Invoke(this);

    private void SetDefaultVolume() {
        _carController.carEngineSound.volume = DefoultcarEngineSoundVolume;
        _carController.tireScreechSound.volume = DefoultcarEngineSoundVolume;
    }

    public void Dispose() {
        SpawnPoint.Selected -= OnSelected;
    }
}
