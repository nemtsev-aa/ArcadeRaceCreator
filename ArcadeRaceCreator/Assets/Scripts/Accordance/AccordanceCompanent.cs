using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum AccordanceCompanentState {
    Unselect,
    Select,
    TrueVerification,
    FalseVerification
}

public abstract class AccordanceCompanent : UICompanent, IConnected {
    public event Action<Transform> CompanentSelected;
    public event Action FrameColorChanged;

    [SerializeField] protected TextMeshProUGUI LabelValue;
    [SerializeField] protected Image FrameImage;
    [SerializeField] protected ConnectStatusView ConnectStatus;

    protected UICompanentConfig Config;
    protected Color _frameColor;
    
    public AccordanceCompanentConfig AccordanceCompanentConfig { get; set; }

    public Color FrameColor {
        get { return _frameColor; }
    }

    public Vector3 ConnectPointPosition => ConnectStatus.ConnectPoint;

    [Inject]
    public void Construct(AccordanceCompanentConfig accordanceCompanentConfig) {
        AccordanceCompanentConfig = accordanceCompanentConfig;
    }

    public virtual void Init(UICompanentConfig config) {
        Config = config;

        ConnectStatus.Init(this);
        FillingCompanents();
    }

    public virtual void SetState(AccordanceCompanentState state) {
        _frameColor = GetColorByState(state);
        
        FrameImage.color = _frameColor;
        FrameColorChanged?.Invoke();
    }

    public virtual void FillingCompanents() {
        SetState(AccordanceCompanentState.Unselect);
        
        LabelValue.text = GetLabelValueText();
    }
    
    protected abstract string GetLabelValueText();

    private Color GetColorByState(AccordanceCompanentState state) {
        switch (state) {
            case AccordanceCompanentState.Unselect:
                return AccordanceCompanentConfig.DefaultColor;

            case AccordanceCompanentState.Select:
                return AccordanceCompanentConfig.SelectionColor;

            case AccordanceCompanentState.TrueVerification:
                return AccordanceCompanentConfig.TrueVerificationColor;

            case AccordanceCompanentState.FalseVerification:
                return AccordanceCompanentConfig.FalseVerificationColor;
            
            default:
                throw new ArgumentException($"Invalid AccordanceCompanentState: {state}");
        }
    }
}

