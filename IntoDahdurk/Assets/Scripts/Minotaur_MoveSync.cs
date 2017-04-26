using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// modeled after the MotionSync script from the tutorial:
// https://www.youtube.com/watch?v=9Gbd5hC0j_k

public class Minotaur_MoveSync : NetworkBehaviour {

	// PRIVATE VARIABLES

	// Synced variables
	[SyncVar] private Vector3 syncPosition;
	[SyncVar] private float syncYRotation;

	// not synced variables
	private Vector3 lastPostion;
	private Quaternion lastRotation;

	private Transform myTransform;
	private float lerpRate = 10;
	private float positionThreshold = 0.5f;
	private float rotateThreshold = 5;

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		myTransform = transform;	
	}
	
	// Update is called once per frame
	void Update () {
		transmitMotion ();
		LerpMotion ();
	}
	#endregion

	#region Private Functions
	// store the values of variables
	private void transmitMotion() {
		// return if not on the server
		if (!isServer) {
			return;
		}

		if(Vector3.Distance(myTransform.position, lastPostion) > positionThreshold ||
			Quaternion.Angle(myTransform.rotation, lastRotation) > rotateThreshold) {
			lastPostion = myTransform.position;
			lastRotation = myTransform.rotation;

			syncPosition = myTransform.position;
			syncYRotation = myTransform.localEulerAngles.y;
		}
	}
		
	// interpolation of movement on client
	private void LerpMotion() {
		if(isServer) {
			return;
		}

		myTransform.position = Vector3.Lerp (myTransform.position,
			syncPosition, Time.deltaTime * lerpRate);

		Vector3 newRoation = new Vector3 (0, syncYRotation, 0);
		myTransform.rotation = Quaternion.Lerp (myTransform.rotation,
			Quaternion.Euler (newRoation), Time.deltaTime * lerpRate);
	}
	#endregion
}
