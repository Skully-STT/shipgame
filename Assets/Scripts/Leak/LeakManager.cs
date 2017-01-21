using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject leak;
    public GameObject[] leakPoints;
    public List<Leak> spawendLeaks;

    public void Start()
    {
        foreach (GameObject _leak in leakPoints)
        {
            Instantiate(leak, _leak.transform.position, leak.transform.rotation,ship.transform);
        }
    }

    public void Update()
    {

    }

}
