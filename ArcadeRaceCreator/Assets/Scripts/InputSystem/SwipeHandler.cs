using System;
using UnityEngine;

public enum SwipeDirection {
    Up,
    Down,
    Right,
    Left,
    None
}

public class SwipeHandler : IPause, IDisposable {
    private const float DeadZone = 100f;

    public event Action<Vector3> PointerShow;
    public event Action<Vector3> PointerPositionChanged;
    public event Action<SwipeDirection> SwipeDirectionChanged;

    private readonly IInput _input;
    private bool _isPaused;

    private Vector2 _swipeDelta;
    private Vector2 _startPosition;
    private Vector3 _currentMousePosition;

    public SwipeHandler(IInput input) {
        _input = input;

        _input.StartSwiping += OnStartSwiping;
        _input.ProgressSwiping += OnSwiping;
    }

    public void SetPause(bool isPaused) => _isPaused = isPaused;

    public void Dispose() {
        _input.StartSwiping -= OnStartSwiping;
        _input.ProgressSwiping -= OnSwiping;
    }

    private void OnStartSwiping(Vector3 startPosition) {
        _startPosition = startPosition;

        //Debug.Log($"OnStartSwiping: {_startPosition}");
    }

    private void OnSwiping(Vector3 mousePosition) {
        _currentMousePosition = mousePosition;

        //Debug.Log($"OnSwiping - StartPosition: {_startPosition}");
        //Debug.Log($"OnSwiping - MousePosition: {_currentMousePosition}");

        if (_startPosition != Vector2.zero)
            CheckSwipe();
    }

    private void CheckSwipe() {
        _swipeDelta = Vector2.zero;
        _swipeDelta = (Vector2)_currentMousePosition - (Vector2)_startPosition;

        if (_swipeDelta.magnitude > DeadZone) {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y)) {
                if (_swipeDelta.x > 0)
                    SwipeDirectionChanged?.Invoke(SwipeDirection.Right);
                else
                    SwipeDirectionChanged?.Invoke(SwipeDirection.Left);
            }
            else {
                if (_swipeDelta.y > 0)
                    SwipeDirectionChanged?.Invoke(SwipeDirection.Up);
                else
                    SwipeDirectionChanged?.Invoke(SwipeDirection.Down);
            }

            ResetSwipe();
        }
    }

    private void ResetSwipe() {
        _startPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }
}
