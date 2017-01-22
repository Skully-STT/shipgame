using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScorer : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OnTriggerEnter Crate"+ collider.name);
        if (collider.tag != "CrateOnDeck")
        {
            return;
        }
        
        CrateManager.Singleton.OnCrateScore(collider.gameObject);
    }
}
