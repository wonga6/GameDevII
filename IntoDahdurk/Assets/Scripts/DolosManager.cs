using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DolosManager: NetworkBehaviour {

	// PUBLIC VARIABLES
	public string path;

	// PRIVATE VARIABLES
	private int index = 0;
	private List<string> text;
	private List<GameObject> players;

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		text = new List<string> ();
		players = new List<GameObject> ();

		readTextFile ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion

	#region Public Functions
	public void showSpeech() {
		if (index >= text.Count) {
			return;
		}

		for(int i = 0; i < players.Count; i++) {
			players [i].GetComponent<DolosNotes> ().RpcShowNote (text [index]);
		}

	}

	public void increment() {
		index++;
	}

	public void addPlayer(GameObject player) {
		players.Add (player);
	}
	#endregion

	#region Private Functions
	// read in the text file of dolos's speech
	private void readTextFile() {
		StreamReader instream = new StreamReader (path);

		while (!instream.EndOfStream) {
			string line = instream.ReadLine ();
			text.Add (line);
		}


		/* DEBUGGING INPUT */
		for(int i = 0; i < text.Count; i++) {
			Debug.Log (text [i]);
		}
		Debug.Log ("======");
	}
	#endregion
}
