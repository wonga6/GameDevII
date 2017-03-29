using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TempPlayerMovement : NetworkBehaviour {

	// PUBLIC VARIABLES
	public NotePassing nP;

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer) {
			return;
		}

		if(Input.GetKeyDown(KeyCode.T)) {
			nP.nextNote();
		}

		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);
	}
	#endregion
}
