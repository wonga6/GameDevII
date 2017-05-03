using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

public class MatchMaker : MonoBehaviour {

	private NEWNetworkManagerOverride networkManager;
	private MatchInfo matchInfo;

	void Start()
	{
		networkManager = GetComponent<NEWNetworkManagerOverride> ();
		networkManager.StartMatchMaker ();
	}


	void Update()
	{
		if(Input.GetKeyDown("m"))
			networkManager.matchMaker.ListMatches(0, 10, "", true, 0, 0, CountMatches);
	}

	void CountMatches(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		Debug.Log ("here");
		if(success)
		{
			if (matches.Count != 0)
			{
				Debug.Log ("List was returned");
				//join last server(Temporary)
				Debug.Log("Open Matches = " + matches.Count);
			}
			else
			{
				Debug.Log ("No matches in room!");
			}
		}
		else
		{
			Debug.LogError ("Could not connect to matchmaker");
		}
	}



	//sets up match with host's info
	public void CreateInternetMatch(string hostName)
	{
		networkManager.matchMaker.CreateMatch (hostName, 2, true, "", "", "", 0, 0, OnInternetMatchCreate);
	}

	void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo MI)
	{
		if (success) {
			Debug.Log ("Match Created");
			MatchInfo hostInfo = MI;
			NetworkServer.Listen (hostInfo, 9000);
			matchInfo = MI;
			networkManager.setPlayer (0);
			networkManager.StartHost (hostInfo);
		} else 
		{
			Debug.LogError ("failed to create match");
		}
	}


	//finds a match for client
	public void FindInternetMatch()
	{
		networkManager.matchMaker.ListMatches (0, 10, "", true, 0, 0, OnInternetMatchList);
	}

	void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if(success)
		{
			if (matches.Count != 0)
			{
				Debug.Log ("List was returned");
				//join last server(Temporary)
				networkManager.matchMaker.JoinMatch(matches[matches.Count-1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
			}
			else
			{
				Debug.Log ("No matches in room!");
			}
		}
		else
		{
			Debug.LogError ("Could not connect to matchmaker");
		}
	}


	//sets up client to host's instance
	void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo MI)
	{
		if (success) 
		{
			Debug.Log ("Able to Join Match");
			MatchInfo hostInfo = MI;
			matchInfo = MI;
			networkManager.setPlayer (1);
			networkManager.StartClient (hostInfo);
		} 
		else 
		{
			Debug.LogError ("Match Join Failed");
		}
	}


	public void disconnectPlayer(){
		networkManager.matchMaker.SetMatchAttributes (matchInfo.networkId, false, matchInfo.domain, OnAttributesStopHost);
	}

	void OnAttributesStopHost(bool success, string extendedInfo)
	{
		/*if (success) 
		{
			Debug.Log ("Dropping Match");
			networkManager.StopHost ();

		} 
		else 
		{
			Debug.LogError ("Failed to unlist");
		}*/

		//networkManager.StopHost ();
		//Destroy (this.gameObject);
		networkManager.matchMaker.DropConnection (matchInfo.networkId , matchInfo.nodeId, matchInfo.domain, OnDropConnection);
	}

	void OnDropConnection(bool success, string extendedInfo)
	{
		//networkManager.dontDestroyOnLoad = false;
		SceneManager.LoadScene("Lobby");
	}
}
