using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MinotaurMovement: MonoBehaviour {

	// PUBLIC VARIABLES

	// path follow variables (public)
	public List<Transform> path;
	public float pathOffset; // distance ahead to look for target
	public float pathPntRad = 1.0f; // distance from path pt at which
	                                // pt can be considered reached

	public float speed = 5.0f;

	// chasing player variables (public)
	public List<GameObject> players;

	// PRIVATE VARIABLES

	// path following variables (private)
	int currentNode = 0;
	bool goBackwards = false;


	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		// get the path to follow
		GameObject pathParent = GameObject.FindGameObjectWithTag ("AI_Path");

		path = pathParent.transform.Cast<Transform> ().ToList ();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * Working pathfinding if my implementation doesn't work as expected to
		Vector3 dir = path [currentNode].position - transform.position;

		transform.position += Vector3.Normalize(dir) * Time.deltaTime * speed;

		if(dir.magnitude <= pathPntRad) {
			currentNode++;

			if (currentNode >= path.Count) {
				currentNode = 0;
			}
		}
		*/

		pathFinding ();
	}

	void OnTriggerEnter(Collider col) {
		
	}

	void OnDrawGizmos() {
		for(int i = 0; i < path.Count; i++) {
			if(path[i] != null) {
				Gizmos.DrawSphere(path[i].position, pathPntRad);
			}
		}
	}
	#endregion

	#region Private Functions
	/* Original Path Finding - might not be what we want for this game
	 * TODO: return later and see if it can be used in the way we want it to
	 * 
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

		transform.rotation = Quaternion.LookRotation (direction.normalized);

		if(direction.magnitude <= pathPntRad) {
			currentNode = targetIndex;
		}
	}

	// find the closest node on the path
	private int findPathNode(Vector3 position) {
		int closestIndex = 0;
		float minDist = float.MaxValue;

		// loop and find closes node to the Minotaur
		for(int i = 0; i < path.Count; i++) {
			float dist = Vector3.Distance(path[i].position, position);
			if(dist < minDist && i != currentNode) {
				closestIndex = i;
				minDist = dist;
			}
		}
		return closestIndex;	
	}
	*/

	private void pathFinding() {
		int targetIndex = currentNode;

		// seek target position
		Vector3 direction = path[targetIndex].position - transform.position;
		transform.position += Vector3.Normalize (direction) * Time.deltaTime * speed;

		transform.rotation = Quaternion.LookRotation (direction.normalized);

		if(direction.magnitude <= pathPntRad) {
			if(!goBackwards) {
				currentNode++;

				if (currentNode == path.Count) {
					currentNode = path.Count - 1;
					goBackwards = true;
				}
			}
			else {
				currentNode--;

				if (currentNode < 0) {
					currentNode = 0;
					goBackwards = false;
				}
			}			
		}
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
