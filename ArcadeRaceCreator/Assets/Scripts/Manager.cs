using System;
using UnityEngine;

public abstract class Manager : MonoBehaviour, IDisposable {
    protected ApplicationManager ApplicationManager { get; private set; }
    protected bool IsActive { get; private set; }

    public virtual void Init(ApplicationManager applicationManager) {
        ApplicationManager = applicationManager;
    }

    public virtual void Activate(bool status) {
        IsActive = status;
    }

    public virtual void Dispose() {
        
    }
}

