using System;
using UnityEngine;

public class ToolsManager : MonoBehaviour, IDisposable {
    [SerializeField] private ToolsPanel _toolsPanel;
    [SerializeField] private TrackGridCellManager _elementSelector;

    private TrackGridCell CurrentTrackGridCell => _elementSelector.CurrentTrackGridCell;

    private void Start() {
        _toolsPanel.RotateLeftClicked += OnRotateLeftClicked;
        _toolsPanel.RotateRightClicked += OnRotateRightClicked;
        _toolsPanel.CancelClicked += OnCancelClicked;
    }

    private void OnRotateRightClicked() {
        CurrentTrackGridCell.transform.Rotate(Vector3.forward, -90f);
    }

    private void OnRotateLeftClicked() {
        CurrentTrackGridCell.transform.Rotate(Vector3.forward, 90f);
    }

    private void OnCancelClicked() {
        CurrentTrackGridCell.ResetContent();
    }

    public void Dispose() {
        _toolsPanel.RotateLeftClicked -= OnRotateLeftClicked;
        _toolsPanel.RotateRightClicked -= OnRotateRightClicked;
        _toolsPanel.CancelClicked -= OnCancelClicked;
    }
}
