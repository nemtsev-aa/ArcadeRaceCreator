using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingMenuView : UICompanent, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public event Action<BuildingMenuView> BuildingFunctionTypeSelected;

    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameLabel;

    [SerializeField] private Color _selectionColor;
    [SerializeField] private Color _defaultColor;

    public BuildingMenuViewConfig Config { get; private set; }

    public void Init(BuildingMenuViewConfig config) {
        Config = config;

        UpdateCompanents();
    }

    public void Activate(bool status) {
        if (status == true)
            Select();
        else
            UpdateCompanents();
    }

    private void UpdateCompanents() {
        _background.gameObject.SetActive(false);

        _icon.sprite = Config.Icon;
        _icon.color = _defaultColor;
        _nameLabel.text = Config.Name;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _icon.color = _selectionColor;
        _nameLabel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_background.gameObject.activeInHierarchy == true) {
            _icon.color = _selectionColor;
            _nameLabel.gameObject.SetActive(false);

            return;
        }

        _icon.color = _defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        Select();
    }

    private void Select() {
        _nameLabel.gameObject.SetActive(false);
        _background.gameObject.SetActive(true);
        _icon.color = _selectionColor;

        BuildingFunctionTypeSelected?.Invoke(this);
    }
}