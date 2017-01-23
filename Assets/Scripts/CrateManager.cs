using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour {

	public static CrateManager Singleton { get; set; }

	public int _desiredCratesInWater = 10;
	public float _crateSpawnRange = 500f;
	public float _minDistanceToShip = 200f;

	public GameObject _waterCratePrefab, _deckCratePrefab;
	public Transform _collectedCrateSpawn;

	int _currentCrateAmount;

	void Awake()
	{
		if (Singleton == null)
		{
			Singleton = this;
		}
		else
		{
			throw new System.InvalidOperationException("Cannot create another instance of the 'CrateManager' class");
		}
	}

	void Start()
	{
		for (int i = 0; i < _desiredCratesInWater; i++)
		{
			SpawnWaterCrate();
		}
	}

	/// <summary>
	/// To be called when a crate floating in the water was collected by the ship
	/// Destroys collecte crate, spawns one on deck and if needed another water crate
	/// </summary>
	/// <param name="collectedCrate">Reference to the collected crate's gameobject</param>
	public void OnCrateCollect(GameObject collectedCrate)
	{
		// Delete collected crate
		Destroy(collectedCrate);

		_currentCrateAmount--;

		// Spawn new crate in water if necessary
		if (_currentCrateAmount < _desiredCratesInWater)
		{
			SpawnWaterCrate();
		}

		// Spawn collected crate on deck with delay
		Invoke("SpawnDeckCrate", 1f);

		// Play sound
		AudioManager.Singleton.PlayEffect(AudioManager.Singleton._crateCollect);
	}

    /// <summary>
    /// To be called when a crate floating in the water was collected by the ship
    /// Destroys collecte crate, spawns one on deck and if needed another water crate
    /// </summary>
    /// <param name="collectedCrate">Reference to the collected crate's gameobject</param>
    public void OnCrateScore(GameObject collectedCrate, bool isCrateOnUpperDeck)
    {
        // Delete collected crate
        Destroy(collectedCrate);
		
        // score the collected crate
        GameManager.highscore++;

		AudioManager.Singleton.PlayEffect(AudioManager.Singleton._crateScore);

	    if (isCrateOnUpperDeck)
	    {
		    LowerDeckManager.Singleton.OnUpperDeckCrateScore();
	    }
    }
    
    /// <summary>
    /// Spawns another water crate at random position and increases current crate counter
    /// </summary>
    void SpawnWaterCrate()
    {
	    var pos = new Vector3(
		    Random.Range(-_crateSpawnRange, _crateSpawnRange),
			    10f,
			    Random.Range(-_crateSpawnRange, _crateSpawnRange));

	    if (Vector3.Distance(pos, ShipManager.Singleton.transform.position) < _minDistanceToShip)
	    {
		    pos = (pos - ShipManager.Singleton.transform.position).normalized * _minDistanceToShip;
	    }

		Instantiate(_waterCratePrefab, pos , Quaternion.identity);
	
		_currentCrateAmount++;
	}

	void SpawnDeckCrate()
	{
		Instantiate(_deckCratePrefab, _collectedCrateSpawn.position, Quaternion.identity);
	}
}