using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuildMaze : NetworkBehaviour {

	public GameObject wallPrefab;

	public Vector3[] wallPositions;
	public float[] WallRotations;
	// Use this for initialization
	void Start () {
		if (isLocalPlayer) 
		{
			for (int i = 0; i < wallPositions.Length; i++) 
			{
				Instantiate (wallPrefab, wallPositions [i], Quaternion.Euler(0f, WallRotations[i], 0f));
			}
		}
	}

}
