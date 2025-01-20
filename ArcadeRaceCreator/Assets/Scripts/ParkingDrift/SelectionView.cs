using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionView : MonoBehaviour, IDisposable {
    public event Action Selected;

    [SerializeField] private Button _selectButton;

    private void Start() {
        _selectButton.onClick.AddListener(SelectButton_Click);
    }

    public void Show(bool status) {
        gameObject.SetActive(status);
    }

    private void SelectButton_Click() {
        Selected?.Invoke();
    }

    public void Dispose() {
        _selectButton.onClick.RemoveListener(SelectButton_Click);
    }
}
