using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ProjectStageTypes {
    EnvironmentTypeSelection,
    CarsTypeSelection,
    EnvironmentEditing,
    CodingMiniGame,
    Gameplay,
    None
}

public enum ProjectStageState {
    Unselect,
    Select,
    TrueVerification,
    FalseVerification,
    None
}

public class ProjectStageView : UICompanent {
    public event Action<ProjectStageTypes> ProjectStageViewSelected;

    [field: SerializeField] public ProjectStageTypes Type { get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private Image _stateIcon;
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private Button _confirmButton;

    [Space(10)]
    [SerializeField] private float _animationDuration = 0.3f;
    [SerializeField] private float _hoverDuration = 0.3f;

    public ProjectStageState CurrentState { get; private set; } = ProjectStageState.None;
    public ProjectStageViewConfig Config { get; private set; }

    private Sequence _emergence;
    private Sequence _disappear;
    private bool _isActive;

    public void Init(ProjectStageViewConfig config) {
        Config = config;

        UpdateCompanents();
        SetState(ProjectStageState.Unselect);

        _confirmButton.onClick.AddListener(ConfirmButton_Click);
    }

    public virtual void SetState(ProjectStageState state) {
        ExitState();
        CurrentState = state;
        EnterState();
    }

    private void ExitState() {
        if (CurrentState == ProjectStageState.None)
            return;

        switch (CurrentState) {
            case ProjectStageState.Select:
                StartDisappearSequence();
                break;

            case ProjectStageState.TrueVerification:

                break;

            case ProjectStageState.FalseVerification:

                break;

            default:
                break;
        }
    }

    private void EnterState() {

        switch (CurrentState) {
            case ProjectStageState.Unselect:
                UpdateCompanents();

                break;

            case ProjectStageState.Select:
                UpdateCompanents();
                StartEmergenceSequence();

                break;

            case ProjectStageState.TrueVerification:
                _stateIcon.sprite = Config.Complite;
                UpdateConfurmButtonVision();

                break;

            case ProjectStageState.FalseVerification:
                _stateIcon.sprite = Config.Fail;
                UpdateConfurmButtonVision();

                break;

            default:
                break;
        }
    }

    private void UpdateCompanents() {
        _icon.sprite = Config.Icon;
        _nameLabel.gameObject.SetActive(false);

        ShowStateIcon();
        UpdateConfurmButtonVision();

        _icon.DOFade(0.5f, _animationDuration);
    }

    private void ConfirmButton_Click() {
        ProjectStageViewSelected?.Invoke(Type);
    }

    private void StartEmergenceSequence() {

        _nameLabel.gameObject.SetActive(true);

        _emergence = DOTween.Sequence();
        _emergence.Append(_nameLabel.DOFade(0f, 0f));
        _emergence.Append(_icon.DOFade(1f, _animationDuration));
        _emergence.Append(_icon.transform.DOLocalMove(_icon.transform.localPosition + Vector3.up * 100f, _animationDuration));
        _emergence.Append(_nameLabel.DOFade(1f, _animationDuration));
        _emergence.OnComplete(() => {
            UpdateConfurmButtonVision();
            _isActive = true;
        });

        _emergence.Play();
    }

    private void StartDisappearSequence() {
        if (_confirmButton.gameObject.activeInHierarchy == true)
            UpdateConfurmButtonVision();

        _disappear = DOTween.Sequence();
        _disappear.Append(_nameLabel.DOFade(0f, _animationDuration));
        _disappear.Append(_icon.transform.DOLocalMove(_icon.transform.localPosition + Vector3.up * -100f, _animationDuration)) ;
        _disappear.Append(_icon.DOFade(0.5f, _animationDuration));
        _disappear.OnComplete(() => {
            UpdateConfurmButtonVision();
            _isActive = false;
        });

        _disappear.Play();
    }

    private void ShowStateIcon() {
        _stateIcon.gameObject.SetActive(true);

        switch (CurrentState) {
            case ProjectStageState.Unselect:
                _stateIcon.sprite = Config.Lock;
                break;

            case ProjectStageState.Select:
                _stateIcon.gameObject.SetActive(false);
                break;

            case ProjectStageState.TrueVerification:
                _stateIcon.sprite = Config.Complite;
                break;

            case ProjectStageState.FalseVerification:
                _stateIcon.sprite = Config.Fail;
                break;

            default:
                break;
        }
    }

    private void UpdateConfurmButtonVision() {

        if (CurrentState == ProjectStageState.Unselect || CurrentState == ProjectStageState.TrueVerification)
            _confirmButton.gameObject.SetActive(false);

        if (CurrentState == ProjectStageState.Select || CurrentState == ProjectStageState.FalseVerification)
            _confirmButton.gameObject.SetActive(true);
    }

    public override void Dispose() {
        base.Dispose();

        _confirmButton.onClick.RemoveListener(ConfirmButton_Click);
    }

}
