using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerDeckManager : MonoBehaviour {

	public static LowerDeckManager Singleton { get; set; }

	public GameObject _cratePrefab;

	public Transform _crateSpawn;

	Rigidbody _upperDeckRigid, _lowerdeckRigid;

	void Awake()
	{
		Singleton = this;

		_lowerdeckRigid = GetComponent<Rigidbody>();
	}

	void Start()
	{
		_upperDeckRigid = ShipManager.Singleton.transform.GetComponent<Rigidbody>();

		_lowerdeckRigid.mass = _upperDeckRigid.mass;
		_lowerdeckRigid.drag = _upperDeckRigid.drag;
		_lowerdeckRigid.angularDrag = _upperDeckRigid.angularDrag;
		_lowerdeckRigid.constraints = _upperDeckRigid.constraints;
	}

	void FixedUpdate()
	{
		// Apply ship movement so physic forces are applied to lower deck (like when ship is crushing)
		_lowerdeckRigid.velocity = _upperDeckRigid.velocity;
		_lowerdeckRigid.angularVelocity = _upperDeckRigid.angularVelocity;
	}

	public void OnUpperDeckCrateScore()
	{
		// The upper deck player has scored a crate

		var c = Instantiate(_cratePrefab, _crateSpawn.position, Quaternion.identity) as GameObject;

		// gameobject.scene cannot be set directly
		c.transform.parent = transform;
		c.transform.parent = null;
	}
}