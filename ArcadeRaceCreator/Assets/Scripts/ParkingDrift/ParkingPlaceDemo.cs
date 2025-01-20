using System;
using UnityEngine;
using DG.Tweening;

public class ParkingPlaceDemo : MonoBehaviour {
    [Range(1, 10)]
    [SerializeField] private float _moveSpeed = 2;
    [Range(1, 10)] 
    [SerializeField] private float _rotateSpeed = 5;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    private Transform _defaultCameraPoint;
    private bool _isActive;
    private Transform _currentTargetCameraPoint;

    private void Awake() {
        _defaultCameraPoint = Camera.main.transform;

        Activate(true);
    }

    public void Activate(bool status) {
        _isActive = status;

        if (status == false) 
            _currentTargetCameraPoint = _defaultCameraPoint;
        else
            _currentTargetCameraPoint = GetNextTarget();

        MoveToTargetCameraPoint();
    }

    [ContextMenu(nameof(DeActivate))]
    public void DeActivate() {
        Activate(false);
    }

    private void MoveToTargetCameraPoint() {
        if (_currentTargetCameraPoint == null)
            return;

        Transform cameraTransform = Camera.main.transform;

        Sequence newSeq = DOTween.Sequence();
            newSeq.Append(cameraTransform.DOMove(_currentTargetCameraPoint.position, _moveSpeed));
            newSeq.Append(cameraTransform.DORotate(_currentTargetCameraPoint.localEulerAngles, _rotateSpeed));
            newSeq.OnComplete(() => {
                _currentTargetCameraPoint = GetNextTarget();
                MoveToTargetCameraPoint();
            });
    }

    private Transform GetNextTarget() {
        if (_isActive == false) {
            _moveSpeed = 1f;
            return null;
        }

        if (_currentTargetCameraPoint == null) {
            _moveSpeed = 1f;
            return _startPoint;
        }

        if (_currentTargetCameraPoint == _startPoint) {
            _moveSpeed = 15f;
            return _endPoint;
        }
            
        if (_currentTargetCameraPoint == _endPoint) {
            _moveSpeed = 15f;
            return _startPoint;
        }

        return null;
    }
}