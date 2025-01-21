using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectStagesPanel : UIPanel {
    public event Action<ProjectStageTypes> ProjectStageTypesChanged;

    [SerializeField] private List<ProjectStageView> _views;
    private float _animationDuration = 0.3f;

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _views.Count == 0) {
            AddListeners();
            ShowProjectStageViews();
        }

    }
    public override void AddListeners() {
        base.AddListeners();

        foreach (ProjectStageView iView in _views) {
            iView.ProjectStageViewSelected += OnProjectStageViewSelected;
            iView.transform.localScale = Vector3.zero;
        }
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (ProjectStageView iView in _views) {
            iView.ProjectStageViewSelected -= OnProjectStageViewSelected;
            iView.transform.localScale = Vector3.one;
        }
    }

    private void ShowProjectStageViews() {
        foreach (ProjectStageView iView in _views) {
            iView.transform.DOScale(Vector3.one, _animationDuration).SetDelay(_animationDuration * 1.1f);
        }
    }

    private void OnProjectStageViewSelected(ProjectStageTypes projectStageType) {
        ProjectStageTypesChanged?.Invoke(projectStageType);
    }
}
