using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuildMaze : NetworkBehaviour {

	public GameObject wallPrefab;
	public GameObject invisWallPrefab;
	public GameObject spawnObject;

	public Vector3[] wallPos;
	public Vector3[] invisWallPos;
	public Vector3 spawnPos;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) 
		{
			for (int i = 0; i < wallPos.Length; i++) 
			{
				Instantiate (wallPrefab, wallPos [i], Quaternion.identity);
			}

			for (int i = 0; i < invisWallPos.Length; i++) 
			{
				Instantiate (invisWallPrefab, invisWallPos [i], Quaternion.identity);
			}

			Instantiate (spawnObject, spawnPos, Quaternion.identity);
		}
	}

}
