using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Visibility : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (Network.isServer) {
			GetComponent<MeshRenderer> ().material.color = Color.red;
		} else {
			GetComponent<MeshRenderer> ().material.color = Color.green;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
