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

	// PRIVATE VARIABLES
	int index = 0; // index for which gameobject from plaerPrefabs array to spawn

	// SUBCLASSES
	//subclass for sending network messages
	public class NetworkMessage : MessageBase
	{
		public int chosenClass;
	}

	// FUNCTIONS

	#region Unity Functions
	// override network manager's OnServerAddPlayer so that multiple player prefabs can be added
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		GameObject player;
		// set spawn position (currently using Unity's GetStartPosition() as default)
		// TODO: set spawn position possibly based on player (similar to chosenCharacter)?
		Transform startPos = GetStartPosition();

		// determine which player to spawn
		index = (NetworkServer.connections.Count == 1) ? 0 : 1;
		chosenPrefab = index;

		// create player
		if(startPos != null)
		{
			player = Instantiate(playerPrefabs[chosenPrefab], startPos.position,startPos.rotation)as GameObject;
		}
		else
		{
			player = Instantiate(playerPrefabs[chosenPrefab], Vector3.zero, Quaternion.identity) as GameObject;

		}

		// add player to server
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	// override network manager's OnClientConnect
	public override void OnClientConnect(NetworkConnection conn)
	{
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