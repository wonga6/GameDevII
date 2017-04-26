using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameOver : NetworkBehaviour {

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) {
			//SceneManager.LoadScene ("AndreaTest3");
			NetworkServer.Reset();
			SceneManager.LoadScene ("AndreaTest3");
		}
	}
	#endregion
}
