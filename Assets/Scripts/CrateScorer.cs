using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScorer : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "CrateOnDeck" && collider.tag != "CrateLowerDeck")
        {
            return;
        }
        
        CrateManager.Singleton.OnCrateScore(collider.gameObject, collider.tag == "CrateOnDeck");
    }
}
