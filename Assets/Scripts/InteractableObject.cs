using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class InteractableObject : MonoBehaviour
{
    public UnityEvent hitDetected;
    public UnityEvent hitLost;
    public FloatEvent filling;
    public UnityEvent filled;
    public UnityEvent empty;

    public bool isHit;

    [Header("Private but Serialized")]
    [SerializeField]
    private float maxFill = 3;

    [SerializeField]
    private float clearSpeed = 1;

    [SerializeField]
    private float fillSpeed = 1;

    [Header("Private for Controll")]
    [SerializeField]
    private float fillState = 0;

    public float FillState
    {
        get
        {
            return fillState;
        }
        set
        {
            if (value > maxFill)
            {
                fillState = maxFill;
                isFilling = false;
                filled.Invoke();
            }
            else if (value <= 0)
            {
                fillState = 0;
                empty.Invoke();
            }
            else
            {
                filling.Invoke(fillState / maxFill);
                fillState = value;
            }
        }
    }

    private bool isFilling;

    public void HitDetected()
    {
        isHit = true;
        hitDetected.Invoke();
    }

    public void HitLost()
    {
        hitLost.Invoke();
    }

    public void StartFill()
    {
        if (FillState < maxFill)
            FillState += Time.deltaTime * fillSpeed;
        else
            FillState = maxFill;
    }

    public void StopFill()
    {
        if (FillState > 0)
            FillState -= Time.deltaTime * clearSpeed;
    }

    public void Update()
    {
        if (!isHit)
            hitLost.Invoke();

        if (isHit&&Input.GetButton("Fire1"))
        {
            StartFill();
        }
        else
        {
            StopFill();
        }
        isHit = false;
    }
}