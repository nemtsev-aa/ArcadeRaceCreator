using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CameraControllsPanel : UIPanel {
    [SerializeField] private Image _moveIcon;
    [SerializeField] private Image _zoomIcon;
    [SerializeField] private Image _rotateIcon;

    [SerializeField] private float _animationDuration = 0.5f;

    private CameraMoveController _cameraMoveController;
    private Sequence _moveIconColored;
    private Sequence _zoomIconColored;
    private Sequence _rotateIconColored;

    public void Init(CameraMoveController cameraMove) {
        _cameraMoveController = cameraMove;

        AddListeners();
        CreateSequences();
    }

    public override void AddListeners() {
        base.AddListeners();

        _cameraMoveController.Moved += OnMoved;
        _cameraMoveController.Zoomed += OnZoomed;
        _cameraMoveController.Rotated += OnRotated;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _cameraMoveController.Moved -= OnMoved;
        _cameraMoveController.Zoomed -= OnZoomed;
        _cameraMoveController.Rotated -= OnRotated;
    }

    private void OnMoved() => StartMoveIconColoredSequence();

    private void OnZoomed() => StartZoomIconColoredSequence();

    private void OnRotated() => StartRotateIconColoredSequence();

    private void CreateSequences() {
        _moveIconColored = DOTween.Sequence();
        _moveIconColored.Append(_moveIcon.DOColor(Color.green, _animationDuration));
        _moveIconColored.Append(_moveIcon.DOColor(Color.white, _animationDuration));
        _moveIconColored.SetAutoKill(false);

        _zoomIconColored = DOTween.Sequence();
        _zoomIconColored.Append(_zoomIcon.DOColor(Color.green, _animationDuration));
        _zoomIconColored.Append(_zoomIcon.DOColor(Color.white, _animationDuration));
        _zoomIconColored.SetAutoKill(false);

        _rotateIconColored = DOTween.Sequence();
        _rotateIconColored.Append(_rotateIcon.DOColor(Color.green, _animationDuration));
        _rotateIconColored.Append(_rotateIcon.DOColor(Color.white, _animationDuration));
        _rotateIconColored.SetAutoKill(false);
    }

    private void StartMoveIconColoredSequence() {
        _moveIconColored.Play();
        _moveIconColored.Restart();
    }

    private void StartZoomIconColoredSequence() {
        _zoomIconColored.Play();
        _zoomIconColored.Restart();
    }

    private void StartRotateIconColoredSequence() {
        _rotateIconColored.Play();
        _rotateIconColored.Restart();
    }

}
