using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnObjs : NetworkBehaviour {

	// PUBLIC VARIABLES
	public GameObject[] toSpawn;

	// FUNCTIONS

	#region Unity Functions
	// override OnStartServer to spawn the object on the non-host client only
	public override void OnStartServer () {
		for(int i = 0; i < toSpawn.Length; i++) {
			//GameObject obj = (GameObject)Instantiate(toSpawn[i]); - this line will spawn the object on server side
			NetworkServer.Spawn(toSpawn[i]); // this line spawns object on client side
		}
	}

	// override OnStartClient to register the objects to spawn with the client
	public override void OnStartClient() {
		for(int i = 0; i < toSpawn.Length; i++) {
			ClientScene.RegisterPrefab (toSpawn [i]);
		}
	}
	#endregion
}
