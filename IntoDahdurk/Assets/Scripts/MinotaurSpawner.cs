using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinotaurSpawner : NetworkBehaviour {

	// PUBLIC VARIABLES
	public Vector3 startPosition;
	public GameObject minotaur;

	// FUNCTIONS

	#region Unity Functions
	public override void OnStartServer() {
		var enemy = (GameObject)Instantiate (minotaur, transform.position, transform.rotation);
		NetworkServer.Spawn (enemy);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion
}
