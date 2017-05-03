using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ToGameOver : NetworkBehaviour {

	// PRIVATE VARIABLES
	private GameObject teleportPoint;

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		teleportPoint = GameObject.Find ("TeleportPoint");
	}
	
	// Update is called once per frame
	void Update () {
		// keyboard shortcuts for testing and easy showing
/*		if(Input.GetKeyDown(KeyCode.E)) {
			NetworkManager.singleton.ServerChangeScene ("EndGame");
		}

		if (Input.GetKeyDown (KeyCode.O)) {
			NetworkManager.singleton.ServerChangeScene ("GameOver");
		}
		*/

	}
	#endregion

	#region Public Functions
	public void endGame() {
		NetworkManager.singleton.ServerChangeScene ("EndGame");
	}

	public void gameOver() {
		NetworkManager.singleton.ServerChangeScene ("GameOver");
	}
	#endregion
}
