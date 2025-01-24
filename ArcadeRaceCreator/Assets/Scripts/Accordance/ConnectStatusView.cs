using System;
using UnityEngine;
using UnityEngine.UI;

public class ConnectStatusView : MonoBehaviour, IDisposable {
    [SerializeField] private Image _background;
    [SerializeField] private Image _filler;

    private AccordanceCompanent _connectedView;

    private Color _currentFillerColor;
    private Color _defaultFillerColor = Color.white;

    public Vector3 ConnectPoint {
        get {
            Vector3[] corners = new Vector3[4];
            _filler.gameObject.GetComponent<RectTransform>().GetWorldCorners(corners);
            return (corners[0] + corners[2]) / 2;

            //_filler.gameObject.transform.GetPositionAndRotation(out Vector3 position, out Quaternion quaternion);
            //return position;
        } 
    }

    public void Init(AccordanceCompanent connectedView) {
        _connectedView = connectedView;
        
        AddListeners();
        OnFrameColorChanged();
    }

    private void AddListeners() {
        _connectedView.FrameColorChanged += OnFrameColorChanged;
    }

    private void RemoveListeners() {
        _connectedView.FrameColorChanged -= OnFrameColorChanged;
    }

    private void OnFrameColorChanged() {
        _background.color = _connectedView.FrameColor;
        _currentFillerColor = _defaultFillerColor;
        _filler.color = _currentFillerColor;
    }

    public void Dispose() {
        RemoveListeners();
    }
}
