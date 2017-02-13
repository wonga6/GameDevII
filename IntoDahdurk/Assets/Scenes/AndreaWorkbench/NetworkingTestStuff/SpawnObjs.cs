using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnObjs : NetworkBehaviour {

	// PUBLIC VARIABLES
	public GameObject[] toSpawn;

	// FUNCTIONS

	#region Unity Functions
	public override void OnStartServer () {
		for(int i = 0; i < toSpawn.Length; i++) {
			//GameObject obj = (GameObject)Instantiate(toSpawn[i]); - this line will spawn the object on server side
			NetworkServer.Spawn(toSpawn[i]); // this line spawns object on client side
		}
	}

	// Update is called once per frame
	void Update () {

	}
	#endregion
}
