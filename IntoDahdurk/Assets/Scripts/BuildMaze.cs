using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuildMaze : MonoBehaviour {

	public GameObject wallPrefab;
	public GameObject invisiWallPrefab;

	public Vector3[] Player1WallPos;
	public float[] Player1WallRots;
	public Vector3[] Player2WallPos;
	public float[] Player2WallRots;

	// Use this for initialization
	void Start () {
		
	}

	//if index=0, populates visible walls at player 1 positions and invisible walls at player 2 positions
	//if index=1, invert it
	public void SetWalls(int index){
		Debug.Log ("Building for" + index);
		if (index == 0) {
			for (int i = 0; i < Player1WallPos.Length; i++) {
				Instantiate (wallPrefab, Player1WallPos [i], Quaternion.Euler (0f, Player1WallRots [i], 0f));
			}
			for (int i = 0; i < Player2WallPos.Length; i++) {
				Instantiate (invisiWallPrefab, Player2WallPos [i], Quaternion.Euler (0f, Player2WallRots [i], 0f));
			}

		} else if (index == 1) {
			for (int i = 0; i < Player2WallPos.Length; i++) {
				Instantiate (wallPrefab, Player2WallPos [i], Quaternion.Euler (0f, Player2WallRots [i], 0f));
			}
			for (int i = 0; i < Player1WallPos.Length; i++) {
				Instantiate (invisiWallPrefab, Player1WallPos [i], Quaternion.Euler (0f, Player1WallRots [i], 0f));
			}
		}
	}

}
