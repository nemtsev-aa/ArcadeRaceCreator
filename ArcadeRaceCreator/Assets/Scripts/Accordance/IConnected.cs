using System;
using UnityEngine;

public interface IConnected {
    public event Action<Transform> CompanentSelected;
    public Vector3 ConnectPointPosition { get; }
    public void SetState(AccordanceCompanentState state);
    
}
