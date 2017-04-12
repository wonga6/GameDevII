﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMovement: MonoBehaviour {

	// PUBLIC VARIABLES

	// path follow variables (public)
	public Transform[] path;
	public float pathOffset; // distance ahead to look for target
	public float pathPntRad = 1.0f; // distance from path pt at which
	                                // pt can be considered reached

	public float speed = 5.0f;

	// chasing player variables (public)
	public List<GameObject> players;

	// PRIVATE VARIABLES

	// path following variables (private)
	int currentNode = 0;


	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * Working pathfinding if my implementation doesn't work as expected to
		Vector3 dir = path [currentNode].position - transform.position;

		transform.position += Vector3.Normalize(dir) * Time.deltaTime * speed;

		if(dir.magnitude <= pathPntRad) {
			currentNode++;

			if (currentNode >= path.Length) {
				currentNode = 0;
			}
		}
		*/

		pathFinding ();
	}

	void OnTriggerEnter(Collider col) {
		
	}

	void OnDrawGizmos() {
		for(int i = 0; i < path.Length; i++) {
			if(path[i] != null) {
				Gizmos.DrawSphere(path[i].position, pathPntRad);
			}
		}
	}
	#endregion

	#region Private Functions
	// path finding function
	private void pathFinding() {
		// find current position in relation to nodes of path
		Vector3 currentPosition = path[currentNode].position;

		// offset current position
		currentPosition.x += pathOffset;
		currentPosition.z += pathOffset;

		// find target position - closes path node to offset position
		int targetIndex = findPathNode(currentPosition);

		// seek target position
		Vector3 direction = path[targetIndex].position - transform.position;
		transform.position += Vector3.Normalize (direction) * Time.deltaTime * speed;

		if(direction.magnitude <= pathPntRad) {
			currentNode = targetIndex;
		}
	}

	// find the closest node on the path
	private int findPathNode(Vector3 position) {
		int closestIndex = 0;
		float minDist = float.MaxValue;

		// loop and find closes node to the 
		for(int i = 0; i < path.Length; i++) {
			float dist = Vector3.Distance(path[i].position, position);
			if(dist < minDist && i != currentNode) {
				closestIndex = i;
				minDist = dist;
			}
		}

		return closestIndex;	
	}

	// chase the player function
	private void chasePlayer() {
		// move towards player's position if there's nothing in the way
		// else stop at thing in the way
	}

	// figure out which player to chase based on who's closer
	private int findPlayerToChase() {
		int closestPlayer = 0;
		float minDist = float.MaxValue;

		for(int i = 0; i < players.Count; i++) {
			Vector3 playerPos = players[i].transform.position;
			float dist = Vector3.Distance(playerPos, transform.position);
			if(dist < minDist) {
				closestPlayer = i;
				minDist = dist;
			}
		}

		return closestPlayer;
	}
	#endregion
}
