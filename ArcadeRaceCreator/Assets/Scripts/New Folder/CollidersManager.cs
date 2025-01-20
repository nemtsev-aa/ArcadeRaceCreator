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
        // �����������
        
        // ��������� ����� ���� - ������� �������� �������
        Vector3 rayOrigin = view.transform.position + view.transform.up * 20f;
        // ����������� ���� - ���� �� ��� Y
        Vector3 rayDirection = view.transform.up;
        // ����� ���� - 10 ������
        float rayLength = 40f;

        // ���������� ���
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayLength)) {
            // ���������, �������� �� ������, �������� � ���, ���� ColliderView
            ColliderView colliderView = hit.collider.GetComponentInParent<ColliderView>();
            if (colliderView != null) {
                Debug.Log("������ ���� ColliderView ������: " + hit.collider.gameObject.name);
                // ����� ����� �������� �������������� ������, ���� ����� ���-�� ������� � ��������� ��������
                return true;
            }

            Debug.Log("������ ������, �� �� �������� ����� ColliderView.");
            return false;
        }

        return false;
    }
}


