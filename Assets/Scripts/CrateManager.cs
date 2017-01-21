using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour {

	public static CrateManager Singleton { get; set; }

	public int _desiredCratesInWater = 10;
	public float _crateSpawnRange = 500f;

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

		// Spawn collected crate on deck
		Instantiate(_deckCratePrefab, _collectedCrateSpawn.position, Quaternion.identity);
	}

    /// <summary>
    /// To be called when a crate floating in the water was collected by the ship
    /// Destroys collecte crate, spawns one on deck and if needed another water crate
    /// </summary>
    /// <param name="collectedCrate">Reference to the collected crate's gameobject</param>
    public void OnCrateScore(GameObject collectedCrate)
    {
        // Delete collected crate
        Destroy(collectedCrate);

        _currentCrateAmount--;

        // Spawn new crate in water if necessary
        if (_currentCrateAmount < _desiredCratesInWater)
        {
            SpawnWaterCrate();
        }

        // score the collected crate
        GameManager.highscore++;

    }
    
    /// <summary>
    /// Spawns another water crate at random position and increases current crate counter
    /// </summary>
    void SpawnWaterCrate()
	{
		Instantiate(_waterCratePrefab, new Vector3(Random.Range(-_crateSpawnRange, _crateSpawnRange), 10f, Random.Range(-_crateSpawnRange, _crateSpawnRange)), Quaternion.identity);
	
		_currentCrateAmount++;
	}
}