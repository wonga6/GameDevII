using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

// copied from: https://forum.unity3d.com/threads/using-multiple-player-prefabs-with-the-network-manager.438930/

// custom NetworkManager class to allow multiple player prefabs to be spawned
// spawning of objects that arent' the players' now controlled by SpawnObjs.cs
public class NetworkManagerOverride : NetworkManager
{
	// PUBLIC VARIABLES
	public int chosenPrefab = 0;
	public GameObject[] playerPrefabs;
	public Vector3[] playerStartPos;
	public GameObject maze;

	// PRIVATE VARIABLES
	int index = 0; // index for which gameobject from plaerPrefabs array to spawn
	BuildMaze bm;

	// SUBCLASSES
	//subclass for sending network messages
	public class NetworkMessage : MessageBase
	{
		public int chosenClass;
	}

	// FUNCTIONS

	#region Unity Functions

	//When game starts, set max players to 2
	public void Awake()
	{
		this.maxConnections = 2;
		bm = maze.GetComponent<BuildMaze> ();
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
	#endregion
}