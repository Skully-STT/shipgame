using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollector : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag != "Crate")
		{
			return;
		}

		CrateManager.Singleton.OnCrateCollect(collider.gameObject);
	}
}