using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrackGridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDisposable {
    public event Action<TrackGridCell> Selected;
    public event Action<TrackSegmentType, float> SegmentTypeReceived;

    [SerializeField] private CollidersManager _collidersManager;
    [SerializeField] private Image _contentImage;

    private Sprite _defaultSprite;
    private TrackSegmentConfig _content;

    public bool IsSelected { get; private set; }

    void Start() {
        _defaultSprite = _contentImage.sprite;
        _collidersManager.ListColliderViewReceived += OnListColliderViewReceived;
    }

    private void OnListColliderViewReceived(List<PositionTypes> list) {
        SegmentParamerets parameters = GetSegmentParamerets(list);

        SegmentTypeReceived?.Invoke(parameters.Type, parameters.Angle);
    }

    public void SetContent(TrackSegmentConfig content) {
        _content = content;
        _contentImage.sprite = _content.Icon;

        DeselectElement();
        _collidersManager.Activate(false);
    }

    public void ResetContent() {
        _content = null;
        _contentImage.sprite = _defaultSprite;
    }

    public void SelectElement() {
        IsSelected = true;
        // Выделение элемента изменением цвета
        _contentImage.color = Color.green;

        //Selected?.Invoke(this);
    }

    public void DeselectElement() {
        IsSelected = false;
        // Снятие выделения
        _contentImage.color = Color.white;

    }

    public void HoverElement() {
        // Выделение элемента изменением цвета
        _contentImage.color = new Color(0.5f, 0.5f, 0.5f, 0.1f);

        Selected?.Invoke(this);
    }

    public void ActivateCollidersManager(bool status) {
        _collidersManager.Activate(status);
    }

    public TrackSegmentData GetTrackSegmentData() {
        if (_content == null) {
            return new TrackSegmentData();
        }

        return new TrackSegmentData(
            $"{_content.Type}",
            transform.localPosition,
            transform.localRotation
            );
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //if (Input.GetMouseButton(0)) {
        //    SelectElement();
        //    _collidersManager.Activate(true);
        //}
    }

    public void OnPointerExit(PointerEventData eventData) {
        //if (IsSelected == true) {
        //    _collidersManager.Activate(false);
        //}
    }

    public void OnPointerClick(PointerEventData eventData) {
        //SelectElement();
        ////_collidersManager.Activate(true);
    }

    private SegmentParamerets GetSegmentParamerets(List<PositionTypes> positions) {
        if (positions[0] == PositionTypes.Top) {

            if (positions[1] == PositionTypes.Right)
                return new SegmentParamerets(TrackSegmentType.Turn, 180);

            if (positions[1] == PositionTypes.Left)
                return new SegmentParamerets(TrackSegmentType.Turn, -90);

            if (positions[1] == PositionTypes.Down)
                return new SegmentParamerets(TrackSegmentType.Straight, 90);
        }

        if (positions[0] == PositionTypes.Right) {

            if (positions[1] == PositionTypes.Top)
                return new SegmentParamerets(TrackSegmentType.Turn, 180);

            if (positions[1] == PositionTypes.Down)
                return new SegmentParamerets(TrackSegmentType.Turn, 90);

            if (positions[1] == PositionTypes.Left)
                return new SegmentParamerets(TrackSegmentType.Straight, 180);
        }

        if (positions[0] == PositionTypes.Down) {

            if (positions[1] == PositionTypes.Right)
                return new SegmentParamerets(TrackSegmentType.Turn, 90);

            if (positions[1] == PositionTypes.Left)
                return new SegmentParamerets(TrackSegmentType.Turn, 0);

            if (positions[1] == PositionTypes.Top)
                return new SegmentParamerets(TrackSegmentType.Straight, -90);
        }

        if (positions[0] == PositionTypes.Left) {

            if (positions[1] == PositionTypes.Top)
                return new SegmentParamerets(TrackSegmentType.Turn, -90);

            if (positions[1] == PositionTypes.Down)
                return new SegmentParamerets(TrackSegmentType.Turn, 0);

            if (positions[1] == PositionTypes.Right)
                return new SegmentParamerets(TrackSegmentType.Straight, 0);
        }

        return new SegmentParamerets();
    }

    public void Dispose() {
        _collidersManager.ListColliderViewReceived -= OnListColliderViewReceived;
    }

    private class SegmentParamerets {
        public SegmentParamerets() {
        }

        public SegmentParamerets(TrackSegmentType type, float angle) {
            Type = type;
            Angle = angle;
        }

        public TrackSegmentType Type { get; private set; }
        public float Angle { get; private set; }
    }
}
