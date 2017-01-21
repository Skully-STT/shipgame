using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffManager : MonoBehaviour {

	public static CliffManager Singleton { get; set; }

	public int _desiredCliffsInWater = 50;
	public float _cliffSpawnRange = 500f;
	public float _cliffPosY = 10f;
	public int _cliffDamage = 10;

	public GameObject[] _cliffPrefabPool;

	int _currentCliffAmount;

	void Awake()
	{
		if (Singleton == null)
		{
			Singleton = this;
		}
		else
		{
			throw new System.InvalidOperationException("Cannot create another instance of the 'CliffManager' class");
		}
	}

	void Start()
	{
		for (int i = 0; i < _desiredCliffsInWater; i++)
		{
			SpawnCliff();
		}
	}

	/// <summary>
	/// To be called when the ship collides with a cliff
	/// simultes explosion using rigidbodies, destrys cliff, deals damage to ship and plays sound.
	/// Spawns new cliff if needed
	/// </summary>
	/// <param name="destroyedCliff">Reference to the destroyed cliff's gameobject</param>
	public void OnCliffDestroy(GameObject destroyedCliff)
	{
		for (int i = 0; i < destroyedCliff.transform.childCount; i++)
		{
			destroyedCliff.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
			destroyedCliff.transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
		}

		Destroy(destroyedCliff.GetComponent<Collider>());

		ShipManager.Singleton.TakeDamage(_cliffDamage);

		// Delete cliff
		Destroy(destroyedCliff, 3f);

		_currentCliffAmount--;

		// Spawn new cliff in water if necessary
		if (_currentCliffAmount < _desiredCliffsInWater)
		{
			SpawnCliff();
		}

		// Play sound
		AudioManager.Singleton.PlayEffect(AudioManager.Singleton._shipCrash);
	}
    
    /// <summary>
    /// Spawns another water crate at random position and increases current crate counter
    /// </summary>
    void SpawnCliff()
	{
	    Instantiate(
		    _cliffPrefabPool[Random.Range(0, _cliffPrefabPool.Length)],
		    new Vector3(
			    Random.Range(-_cliffSpawnRange, _cliffSpawnRange),
					_cliffPosY,
				    Random.Range(-_cliffSpawnRange, _cliffSpawnRange)),
		    Quaternion.identity);

		_currentCliffAmount++;
	}
}