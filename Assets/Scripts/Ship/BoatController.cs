using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour
{
    //Speed calculations
    private float currentSpeed;
    private Vector3 lastPosition;

	public int _cliffdamage = 10;

    void FixedUpdate()
    {
        CalculateSpeed();

        //Debug.Log(currentSpeed);
    }

    //Calculate the current speed in m/s
    private void CalculateSpeed()
    {
        //Calculate the distance of the Transform Object between the fixedupdate calls with 
        //'(transform.position - lastPosition).magnitude' Now you know the 'meter per fixedupdate'
        //Divide this value by Time.deltaTime to get meter per second
        currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;

        //Save the position for the next update
        lastPosition = transform.position;
    }

    public float CurrentSpeed
    {
        get
        {
            return this.currentSpeed;
        }
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "Cliff")
		{
			return;
		}

		for (int i = 0; i < collision.transform.childCount; i++)
		{
			collision.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
			collision.transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
		}

		Destroy(collision.gameObject.GetComponent<Collider>());

		ShipManager.Singleton.TakeDamage(_cliffdamage);
	}
}