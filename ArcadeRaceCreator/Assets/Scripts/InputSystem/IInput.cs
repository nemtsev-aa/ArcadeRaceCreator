using System;
using UnityEngine;

public interface IInput {
    event Action<Vector3> StartSwiping;
    event Action<Vector3> ProgressSwiping;

    event Action<bool> SelectorPressed;

    event Action SwipeDown;
    event Action SwipeUp;
    event Action SwipeRight;
    event Action SwipeLeft;

    event Action<Vector3> ClickDown;
    event Action<Vector3> ClickUp;
    event Action<Vector3> Drag;
}
