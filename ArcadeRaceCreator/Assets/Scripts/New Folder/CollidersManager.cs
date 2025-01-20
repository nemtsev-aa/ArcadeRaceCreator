using System;
using System.Collections.Generic;
using UnityEngine;

public class CollidersManager : MonoBehaviour {
    public event Action<List<PositionTypes>> ListColliderViewReceived;

    [SerializeField] private List<ColliderView> _views;
    [SerializeField] private bool _activateToStart = false;

    private List<PositionTypes> _selectedPositions = new List<PositionTypes>();

    private void Start() {
        if (_views.Count == 0)
            return;

        foreach (ColliderView iView in _views) {
            iView.Selected += OnSelected;
            iView.Activate(_activateToStart);
        }
    }

    public void Activate(bool status) {
        if (_views.Count == 0)
            return;

        foreach (ColliderView iView in _views) {
            iView.Activate(status);
        }
    }

    private void OnSelected(PositionTypes position) {
        if (_selectedPositions.Count == 0) {
            _selectedPositions.Add(position);
            return;
        }

        if (_selectedPositions.Count < 2 && _selectedPositions.Contains(position) == false) 
            _selectedPositions.Add(position);
        

        if (_selectedPositions.Count == 2) {
            ListColliderViewReceived?.Invoke(_selectedPositions);
            _selectedPositions.Clear();
            Activate(false);
        }
    }

    private bool CheckNeighboringCell(ColliderView view) {
        // Трубопровод
        
        // Начальная точка луча - позиция текущего объекта
        Vector3 rayOrigin = view.transform.position + view.transform.up * 20f;
        // Направление луча - вниз по оси Y
        Vector3 rayDirection = view.transform.up;
        // Длина луча - 10 единиц
        float rayLength = 40f;

        // Отправляем луч
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayLength)) {
            // Проверяем, является ли объект, попавший в луч, типа ColliderView
            ColliderView colliderView = hit.collider.GetComponentInParent<ColliderView>();
            if (colliderView != null) {
                Debug.Log("Объект типа ColliderView найден: " + hit.collider.gameObject.name);
                // Здесь можно добавить дополнительную логику, если нужно что-то сделать с найденным объектом
                return true;
            }

            Debug.Log("Объект найден, но не является типом ColliderView.");
            return false;
        }

        return false;
    }
}


