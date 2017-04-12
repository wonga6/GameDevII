using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour {

	// PUBLIC VARIABLES
	public int numNotes;

	// PRIVATE VARIABLES
	private List<GameObject> players;

	// list of the triggers for notes - whether each player has
	// triggered the note and how many notes to show
	private List<Tuple<bool, bool, int> > triggers;

	private bool show = false; // whether or not to start showing notes
	private int index = 0; // number of the note currently on
	private int count = 0; // the count of how many notes gone through

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		players = new List<GameObject> ();
		triggers = new List<Tuple<bool, bool, int> > ();

		// set all triggers as not having been triggered yet
		for(int i = 0; i < numNotes; i++) {
			Tuple<bool, bool, int> p = new Tuple<bool, bool, int> (false, false, 1);
			triggers.Add (p);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// once both players have triggered a note show the note
		if(show) {
			showNotes ();
			show = false;
		}
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
		// don't go out of index
		if (index > triggers.Count)
			return;

		if(!triggers[index].first) {
			triggers [index].first = true;
		}
		else if(!triggers[index].second) {
			triggers [index].second = true;
		}

		// if both triggers have been set off then show the notes
		if(triggers[index].first && triggers[index].second) {
			show = true;
		}
	}

	// show the notes of the players
	public void showNotes() {
		// don't show notes if one of the players doesn't exist
		if (players.Count != 2)
			return;

		StartCoroutine (showFirstPlayer ());
	}
	#endregion

	#region Private Functions
	// show the first player's notes
	private IEnumerator showFirstPlayer() {
		while (count < triggers [index].third) {
			players [0].GetComponent<NotePassing> ().nextNote ();

			yield return StartCoroutine (showCorresponding ());
		}
	}

	// wait for the first player to finish displaying note
	// before showing note for other player
	private IEnumerator showCorresponding() {
		while(true) {
			if(!players[0].GetComponent<NotePassing>().getNoteShowing()) {
				break;
			}

			yield return new WaitForSeconds (0.1f);
		}

		players [1].GetComponent<NotePassing> ().nextNote ();
		count++;
	}
	#endregion
}
