using System;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour, IDisposable {
    public event Action Selected;

    [SerializeField] private Button _selectButton;

    public Car Car { get; private set; }

    private void Start() {
        _selectButton.onClick.AddListener(SelectButton_Click);
    }

    public void Show(bool status) {
        gameObject.SetActive(status);
    }

    public void SetCar(Car car) {
        Car = car;
    }

    private void SelectButton_Click() {
        Selected?.Invoke();
    }

    public void Dispose() {
        _selectButton.onClick.RemoveListener(SelectButton_Click);
    }
}
