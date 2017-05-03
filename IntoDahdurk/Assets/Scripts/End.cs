using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class End : NetworkBehaviour {

	// PRIVATE VARIABLES
	private GameObject teleportPoint;
	private GameObject gameOver;

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		teleportPoint = GameObject.Find ("TeleportPoint");	
		gameOver = GameObject.Find ("EndGameManager");
	}
	
	// Update is called once per frame
	void Update () {
		// keyboard shortcut is for easy testing and showing purposes
		// ideally should be removed when no longer needed
		if(Input.GetKeyDown(KeyCode.E)) {
			Vector3 newPosition = new Vector3 (teleportPoint.transform.position.x, 
				transform.position.y, teleportPoint.transform.position.z);
			transform.position = newPosition;
		}
	}

	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "fake" && gameOver != null) {
			gameOver.GetComponent<ToGameOver> ().gameOver ();
		}
		else if(col.gameObject.tag == "tablet" && gameOver != null) {
			gameOver.GetComponent<ToGameOver> ().endGame ();
		}
	}
	#endregion
}
