using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

// TODO: add something to take in the actual notes - in process
// TODO: fix it so that note is shown based on a collision trigger, not keyboard press

public class NotePassing : NetworkBehaviour {

	// PUBLIC VARIABLES
	public Image noteBackground;
	public Text noteText;
	public string playerName;

	// PRIVATE VARIABLES
	private GameObject noteManager;

	[SyncVar]
	private string noteMessage = "";

	// FUNCTIONS

	#region Unity Functions
	// called before start
	void Awake() {
		noteManager = GameObject.Find ("NoteManager");
		if (noteManager != null) {
			noteManager.GetComponent<NoteManager> ().addPlayer (gameObject);
		}

		noteText.text = noteMessage;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T) && noteManager != null) {
			noteManager.GetComponent<NoteManager> ().setTrigger ();
		}
	}

	void OnTriggerEnter(Collider col) {
		if(col.tag == "noteTrigger") {
			if(noteManager != null) {
				col.gameObject.SetActive (false);
				noteManager.GetComponent<NoteManager> ().setTrigger ();
			}
		}
	}
	#endregion

	#region Public Functions
	[ClientRpc]
	public void RpcShow(string name, string text) {

		string message = "";
		if(isLocalPlayer && name == playerName) {
			message = "You: " + text;
		}
		else {
			message = name + ": " + text;
		}

		noteText.text = message;
		StartCoroutine (wait ());
	}

	[ClientRpc]
	public void RpcShow2(string name, string text) {
		string message = "";
		if(isLocalPlayer && name == playerName) {
			message = "You: " + text;
		}
		else {
			message = name + ": " + text;
		}

		noteText.text = message;
		StartCoroutine (wait2 ());
	}
	#endregion

	#region Private Functions
	private IEnumerator wait() {
		yield return new WaitForSeconds (4.0f);

		noteText.text = "";
		if(noteManager != null && playerName == "Maia") {
			noteManager.GetComponent<NoteManager> ().nextNote ();
		}
	}

	private IEnumerator wait2() {
		yield return new WaitForSeconds (2.0f);

		noteText.text = "";
		if(noteManager != null && playerName == "Maia") {
			noteManager.GetComponent<NoteManager> ().increment ();
		}
	}
	#endregion
}
