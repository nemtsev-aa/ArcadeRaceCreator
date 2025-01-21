using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CarsTypeView : UICompanent, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public event Action<CarsTypeView> CarsTypeViewSelected;

    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _descriptionLabel;

    [SerializeField] private Color _selectionColor;
    [SerializeField] private Color _defaultColor;

    public CarClassConfig Config { get; private set; }
    
    public void Init(CarClassConfig config) {
        Config = config;

        UpdateContent();
    }

    public void Activate(bool status) {
        _background.gameObject.SetActive(status);

        if (status == true)
            Select();
        else
            UpdateContent();
    }

    #region IPointerHandler
    public void OnPointerEnter(PointerEventData eventData) {
        _icon.color = _selectionColor;
        ShowText(true);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Select();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_background.gameObject.activeInHierarchy == true) {
            _icon.color = _selectionColor;
            ShowText(false);

            return;
        }

        _icon.color = _defaultColor;

    }
    #endregion

    private void UpdateContent() {
        _icon.sprite = Config.Icon;
        _nameLabel.text = Config.Name;
        _descriptionLabel.text = Config.Description;
    }

    private void Select() {
        _background.gameObject.SetActive(true);
        _icon.color = _selectionColor;

        CarsTypeViewSelected?.Invoke(this);
    }

    private void ShowText(bool status) {
        _nameLabel.gameObject.SetActive(status);
        _descriptionLabel.gameObject.SetActive(status);
    }
}
