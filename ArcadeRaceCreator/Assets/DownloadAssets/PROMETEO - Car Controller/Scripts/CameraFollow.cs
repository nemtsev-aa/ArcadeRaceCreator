using System;
using UnityEngine;
using Zenject;

public class CameraFollow : Manager {

	public Transform carTransform;
	[Range(1, 10)] public float followSpeed = 2;
	[Range(1, 10)] public float lookSpeed = 5;

	private Vector3 _initialCameraPosition;
	private Vector3 _initialCarPosition;
	private Vector3 _absoluteInitCameraPosition;

    public override void Activate(bool status) {
        base.Activate(status);

		this.enabled = status;

		if (status) {
			_initialCameraPosition = gameObject.transform.position;
			_initialCarPosition = carTransform.position;
			_absoluteInitCameraPosition = _initialCameraPosition - _initialCarPosition;
		}
	}

    public void SetTarget(Transform cameraAnker) {
		carTransform = cameraAnker;
	}

    private void FixedUpdate() {
		if (IsActive == false)
			return;

		//Look at car
		Vector3 _lookDirection = (new Vector3(carTransform.position.x, carTransform.position.y, carTransform.position.z)) - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);

		//Move to car
		Vector3 _targetPos = _absoluteInitCameraPosition + carTransform.transform.position;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);

		transform.Translate(0f, 0f, Input.mouseScrollDelta.y);
	}
}
