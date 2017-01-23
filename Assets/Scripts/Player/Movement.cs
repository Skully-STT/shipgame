using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float _speed;

    Rigidbody _rigid;
	CameraMovement _camMovement;

	GameObject _carriedCrate;
	bool _hasCrate;
	Vector3 _carriedCratePosFromPlayer;

    void Awake()
	{
		_rigid = GetComponent<Rigidbody>();
	    _camMovement = GetComponent<CameraMovement>();
	}

	void Update()
	{
		// Pick up and drop crates
		if (Input.GetKeyDown(KeyCode.JoystickButton0))
		{
			if (!_hasCrate)
			{
				RaycastHit hit;
				if (Physics.Raycast(transform.position, _camMovement._camera.forward, out hit))
				{
					if (hit.collider.tag == "CrateLowerDeck")
					{
						_carriedCrate = hit.collider.gameObject;

						var r = _carriedCrate.GetComponent<Rigidbody>();
						if (r != null)
						{
							r.Sleep();
						}

						_carriedCrate.transform.parent = transform;
						_carriedCratePosFromPlayer = _carriedCrate.transform.localPosition;

						_hasCrate = true;
					}
				}
			}
			else
			{
				var r = _carriedCrate.GetComponent<Rigidbody>();

				if (r != null)
				{
					r.WakeUp();
				}

				_carriedCrate.transform.parent = null;

				_carriedCrate = null;

				_hasCrate = false;
			}
		}

		if (_hasCrate)
		{
			_carriedCrate.transform.localPosition = _carriedCratePosFromPlayer;
		}
	}

	void FixedUpdate()
	{
		// Movement
		var v = transform.forward * Input.GetAxis("GamePadLeftY") * _speed * Time.deltaTime;

		var x = transform.right * Input.GetAxis("GamePadLeftX") * _speed * Time.deltaTime;

		_rigid.MovePosition(transform.position + v + x);
	}
}
