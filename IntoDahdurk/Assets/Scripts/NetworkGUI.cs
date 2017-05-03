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
	private GameObject joinGamePassword;
	private GameObject disconnectScreen;

	void Start()
	{
		networkManager = GetComponent<NEWNetworkManagerOverride> ();
		matchMaker = GetComponent<MatchMaker> ();
	}

	void Update()
	{
		if (SceneManager.GetActiveScene ().name != "Lobby" && Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (disconnectScreen.activeInHierarchy)
				disconnectScreen.SetActive (false);
			else
				disconnectScreen.SetActive (true);
		}
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
		startMenu.SetActive(false);
		joinGamePassword.SetActive(true);
		matchMaker.FindInternetMatch();
	}

	public void ApplicationQuit()
	{
		Application.Quit ();
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
		if (scene.name == "Lobby") {
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

		GameObject.Find ("ButtonQuitGame").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonQuitGame").GetComponent<Button> ().onClick.AddListener (ApplicationQuit);

		startMenu = GameObject.Find ("StartMenu");
		startMenu.SetActive (true);
		waitForClient = GameObject.Find ("WaitForClient");
		waitForClient.SetActive (false);
		joinGamePassword = GameObject.Find ("JoinGamePassword");
		joinGamePassword.SetActive (false);

		yield return new WaitForSeconds (0.3f);
	}

	void setupGameSceneButtons()
	{
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.AddListener (matchMaker.disconnectPlayer);
		disconnectScreen = GameObject.Find ("DisconnectScreen");
		disconnectScreen.SetActive (false);
	}
}
