using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public int maxHealth = 100;
    public static int shipHealth = 100;
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
