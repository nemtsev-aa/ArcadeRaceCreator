using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ProjectStagesPanel : UIPanel {
    public event Action<ProjectStageTypes> ProjectStageTypesChanged;

    [SerializeField] private RectTransform _viewParent;
    private float _animationDuration = 0.3f;

    private ProjectStageConfigs _configs;
    private UICompanentsFactory _factory;
    private Logger _logger;
    private List<ProjectStageView> _views;
    private ProjectStageView _currentView;

    [Inject]
    public void Construct(ProjectStageConfigs configs, UICompanentsFactory factory, Logger logger) {
        _configs = configs;
        _factory = factory;
        _logger = logger;
    }

    public void Init() {
        AddListeners();

        _views = new List<ProjectStageView>();
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true) {
            if (_views.Count > 0) {
                ShowProjectStageViews();
                return;
            }

            CreateProjectStageViews();
        } 
    }

    public void UpdateProjectStateView(ProjectStageTypes type, bool status) {
        var view = _views.FirstOrDefault(t => t.Config.Type == type);

        if (status)
            view.SetState(ProjectStageState.TrueVerification);
        else
            view.SetState(ProjectStageState.FalseVerification);
    }

    public void ShowProjectStageViewByType(ProjectStageTypes type) {
        if (_views.Count == 0)
            return;

        var view = _views.FirstOrDefault(t => t.Config.Type == type);
        view.SetState(ProjectStageState.Select);

        _currentView = view;
    }

    public void SetVerificationResultFromCurrentView(bool status) {
        if (_currentView == null)
            return;

        if (status)
            _currentView.SetState(ProjectStageState.TrueVerification);
        else
            _currentView.SetState(ProjectStageState.FalseVerification);
    }

    private void CreateProjectStageViews() {
        if (_configs.Configs.Count == 0)
            return;

        foreach (ProjectStageConfig iConfig in _configs.Configs) {

            ProjectStageViewConfig newViewConfig = new ProjectStageViewConfig(iConfig);
            newViewConfig.SetProjectStageStateIcons(_configs.Lock, _configs.Complite, _configs.Fail);

            ProjectStageView newView = _factory.Get<ProjectStageView>(newViewConfig, _viewParent);

            newView.Init(newViewConfig);
            newView.ProjectStageViewSelected += OnProjectStageViewSelected;

            _views.Add(newView);
        }
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (ProjectStageView iView in _views) {
            iView.ProjectStageViewSelected -= OnProjectStageViewSelected;
            iView.transform.localScale = Vector3.one;
        }
    }

    private async UniTask ShowProjectStageViews() {
        if (_views.Count == 0)
            return;

        foreach (ProjectStageView iView in _views) {
            iView.transform.localScale = Vector3.zero;
            iView.transform.DOScale(Vector3.one, _animationDuration);
            
            await UniTask.Delay(10);
        }
    }

    private void OnProjectStageViewSelected(ProjectStageTypes projectStageType) {
        ProjectStageTypesChanged?.Invoke(projectStageType);
    }
}
