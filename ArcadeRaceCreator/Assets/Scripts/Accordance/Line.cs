using System;
using UnityEngine;

public class Line : MonoBehaviour {
    private Material _material;
    private AccordanceCompanentConfig _accordanceCompanentConfig;

    [field: SerializeField] public LineRenderer Renderer { get; private set; }

    public Vector3 StartPoint => Renderer.GetPosition(0);
    public Vector3 EndPoint => Renderer.GetPosition(1);

    public void Init(Vector3 startPointPosition, AccordanceCompanentConfig accordanceCompanentConfig) {
        _accordanceCompanentConfig = accordanceCompanentConfig;

        _material = new Material(Renderer.material);
        Renderer.material = _material;
        Renderer.positionCount = 2;

        StartLine(startPointPosition);
        EndLine(startPointPosition);
        //Debug.Log($"Line: Start {startPointPosition}");
    }

    public void StartLine(Vector3 position) {
        Renderer.SetPosition(0, position);
        SetState(AccordanceCompanentState.Select);
    }

    //public void UpdateLine(Vector2 position) {
    //    Debug.Log($"Line: Update {position}");
    //    Renderer.SetPosition(1, position);
    //}

    public void EndLine(Vector3 position) {
        //Debug.Log($"Line: End {position}");
        Renderer.SetPosition(1, position);
    }

    public void SetState(AccordanceCompanentState state) => _material.color = GetColorByState(state);
    
    private Color GetColorByState(AccordanceCompanentState state) {
        switch (state) {
            case AccordanceCompanentState.Unselect:
                return _accordanceCompanentConfig.DefaultColor;

            case AccordanceCompanentState.Select:
                return _accordanceCompanentConfig.SelectionColor;

            case AccordanceCompanentState.TrueVerification:
                return _accordanceCompanentConfig.TrueVerificationColor;

            case AccordanceCompanentState.FalseVerification:
                return _accordanceCompanentConfig.FalseVerificationColor;

            default:
                throw new ArgumentException($"Invalid AccordanceCompanentState: {state}");
        }
    }
}
