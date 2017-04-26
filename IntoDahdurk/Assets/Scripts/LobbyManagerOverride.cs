using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

// custom NetworkLobbyManager class to allow multiple player prefabs to be spawned
public class LobbyManagerOverride : NetworkLobbyManager {

	// PUBLIC VARIABLES
	public int chosenPrefab = 0;
	public GameObject[] playerPrefabs;
	public Vector3[] playerStartPos;

	// PRIVATE VARIABLES
	int index = 0; // index for which gameobject from plaerPrefabs array to spawn

	// SUBCLASSES
	//subclass for sending network messages
	public class NetworkMessage : MessageBase
	{
		public int chosenClass;
	}

	// Use this for initialization
	void Awake () {
		this.maxConnections = 2;
	}

	// override network manager's OnServerAddPlayer so that multiple player prefabs can be added
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		GameObject player;
		chosenPrefab = index;

		//spawn player at start position based on index #
		player = Instantiate(playerPrefabs[index], playerStartPos[index], Quaternion.identity) as GameObject;
		// add player to server
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

		//increment index
		index++;
	}

	// override network manager's OnClientConnect
	public override void OnClientConnect(NetworkConnection conn)
	{
		//if there are more than 2 connections, cancel this connection
		if (NetworkServer.connections.Count > 2) 
		{
			conn.Disconnect();
			return;
		}

		// create a network message (apparently needed to add a player?)
		NetworkMessage test = new NetworkMessage();
		test.chosenClass = chosenPrefab;

		// add player to client scene
		ClientScene.AddPlayer(conn, 0, test);

	}

	// override OnClientScneneChanged to be empty - else will get message about connection already being set up
	public override void OnClientSceneChanged(NetworkConnection conn)
	{
		//base.OnClientSceneChanged(conn);
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
