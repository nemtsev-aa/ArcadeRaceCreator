using System;
using UnityEngine;
using UnityEngine.UI;

public class ToolsPanel : MonoBehaviour {
    public event Action RotateLeftClicked;
    public event Action RotateRightClicked;
    public event Action CancelClicked;

    [SerializeField] private Button _rotateLeft;
    [SerializeField] private Button _rotateRight;
    [SerializeField] private Button _rotateCancel;

    private void Start() {
        _rotateLeft.onClick.AddListener(RotateButtonLeft_Clicked);
        _rotateRight.onClick.AddListener(RotateButtonRightClicked);
        _rotateCancel.onClick.AddListener(CancelButtonClicked);
    }

    private void RotateButtonLeft_Clicked() {
        RotateLeftClicked?.Invoke();
    }

    private void RotateButtonRightClicked() {
        RotateRightClicked?.Invoke();
    }

    private void CancelButtonClicked() {
        CancelClicked?.Invoke();
    }
}
