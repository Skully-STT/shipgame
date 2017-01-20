using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float _speed;

	Rigidbody _rigid;

	void Awake()
	{
		_rigid = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		_rigid.MovePosition(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * _speed * Time.deltaTime);
	}
}
