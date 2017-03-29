using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// TODO: add something to take in the actual notes - in process
// TODO: fix it so that note is shown based on a collision trigger, not keyboard press

public class NotePassing : MonoBehaviour {

	// PUBLIC VARIABLES
	public Image noteBackground;
	public Text noteText;

	public float fadeRate; // how fast the node will fade
	public float timeBeforeFade; // how long to wait before fading note

	public string path; // path to the place where the note text file is stored

	// PRIVATE VARIABLES
	private List<string> notes; // the notes of the character
	private int index; // the index of the current note

	private float timeLeft;
	private bool noteShowing;
	private bool fadeNote; // whether or not to fade the note

	private static float EPSILON = 0.01f; // EPSILON 'cause float compairison w/ 0 sucks

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		// get all the notes
		notes = new List<string>();
		index = 0;
		getNotesFromTextFile();

		// set the initial visisbility variables
		timeLeft = timeBeforeFade;
		noteShowing = false;
		fadeNote = false;

		// set initial visibility of the notes
		noteVisibility(0.0f);
	}
	
	// Update is called once per frame
	void Update () {

		// if the note is showing then start the wait and fade out
		if(noteShowing) {
			// count the time before beginning to fade the note
			if(timeLeft < 0f && !fadeNote) {
				fadeNote = true;
			}
			else {
				timeLeft -= Time.deltaTime;
			}

			// start to fade the note
			if(fadeNote) {
				Color noteBackgroundColor = noteBackground.color;
				Color noteTextColor = noteText.color;

				// fade note until alpha channel is 0
				if(noteBackgroundColor.a > 0f && noteTextColor.a > 0f) {
					noteBackgroundColor.a = Mathf.Lerp(noteBackgroundColor.a, 0f, fadeRate*Time.deltaTime);
					noteBackground.color = noteBackgroundColor;

					noteTextColor.a = Mathf.Lerp(noteTextColor.a, 0f, fadeRate*Time.deltaTime);
					noteText.color = noteTextColor;
				}	

				/* DEBUG STATEMENTS FOR FLOAT COMPARISON TO ALMOST ZERO
				Debug.Log("note background: " + noteBackgroundColor.a);
				Debug.Log("text: " + noteTextColor.a);
				Debug.Log("EPSILON " + EPSILON);
				*/

				// once alpha channel is 0, note is no longer showing
				if(noteBackgroundColor.a <= EPSILON && noteTextColor.a <= EPSILON) {
					// reset the visibility variables
					noteShowing = false;
					fadeNote = false;
					timeLeft = timeBeforeFade;

					// increment the index
					index++;
				}
			}

		}

	}
	#endregion

	#region Public Functions
	// show the next note
	public void nextNote() {
		noteVisibility(1.0f);
		noteShowing = true;

		if(index < notes.Count) {
			noteText.text = notes[index];	
		}
	}
	#endregion

	#region Private Functions
	// set the visibility for the note
	private void noteVisibility(float visibility) {
		Color full = noteBackground.color;
		full.a = visibility;
		noteBackground.color = full;

		full = noteText.color;
		full.a = visibility;
		noteText.color = full;
	}

	// read the notes from a file and store them in a list
	private void getNotesFromTextFile() {
		StreamReader instream = new StreamReader(path);

		while(!instream.EndOfStream) {
			string line = instream.ReadLine();
			notes.Add(line);
		}
	}
	#endregion
}
