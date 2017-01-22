using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float _speedX, _speedY, _rotXMin, _rotXMax, _rotYMin, _rotYMax = 5f;

	Transform _camera;

	void Awake()
	{
		_camera = Camera.main.transform;
	}

	void Update()
	{
		// Cam movement

		// RotY: rotation of camera up/down (not actually y-value of rotation variable)
		// RotX: rotation of player left/right (same with x value)
		var rotY = _camera.localRotation;
		var rotX = transform.localRotation;

		rotY *= Quaternion.Euler(new Vector3(-Input.GetAxis("Mouse Y"), 0f, 0f));
		rotX *= Quaternion.Euler(new Vector3(0f, Input.GetAxis("Mouse X"), 0f));

		// Clamp rotation
		rotY = RotationClamp.ClampAroundAxis(rotY, RotationClamp.Axis.X, _rotYMin, _rotYMax);
		rotX = RotationClamp.ClampAroundAxis(rotX, RotationClamp.Axis.Y, _rotXMin, _rotXMax);

		_camera.localRotation = Quaternion.Lerp(_camera.localRotation, rotY, Time.deltaTime * _speedX);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, rotX, Time.deltaTime * _speedY);
	}
}