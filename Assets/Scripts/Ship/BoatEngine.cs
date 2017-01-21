using UnityEngine;
using System.Collections;

public class BoatEngine : MonoBehaviour
{
	public GameObject _steeringWheel;
	public float _steeringWheelSpeed = 1f;

    //Drags
    public Transform waterJetTransform;

    //How fast should the engine accelerate?
    public float powerFactor;

    //What's the boat's maximum engine power?
    public float maxPower;

    //The boat's current engine power is public for debugging
    public float currentJetPower;

    private float thrustFromWaterJet = 0f;

    private Rigidbody boatRB;

    private float WaterJetRotation_Y = 0f;

    BoatController boatController;

    void Start()
    {
        boatRB = GetComponent<Rigidbody>();

        boatController = GetComponent<BoatController>();
    }


    void Update()
    {
        UserInput();
    }

    void FixedUpdate()
    {
       // Debug.Log("boatRB.velocity="+ boatRB.velocity);
        UpdateWaterJet();
    }

    void UserInput()
    {
        //Forward / reverse
        if (Input.GetKey(KeyCode.W))
        {
            if (boatController.CurrentSpeed < 50f && currentJetPower < maxPower)
            {
                currentJetPower += 1f * powerFactor;
            }
        }
        else
        {
            if (currentJetPower > powerFactor)
            {
                currentJetPower -= 1f*powerFactor;
            }
            else
            {
                currentJetPower = 0f;
            }
        }

	    float wheelRotZ = 0f;

        //Steer left
        if (Input.GetKey(KeyCode.A))
        {
            WaterJetRotation_Y = waterJetTransform.localEulerAngles.y + 2f;

	        if (WaterJetRotation_Y > 30f && WaterJetRotation_Y < 270f)
	        {
		        WaterJetRotation_Y = 30f;
	        }
	        else
	        {
		        wheelRotZ += _steeringWheelSpeed * Time.deltaTime;
	        }

            Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);

            waterJetTransform.localEulerAngles = newRotation;
        }
        //Steer right
        else if (Input.GetKey(KeyCode.D))
        {
            WaterJetRotation_Y = waterJetTransform.localEulerAngles.y - 2f;

            if (WaterJetRotation_Y < 330f && WaterJetRotation_Y > 90f)
            {
                WaterJetRotation_Y = 330f;
            }
			else
            {
	            wheelRotZ -= _steeringWheelSpeed * Time.deltaTime;
            }

            Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);

            waterJetTransform.localEulerAngles = newRotation;
        }

	    _steeringWheel.transform.rotation = Quaternion.Euler(
		    _steeringWheel.transform.rotation.eulerAngles.x,
		    _steeringWheel.transform.rotation.eulerAngles.y,
		    _steeringWheel.transform.rotation.eulerAngles.z + wheelRotZ);
    }

    void UpdateWaterJet()
    {
        //Debug.Log(boatController.CurrentSpeed);

        Vector3 forceToAdd = waterJetTransform.forward * currentJetPower;

        boatRB.AddForceAtPosition(forceToAdd, waterJetTransform.position);
        /*//Only add the force if the engine is below sea level
        float waveYPos = WaterController.current.GetWaveYPos(waterJetTransform.position, Time.time);

        if (waterJetTransform.position.y < waveYPos)
        {
            boatRB.AddForceAtPosition(forceToAdd, waterJetTransform.position);
        }
        else
        {
            boatRB.AddForceAtPosition(Vector3.zero, waterJetTransform.position);
        }
        */
    }
}