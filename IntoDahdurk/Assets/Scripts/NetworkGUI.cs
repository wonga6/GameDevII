using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkGUI : MonoBehaviour {
	private NEWNetworkManagerOverride networkManager;
	private MatchMaker matchMaker;
	private GameObject startMenu;
	private GameObject waitForClient;

	void Start()
	{
		networkManager = GetComponent<NEWNetworkManagerOverride> ();
		matchMaker = GetComponent<MatchMaker> ();
	}



	//The following comes from https://www.youtube.com/watch?v=ZsZiHe8yN9A&index=24&list=PLwyZdDTyvucyAeJ_rbu_fbiUtGOVY55BG
	public void setupHost()
	{
		startMenu.SetActive(false);
		waitForClient.SetActive(true);
		string password = CreatePassword ();

		//string hostName = GameObject.Find ("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text> ().text;
		matchMaker.CreateInternetMatch(password);
	}

	public void setupJoinGame()
	{
		Debug.Log ("setupJinGame");
		matchMaker.FindInternetMatch();
	}


	string CreatePassword()
	{
		string password = "";
		for (int i = 0; i < 4; i++) 
		{
			string digit = Random.Range (0, 10).ToString();
			password += digit;
		}
		GameObject.Find("Password").GetComponent<Text>().text = password;
		return password;
	}



	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
		
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex == 0) {
			StartCoroutine (setupStartMenuButtons ());
		} 
		else 
		{
			setupGameSceneButtons ();
		}
	}


	IEnumerator setupStartMenuButtons()
	{
		yield return new WaitForSeconds (0.3f);
		GameObject.Find ("ButtonStartHost").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonStartHost").GetComponent<Button> ().onClick.AddListener (setupHost);

		GameObject.Find ("ButtonJoinGame").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonJoinGame").GetComponent<Button> ().onClick.AddListener (setupJoinGame);

		startMenu = GameObject.Find ("StartMenu");
		waitForClient = GameObject.Find ("WaitForClient");
		waitForClient.SetActive (false);
	}

	void setupGameSceneButtons()
	{
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.AddListener (matchMaker.disconnectPlayer);
	}
}
