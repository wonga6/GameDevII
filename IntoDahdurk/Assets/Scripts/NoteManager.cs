using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class NoteManager : MonoBehaviour {

	// PUBLIC VARIABLES
	public string path;

	// PRIVATE VARIABLES
	private List<GameObject> players;
	private List<List<Pair<string, string>>> noteGroups;

	private int index = 0; // number of the note currently on
	private int count = 0; // the count of how many notes gone through

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		players = new List<GameObject> ();
		noteGroups = new List<List<Pair<string, string>>> ();

		readNoteFile ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	#endregion

	#region Public Functions
	// add a reference to a player
	public void addPlayer(GameObject player) {
		// only two players should be in the game
		if (players.Count == 2)
			return;

		players.Add (player);
	}

	// set the trigger to true for that note
	public void setTrigger() {
		//StartCoroutine(showNoteGroups());
		showNote();
	}

	public void showNote() {
		for(int i = 0; i < players.Count; i++) {
			if (index < noteGroups.Count) {
				players [i].GetComponent<NotePassing> ().RpcShow (noteGroups [index] [count].first, noteGroups [index] [count].second);
			}
		}
	}

	public void nextNote() {
		if (count < noteGroups [index].Count) {
			count++;
			showNote ();
		}
		else {
			Debug.Log ("reset");
			count = 0;

			if (index < noteGroups.Count - 1) {
				index++;
			}
		}
	}
	#endregion

	#region Private Functions
	private void readNoteFile() {
		StreamReader instream = new StreamReader(path);

		List<Pair<string, string>> group = new List<Pair<string, string>> ();
		Pair<string, string> note = new Pair<string, string> ();

		while(!instream.EndOfStream) {
			string line = instream.ReadLine();

			if(line == "break") {
				noteGroups.Add (group);

				group = new List<Pair<string, string>> ();
				note = new Pair<string, string> ();
			}
			else if(line == "Maia" || line == "Beni") {
				note.first = line;
			}
			else if(line == "") {
				continue;
			}
			else {
				note.second = line;

				group.Add (note);
				note = new Pair<string, string> ();
			}
		}

		/* DEBUGGING PRINT OUT */

		for(int i = 0; i < noteGroups.Count; i++) {
			Debug.Log ("Group: " + i);
			for(int x = 0; x < noteGroups[i].Count; x++) {
				Debug.Log ("  " + noteGroups [i] [x].first + " : " + noteGroups [i] [x].second);
			}
		}


	}
	#endregion
}
