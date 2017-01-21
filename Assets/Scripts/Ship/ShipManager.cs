using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
	public static ShipManager Singleton { get; set; }

    public int maxHealth = 100;
    private int shipHealth = 100;
    public int Shiphealth
    {
        get
        {
            return shipHealth;
        }
        set
        {
            shipHealth = value > 0 ? value < maxHealth ? value : maxHealth:0;
            if (shipHealth == 0)
            {
                GameManager.Instance.OnGameOver.Invoke();
            }
        }
    }
    internal static float lostHealth = 100;
    private float lostHealtOld;

    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value > 100 ? 100 : value < 0 ? 0 : value;
        }
    }

    public void TakeDamage(int damage)
    {
        shipHealth =shipHealth- damage;
        StartCoroutine(lostHealthLerp());
    }

    public void Awake()
    {
	    if (Singleton == null)
	    {
		    Singleton = this;
	    }
	    else
	    {
		    throw new System.InvalidOperationException("Cannot create another instance of the 'ShipManager' class");
	    }
    }

    private IEnumerator lostHealthLerp()
    {
        yield return new WaitForSeconds(1f);
        do
        {
            lostHealth-= 0.1f;
            yield return new WaitForEndOfFrame();
        } while (shipHealth <= lostHealth);
        lostHealth = shipHealth;
    }
}
