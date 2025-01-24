using System;
using UnityEngine;

public class CameraMove : Manager {
    public Camera RaycastCamera;
    [Tooltip("��������� �� ����")]
    public float DistanceToTarget = 50f;
    [Space(10)]
    [Tooltip("���������� ������ ��� ����������� ������� � ���� ������")]
    public bool MoveWhileEdgeScreen;
    [Tooltip("������ ���� ��� ����������� ������")]
    public float SideBorderSize = 20f;
    [Tooltip("�������� ������")]
    public float MoveSpeed = 10f;

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

    private void Update() {
        if (IsActive == false)
            return;

        MoveToRaycastHit();
    }

    void LateUpdate() {
        if (IsActive == false)
            return;

        MoveToTarget();
        MoveToWhileEdgeScreen();
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

        if (Input.GetMouseButtonDown(2)) {
            _startPoint = point;
            _cameraStartPosition = transform.position;
        }

        if (Input.GetMouseButton(2)) {
            Vector3 offset = point - _startPoint;
            transform.position = _cameraStartPosition - offset;
        }

        transform.Translate(0f, 0f, Input.mouseScrollDelta.y);
        RaycastCamera.transform.Translate(0f, 0f, Input.mouseScrollDelta.y);
    }

    private void MoveToWhileEdgeScreen() {
        if (MoveWhileEdgeScreen == false)
            return;

        Vector2 mousePos = Input.mousePosition; // ��������� �������
        mousePos.x /= Screen.width; // �������������� ��������� ������� ������������ ������ ����
        mousePos.y /= Screen.height; // ������������ ��������� ������� ������������ ������ ����

        Vector2 delta = mousePos - new Vector2(0.5f, 0.5f); // ��������� ��������� ����

        float sideBorder = Mathf.Min(Screen.width, Screen.height) / 20f; //������ ����� �� ����� ������

        float xDist = Screen.width * (0.5f - Mathf.Abs(delta.x)); // ������������ �� �������� ��������� ������� �� ������������ ������ ������
        float yDist = Screen.height * (0.5f - Mathf.Abs(delta.y)); // ���������� �� �������� ��������� ������� �� �������������� ������ ������

        if (xDist < sideBorder || yDist < sideBorder) {
            delta = delta.normalized;
            delta *= Mathf.Clamp01(1 - Mathf.Min(xDist, yDist) / sideBorder);

            transform.Translate(delta * MoveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
