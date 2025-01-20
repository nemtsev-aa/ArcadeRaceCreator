using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrackSegmentView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public event Action<TrackSegmentConfig> Selected;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _frameImage;
    [SerializeField] private Image _pointerFrame;

    private TrackSegmentConfig _config;

    [field: SerializeField] public TrackSegmentType Type { get; private set; }

    public void Init(TrackSegmentConfig config) {
        _config = config;
        _contentImage.sprite = _config.Icon;

        _frameImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _pointerFrame.color = Color.green;
    }

    public void OnPointerClick(PointerEventData eventData) {
        Selected?.Invoke(_config);
        _frameImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _frameImage.gameObject.SetActive(false);
        _pointerFrame.color = Color.white;
    }
}

