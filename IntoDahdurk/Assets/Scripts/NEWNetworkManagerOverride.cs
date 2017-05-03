using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Match;


// copied from: https://forum.unity3d.com/threads/using-multiple-player-prefabs-with-the-network-manager.438930/

// custom NetworkManager class to allow multiple player prefabs to be spawned
// spawning of objects that arent' the players' now controlled by SpawnObjs.cs
public class NEWNetworkManagerOverride : NetworkManager {

	// PUBLIC VARIABLES
	public int chosenPrefab = 0;
	public GameObject[] playerPrefabs;
	public Vector3[] playerStartPos;

	public static short msg_launch = 4500;

	// PRIVATE VARIABLES
	int index = 0; // index for which gameobject from plaerPrefabs array to spawn
	bool isReady = false;

	// SUBCLASSES
	//subclass for sending network messages
	/*public class NetworkMessage : MessageBase
	{
		public int chosenClass;
	}*/

	//public struct LaunchMessage
	//{
	//	bool isReady;
	//}


	// FUNCTIONS
	#region Unity Functions

	//When game starts, set max players to 2
	public void Start()
	{
		this.maxConnections = 2;

		foreach(GameObject prefab in playerPrefabs)
			ClientScene.RegisterPrefab (prefab);


	}

	// override network manager's OnServerAddPlayer so that multiple player prefabs can be added
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		Debug.Log ("Instantiating " + index);
		GameObject player;

		Transform startPos = GetStartPosition ();

		//spawn player at start position based on index #
		player = (GameObject)Instantiate(playerPrefabs[index], playerStartPos[index], Quaternion.identity) as GameObject;

		// add player to server
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

		//increment index
		//index++;
	}

	// override network manager's OnClientConnect
	/*public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log ("Client connect");
		//if there are more than 2 connections, cancel this connection
		if (NetworkServer.connections.Count > 2) 
		{
			conn.Disconnect();
			return;
		}


		// create a network message (apparently needed to add a player?)
		NetworkMessage test = new NetworkMessage();
		test.chosenClass = index;
		
		// add player to client scene
		Debug.Log("Adding PLayer");
		ClientScene.AddPlayer(conn, 0, test);

	}*/

	public override void OnServerConnect(NetworkConnection conn)
	{
		Debug.Log ("new player connected");
		if (NetworkServer.connections.Count == 2) 
		{
			Debug.Log ("ready to add players");
			//index = 0;
			//foreach (NetworkConnection nc in NetworkServer.connections) 
			//{
				

			//	index++;
			//}
			// create a message to send to clients
			RpcEnterGame();
		}
	}

	[ClientRpc]
	void RpcEnterGame()
	{
		ClientScene.AddPlayer (0);
	}


	// override OnClientScneneChanged to be empty - else will get message about connection already being set up
	public override void OnClientSceneChanged(NetworkConnection conn)
	{
		//base.OnClientSceneChanged(conn);
	}

	public void setPlayer(int i)
	{
		Debug.Log ("Setting PLayer " + i);
		index = i;
		playerPrefab = playerPrefabs [i];
	}

	#endregion
}
