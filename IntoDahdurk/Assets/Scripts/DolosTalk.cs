using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DolosTalk : MonoBehaviour {

	// PUBLIC VARIABLES
	public string path;
	public Image speechBackground;
	public Text speechText;

	// PRIVATE VARIABLES
	private int index = 0;
	private List<string> text;

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		text = new List<string> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion

	#region Private Functions
	private void showNote() {
		if(index < text.Count) {
			
		}
	}

	private void visible(bool isVisible) {
		if(isVisible) {
			speechBackground.enabled = true;
			speechText.enabled = true;
		}
		else {
			speechBackground.enabled = false;
			speechText.enabled = false;
		}
	}

	// read in the text file of dolos's speech
	private void readTextFile() {
		StreamReader instream = new StreamReader (path);

		while (!instream.EndOfStream) {
			string line = instream.ReadLine ();
			text.Add (line);
		}

	}
	#endregion
}
