using UnityEngine;
using System;
using System.Collections.Generic;

public class TrackDraw : MonoBehaviour, IDisposable {
    public event Action<List<TrackGridCell>> DrawingFinished;

    [SerializeField] private TrackGridCellManager _trackGridCellManager;
    [SerializeField] private TrackGeneratorConfigs _trackGeneratorConfigs;

    private Camera _mainCamera;
    private TrackGridCell _currentTrackGridCell;
    private List<TrackGridCell> _track;

    public bool IsActive { get; private set; } = true;

    void Start() {
        _mainCamera = Camera.main;
        _track = new List<TrackGridCell>();
        
        AddListeners();
    }

    public void Activate(bool status) {
        IsActive = status;
    }

    void Update() {
        if (IsActive == false)
            return;

        TrackGridCell cell = PerformRaycast();
        if (cell == null)
            return;

        if (_currentTrackGridCell == null) {
            _currentTrackGridCell = cell;
            _currentTrackGridCell.HoverElement();
        }

        if (Input.GetMouseButton(0)) {

            if (_track.Count == 0) {
                _currentTrackGridCell.SelectElement();
                return;
            }
 
            if (cell != _currentTrackGridCell) {
                _currentTrackGridCell.DeselectElement();
                _currentTrackGridCell = cell;
                _currentTrackGridCell.SelectElement();
            }

            if (cell == _currentTrackGridCell) {
                _currentTrackGridCell.ActivateCollidersManager(true);
                Debug.Log($"{_currentTrackGridCell.gameObject.name}");
            }
        }
    }
    private void AddListeners() {
        _trackGridCellManager.StartSegmentAdded += OnStartSegmentAdded;
        _trackGridCellManager.FinishSegmentAdded += OnFinishSegmentAdded;
        _trackGridCellManager.SegmentTypeReceived += OnSegmentTypeReceived;
    }

    private void OnStartSegmentAdded(TrackGridCell startCell) {
        _track.Add(startCell);
    }

    private void OnSegmentTypeReceived(TrackSegmentType type, float angle) {
        if (_track.Count == 0)
            return;

        _currentTrackGridCell.SetContent(_trackGeneratorConfigs.GetSegmentConfigByType(type));
        _currentTrackGridCell.transform.localEulerAngles = new Vector3(0, 0, angle);
        

        // Переход на следующую клетку

        // Добавление текущей клетки в путь
        if (_track.Contains(_currentTrackGridCell) == false) {
            _track.Add(_currentTrackGridCell);
            _currentTrackGridCell.ActivateCollidersManager(false);
            _currentTrackGridCell.DeselectElement();

            _currentTrackGridCell = null;
        }
            
    }

    private void OnFinishSegmentAdded(TrackGridCell finishCell) {
        _track.Add(finishCell);

        DrawingFinished?.Invoke(_track);
    }


   

    private TrackGridCell PerformRaycast() {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
            TrackGridCell cell = hit.collider.GetComponent<TrackGridCell>();

            if (cell != null)
                return cell;
        }

        return null;
    }

    private void OnDrawGizmos() {
        // Визуализация луча в редакторе для отладки
        if (_mainCamera != null) {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 500f); // 500 единиц для визуализации
        }
    }

    public void Dispose() {
        _trackGridCellManager.StartSegmentAdded -= OnStartSegmentAdded;
        _trackGridCellManager.FinishSegmentAdded -= OnFinishSegmentAdded;
        _trackGridCellManager.SegmentTypeReceived -= OnSegmentTypeReceived;
    }
}
