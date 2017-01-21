using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private static ShipManager instance;

    public static ShipManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ShipManager();
            }
            return instance;
        }
    }

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

    public void TakeDamage(int damage)
    {
        shipHealth =shipHealth- damage;
        StartCoroutine(lostHealthLerp());
    }

    public void Update()
    {
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
