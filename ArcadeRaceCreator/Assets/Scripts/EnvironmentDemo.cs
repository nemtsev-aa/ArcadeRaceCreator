using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class EnvironmentDemo : Manager {
    [SerializeField] private List<EnvironmentManager> _managers;
    [SerializeField] private float _animationDuration = 1f;

    private Tween _rotationAnimation;
    public EnvironmentManager CurrentEnvironmentManager { get; private set; }

    public override void Activate(bool status) {
        base.Activate(status);

        if (status == false) {
            if (_rotationAnimation != null)
                _rotationAnimation.Kill();

            transform.DORotate(Vector3.zero, _animationDuration);
        }
    }

    public void ShowEnvironmentByType(EnvironmentTypes type) {
        if (CurrentEnvironmentManager != null && CurrentEnvironmentManager.Type == type)
            return;

        if (CurrentEnvironmentManager != null)
            CurrentEnvironmentManager.gameObject.SetActive(false);

        EnvironmentManager newManager = _managers.FirstOrDefault(e => e.Type == type);
        if (newManager != null) {
            CurrentEnvironmentManager = newManager;
            CurrentEnvironmentManager.gameObject.SetActive(true);
        }

        if (_rotationAnimation != null && _rotationAnimation.IsPlaying() == true) {
            _rotationAnimation.Kill();
            transform.localEulerAngles = Vector3.zero;
        }
            
        _rotationAnimation = transform.DORotate(Vector3.up * 360, _animationDuration, RotateMode.FastBeyond360)
                                      .SetEase(Ease.Linear)
                                      .SetLoops(-1);
    }
}

