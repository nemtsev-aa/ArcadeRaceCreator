using System;
using UnityEngine;

public class AnimatorEventsHandler : MonoBehaviour {
    public event Action DriftStarted;
    public event Action DriftProgressed;
    public event Action Drifted;

    public event Action StrikStarted;
    public event Action Striked;

    public void DriftStart() => DriftStarted?.Invoke();
    
    public void DriftProgress() => DriftProgressed?.Invoke();
    
    public void DriftComplited() => Drifted?.Invoke();
    
    public void StrikeStart() => StrikStarted?.Invoke();
    
    public void StrikeComplited() => Striked?.Invoke();
}
