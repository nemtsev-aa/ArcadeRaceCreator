using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColliderView : MonoBehaviour, IPointerEnterHandler {
    public event Action<PositionTypes> Selected;

    [SerializeField] private PositionTypes _type;
    [SerializeField] private Image _icon;

    private Color _selectedColor = Color.white;
    private Color _deSelectedColor;

    private bool _isActive = false;
    
    private void Start() {
        _deSelectedColor = _icon.color;
    }

    public void Activate(bool status) {
        _isActive = status;
        gameObject.SetActive(status);
        _icon.color = _deSelectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (_isActive == true) {
            Selected?.Invoke(_type);
            _icon.color = _selectedColor;
        }
    }

    //private void OnDrawGizmos() {
    //    // Визуализация луча в редакторе для отладки
    //    Gizmos.color = Color.red;
    //    Vector3 rayOrigin = transform.position + transform.up * 20f;
    //    Vector3 rayDirection = transform.up * 40f;
    //    Gizmos.DrawLine(rayOrigin, rayOrigin + rayDirection);
    //}
}
