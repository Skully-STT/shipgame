using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float _speed, _lookSpeed;

    Rigidbody _rigid;

    void Awake()
	{
		_rigid = GetComponent<Rigidbody>();
	}

    void Update()
    {
        // Cam movement 
        var r = transform.rotation;
        var r2 = Camera.main.transform.rotation;

        r *= Quaternion.Euler(new Vector3(0f, Input.GetAxis("Mouse X"), 0f));
        r2 *= Quaternion.Euler(new Vector3(-Input.GetAxis("Mouse Y"), 0f, 0f));

        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, r2, Time.deltaTime * _lookSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, r, Time.deltaTime * _lookSpeed);
    }


    void FixedUpdate()
    {
        // Movement 
        var v = transform.forward * Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        var x = transform.right * Input.GetAxis("Horizontal") * _speed * Time.deltaTime;

        _rigid.MovePosition(transform.position + v + x);
    }
}
