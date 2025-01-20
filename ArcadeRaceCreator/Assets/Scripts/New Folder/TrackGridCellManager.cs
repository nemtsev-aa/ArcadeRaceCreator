using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackGridCellManager : MonoBehaviour {
    public event Action<TrackGridCell> StartSegmentAdded;
    public event Action<TrackGridCell> FinishSegmentAdded;
    public event Action<TrackSegmentType, float> SegmentTypeReceived;

    [SerializeField] private RectTransform _grid;

    private TrackGridCell[] _cells;
    private TrackGridCell _currentTrackGridCell;

    public TrackGridCell CurrentTrackGridCell => _currentTrackGridCell;

    public IReadOnlyList<TrackGridCell> TrackGridCells => _cells.ToList();

    void Start() {
        _cells = _grid.GetComponentsInChildren<TrackGridCell>();

        foreach (TrackGridCell element in _cells) {
            element.Selected += OnTrackGridCellSelected;
            element.SegmentTypeReceived += OnSegmentTypeReceived;
        }
    }

    public void SetContentToCell(TrackSegmentConfig config) {
        if (_currentTrackGridCell == null)
            return;

        _currentTrackGridCell.SetContent(config);

        if (config.Type == TrackSegmentType.Start)
            StartSegmentAdded?.Invoke(_currentTrackGridCell);

        if (config.Type == TrackSegmentType.Finish)
            FinishSegmentAdded?.Invoke(_currentTrackGridCell);
    }

    private void OnTrackGridCellSelected(TrackGridCell cell) {
        if (_currentTrackGridCell != null)
            _currentTrackGridCell.DeselectElement();

        _currentTrackGridCell = cell;
        //
    }

    private void OnSegmentTypeReceived(TrackSegmentType type, float angle) {
        SegmentTypeReceived?.Invoke(type, angle);

        _currentTrackGridCell = null;
    }

    private void SelectElement(TrackGridCell element) {
        // Сначала снимаем выделение со всех элементов
        DeselectAllElements();

        // Выделяем текущий элемент
        element.SelectElement();
    }

    private void DeselectAllElements() {
        TrackGridCell[] allElements = FindObjectsOfType<TrackGridCell>();
        
        foreach (TrackGridCell element in allElements) {
            element.DeselectElement();
        }
    }
}
