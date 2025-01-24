using System;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public event Action<MultipliersResultView> ResultViewSelected;

    private Camera _playerCamera;

    public Vector2 CurrentPosition => transform.position;

    private void Start() {
        _playerCamera = Camera.main;
    }

    private void Update() {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(-Vector3.forward, Vector3.zero);

        if (plane.Raycast(ray, out float distance)) {
            Vector3 point = ray.GetPoint(distance);

            transform.position = point;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out MultipliersResultView resultView)) {
            ResultViewSelected?.Invoke(resultView);
        }
    }
}
