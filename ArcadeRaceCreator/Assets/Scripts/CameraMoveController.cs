using System;
using UnityEngine;

public class CameraMoveController : Manager {
    public event Action Moved;
    public event Action Zoomed;
    public event Action Rotated;

    public Camera RaycastCamera;
    [Tooltip("Дистанция до цели")]
    public float DistanceToTarget = 50f;
    [Space(10)]
    [Tooltip("Перемещать камеру при приближении курсора к краю экрана")]
    public bool MoveWhileEdgeScreen;
    [Tooltip("Ширина зоны для перемещения камеры")]
    public float SideBorderSize = 20f;
    [Tooltip("Скорость камеры")]
    public float MoveSpeed = 10f;
    [Tooltip("Точность камеры")]
    public float MouseSensetivity = 1f;

    private Vector3 _startPoint;
    private Vector3 _cameraStartPosition;
    private Plane _plane;

    private Vector3 _defaultCameraPostion;
     private Vector3  _targetPostion;

    private bool _isTarget;

    public override void Activate(bool status) {
        base.Activate(status);

        if (status == true) {
            _plane = new Plane(Vector3.up, Vector3.zero);
            _defaultCameraPostion = RaycastCamera.transform.position;
            this.enabled = true;
        }
        else 
        {
            SetTargetPosition(_defaultCameraPostion);
            _isTarget = false;
        }    
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        _targetPostion = targetPosition;
    }

    void FixedUpdate() {
        if (IsActive == false)
            return;

        MoveToTarget();
        MoveToRaycastHit();
        MoveToWhileEdgeScreen();
        RotateY();
    }

    private void MoveToTarget() {
        if (_isTarget == true)
            return;

        if (Vector3.Distance(transform.position, _targetPostion) <= DistanceToTarget) {
            _isTarget = true;
            return;
        }

        transform.position = Vector3.Lerp(transform.position, _targetPostion, MoveSpeed * Time.deltaTime);
    }
    
    private void MoveToRaycastHit() {
        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);

        float distance;
        _plane = new Plane(Vector3.up, Vector3.zero);
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance);

        //if (Input.GetMouseButtonDown(2)) {
        //    _startPoint = point;
        //    _cameraStartPosition = transform.position;

        //    Moved?.Invoke();
        //}

        //if (Input.GetMouseButton(2)) {
        //    Vector3 offset = point - _startPoint;
        //    Vector3 newPosition = _cameraStartPosition + offset;

        //    transform.position = newPosition;
        //}

        if (Input.GetAxis("Mouse ScrollWheel") >= 0.1f || Input.GetAxis("Mouse ScrollWheel") < 0f)
            Zoomed?.Invoke();


        transform.Translate(0f, 0f, Input.mouseScrollDelta.y);
        RaycastCamera.transform.Translate(0f, 0f, Input.mouseScrollDelta.y);
    }

    private void RotateY() {

        if (Input.GetMouseButtonDown(1)) {
            Rotated?.Invoke();
        }

        if (Input.GetMouseButton(1)) {
            //float rotateX = Input.GetAxisRaw("Mouse Y") * -MouseSensetivity;
            float rotateY = Input.GetAxis("Mouse X") * MouseSensetivity;

            transform.parent.Rotate(0f, rotateY, 0f);
        }
    }

    private void MoveToWhileEdgeScreen() {
        if (MoveWhileEdgeScreen == false)
            return;

        Vector2 mousePos = Input.mousePosition; // Положение курсора
        mousePos.x /= Screen.width; // Горизонтальное положение курсора относительно ширины окна
        mousePos.y /= Screen.height; // Вертикальное положение курсора относительно высоты окна

        Vector2 delta = mousePos - new Vector2(0.5f, 0.5f); // Изменение положения мыши

        float sideBorder = Mathf.Min(Screen.width, Screen.height) / 20f; //Размер рамки по краям экрана

        float xDist = Screen.width * (0.5f - Mathf.Abs(delta.x)); // Расстояниедо от текущего положения курсора до вертикальных границ экрана
        float yDist = Screen.height * (0.5f - Mathf.Abs(delta.y)); // Расстояние от текущего положения курсора до горизонтильных границ экрана

        if (xDist < sideBorder || yDist < sideBorder) {
            delta = delta.normalized;
            delta *= Mathf.Clamp01(1 - Mathf.Min(xDist, yDist) / sideBorder);

            transform.Translate(delta * MoveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
