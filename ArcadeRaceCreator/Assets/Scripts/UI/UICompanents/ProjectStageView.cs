using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ProjectStageTypes {
    EnvironmentTypeSelection,
    CarsTypeSelection,
    EnvironmentEditing,
    CodingMiniGame,
    Gameplay
}


public class ProjectStageView : UICompanent, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public event Action<ProjectStageTypes> ProjectStageViewSelected;
    [field: SerializeField] public ProjectStageTypes Type { get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private float _animationDuration = 0.3f;

    private Sequence _emergence;
    private Sequence _disappear;

    public void Init() {
        UpdateCompanents();

        _confirmButton.onClick.AddListener(ConfirmButton_Click);
    }

    private void UpdateCompanents() {
        _nameLabel.gameObject.SetActive(false);
        ShowConfurmButton(false);

        _icon.DOFade(0.9f, _animationDuration);
    }

    private void ConfirmButton_Click() {
        ProjectStageViewSelected?.Invoke(Type);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (_emergence.IsActive() == true)
            _emergence.Kill();

        _nameLabel.gameObject.SetActive(true);

        _emergence = DOTween.Sequence();
        _emergence.Append(_nameLabel.DOFade(0f, 0f));
        _emergence.Append(_icon.DOFade(1f, _animationDuration));
        _emergence.Append(_icon.transform.DOMoveY(100f, _animationDuration));
        _emergence.Append(_nameLabel.DOFade(1f, 0f));
        _emergence.Append(_icon.transform.DOMoveY(50f, _animationDuration));

    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_confirmButton.gameObject.activeInHierarchy == true)
            ShowConfurmButton(false);

        _disappear = DOTween.Sequence();

        if (_disappear.IsActive() == true)
            _disappear.Kill();

        _disappear = DOTween.Sequence();
        _disappear.Append(_nameLabel.DOFade(0f, 0f));
        _disappear.Append(_icon.transform.DOMoveY(-50f, _animationDuration));
        _disappear.Append(_icon.DOFade(0.9f, _animationDuration));
        _disappear.Append(_icon.transform.DOMoveY(-100f, _animationDuration));

    }

    public void OnPointerClick(PointerEventData eventData) {
        ShowConfurmButton(true);
    }

    private void ShowConfurmButton(bool status) {
        _confirmButton.gameObject.SetActive(status);
    }

    public override void Dispose() {
        base.Dispose();

        _confirmButton.onClick.RemoveListener(ConfirmButton_Click);
    }
}
