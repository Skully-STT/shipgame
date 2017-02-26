using UnityEngine;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;

public class BoatEngine : MonoBehaviour
{
    public Controller _leftController, _rightController;
    public GameObject _steeringWheel, _leftPaddleWheel, _rightPaddleWheel;
    public float _steeringWheelSpeed = 1f;
    public float _paddleWheelSpeed = 1f;

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
        // Debug.Log("Throttle %=" + (int)(currentJetPower/maxPower*100));

        int Throttle = (int)(currentJetPower / maxPower * 100);
        ShipManager.Singleton.Speed = Throttle;
        UpdateWaterJet();
    }

    void Move(bool forward)
    {
        if (boatController.CurrentSpeed < 50f && currentJetPower < maxPower)
        {
            currentJetPower += powerFactor * (forward ? 1f : -.25f);
        }
    }

    void Steer(bool left)
    {
        print("Steering " + (left ? "left" : "right"));
        float steeringWheelRotZ = 0f;

        if (left)
        {
            WaterJetRotation_Y = waterJetTransform.localEulerAngles.y + 2f;

            if (WaterJetRotation_Y > 30f && WaterJetRotation_Y < 270f)
            {
                WaterJetRotation_Y = 30f;
            }
            else
            {
                steeringWheelRotZ += _steeringWheelSpeed * Time.deltaTime;
            }

            Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);

            waterJetTransform.localEulerAngles = newRotation;
        }
        else
        {
            WaterJetRotation_Y = waterJetTransform.localEulerAngles.y - 2f;

            if (WaterJetRotation_Y < 330f && WaterJetRotation_Y > 90f)
            {
                WaterJetRotation_Y = 330f;
            }
            else
            {
                steeringWheelRotZ -= _steeringWheelSpeed * Time.deltaTime;
            }

            Vector3 newRotation = new Vector3(0f, WaterJetRotation_Y, 0f);

            waterJetTransform.localEulerAngles = newRotation;
        }

        _steeringWheel.transform.rotation = Quaternion.Euler(
            _steeringWheel.transform.rotation.eulerAngles.x,
            _steeringWheel.transform.rotation.eulerAngles.y,
            _steeringWheel.transform.rotation.eulerAngles.z + steeringWheelRotZ);
    }

    void UserInput()
    {
        if (_leftController == null || _rightController == null)
        {
            return;
        }

        if (_leftController.gripButtonPressed || _rightController.gripButtonPressed)
        {
            Move(true);
        }
        
        if (_leftController.gripButtonPressed && !_rightController.gripButtonPressed)
        {
            Steer(true);
        }
        else if (_rightController.gripButtonPressed && !_leftController.gripButtonPressed)
        {
            Steer(false);            
        }
        else if (!_leftController.gripButtonPressed && !_rightController.gripButtonPressed)
        {
            Move(false);
            if (currentJetPower <= powerFactor)
            {
                currentJetPower = 0f;
            }
        }

    }
    float angleBetweenLinesInRad(Vector3 line1Start, Vector3 line1End, Vector3 line2Start, Vector3 line2End)
    {
        float a = line1End.x - line1Start.x;
        float b = line1End.y - line1Start.y;
        float c = line2End.x - line2Start.x;
        float d = line2End.y - line2Start.y;

        float atanA = Mathf.Atan2(a, b);
        float atanB = Mathf.Atan2(c, d);

        return atanA - atanB;
    }

    void UpdateWaterJet()
    {
        //Debug.Log(boatController.CurrentSpeed);

        Vector3 forceToAdd = waterJetTransform.forward * currentJetPower;

        boatRB.AddForceAtPosition(forceToAdd, waterJetTransform.position);

        var a = waterJetTransform.localEulerAngles.y;
        a = a >= 330f ? -(360f - a) : a;

        var l = _paddleWheelSpeed * (a > 0f ? a / 30f : 0f);
        var r = _paddleWheelSpeed * (a <= 0f ? -a / 30f : 0f);

        _rightPaddleWheel.transform.localRotation *=
            Quaternion.Euler(
                (ShipManager.Singleton.Speed + l) * Time.deltaTime,
                0f,
                0f);

        _leftPaddleWheel.transform.localRotation *=
            Quaternion.Euler(
                (ShipManager.Singleton.Speed + r) * Time.deltaTime,
                0f,
                0f);

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