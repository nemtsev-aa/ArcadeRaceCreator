using System;
using UnityEngine;
using Zenject;

public class DesktopInput : ITickable, IInput {
    private const float DeadZone = 100;
    private const int LeftMouseButton = 0;

    public event Action<Vector3> StartSwiping;
    public event Action<Vector3> ProgressSwiping;
    public event Action<bool> SelectorPressed;

    public event Action SwipeDown;
    public event Action SwipeUp;
    public event Action SwipeRight;
    public event Action SwipeLeft;

    private bool _isSwiping;

    private Vector2 _startPosition;
    private Vector3 CurrentMousePosition => Input.mousePosition;

    public void Tick() {

        ProcessClickUp();

        ProcessClickDown();

        ProcessSwipe();
    }

    private void ProcessClickDown() {
        if (Input.GetMouseButtonDown(LeftMouseButton)) {
            _isSwiping = true;
            _startPosition = CurrentMousePosition;

            StartSwiping?.Invoke(_startPosition);
        }
    }

    private void ProcessSwipe() {
        if (_isSwiping == false) {
            SelectorPressed?.Invoke(false);
            return;
        }

        SelectorPressed?.Invoke(true);
        ProgressSwiping?.Invoke(CurrentMousePosition);
    }

    private void ProcessClickUp() {
        if (Input.GetMouseButtonUp(LeftMouseButton))
            _isSwiping = false;
    }
}
