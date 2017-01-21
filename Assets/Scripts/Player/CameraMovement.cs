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
		rotY = ClampRotationAroundXAxis(rotY, _rotYMin, _rotYMax);
		rotX = ClampRotationAroundYAxis(rotX, _rotXMin, _rotXMax);

		_camera.localRotation = Quaternion.Lerp(_camera.localRotation, rotY, Time.deltaTime * _speedX);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, rotX, Time.deltaTime * _speedY);
		
	}

	Quaternion ClampRotationAroundYAxis(Quaternion q, float minimumY, float maximumY)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);
		angleY = Mathf.Clamp(angleY, minimumY, maximumY);
		q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

		return q;
	}

	Quaternion ClampRotationAroundXAxis(Quaternion q, float minimumX, float maximumX)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
		angleX = Mathf.Clamp(angleX, minimumX, maximumX);
		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}