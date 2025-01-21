using System;
using UnityEngine;

public class Car : MonoBehaviour, IDisposable {
    public event Action<Car> Selected;

    [SerializeField] private PrometeoCarController _carController;
    [SerializeField] private SelectionView _selectionView;

    [field: SerializeField] public Transform CameraAnker { get; private set; }
    [field: SerializeField] public AnimatorEventsHandler EventsHandler { get; private set; }
    public void Init() {
        _carController.enabled = false;

        _selectionView.Selected += OnSelected;
    }

    public void Show(bool status) {
        gameObject.SetActive(status);

        if (status == false)
            Activate(false);
        else
            _selectionView.gameObject.SetActive(true);
    }

    public void Activate(bool status) {
        _selectionView.gameObject.SetActive(false);
        _carController.enabled = status;
        _carController.carEngineSound.Play();
    }

    private void OnSelected() => Selected?.Invoke(this);

    public void Dispose() {
        _selectionView.Selected -= OnSelected;
    }
}
