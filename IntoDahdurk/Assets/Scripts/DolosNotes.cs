using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DolosNotes : NetworkBehaviour {

	// PUBLIC VARIABLES
	public GameObject dolosManager;
	public Image background;
	public Text dolosTalk;
	public string playerName;

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		dolosManager = GameObject.Find ("DolosManager");
		if(dolosManager != null) {
			dolosManager.GetComponent<DolosManager> ().addPlayer (gameObject);
		}

		visibility (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Y) && dolosManager != null) {
			dolosManager.GetComponent<DolosManager> ().showSpeech ();
		}
	}

	void OnTriggerEnter(Collider col) {
		if(col.tag == "dolosTrigger" && dolosManager != null) {
			dolosManager.GetComponent<DolosManager> ().showSpeech ();
		}
	}
	#endregion

	#region Public Functions
	[ClientRpc]
	public void RpcShowNote(string message) {
		visibility (true);
		dolosTalk.text = message;

		StartCoroutine (waitToHide ());
	}
	#endregion

	#region Private Functions
	private IEnumerator waitToHide() {
		yield return new WaitForSeconds (4.0f);

		visibility (false);
		if(dolosManager != null && playerName == "Maia") {
			dolosManager.GetComponent<DolosManager> ().increment ();
		}
	}

	private void visibility(bool visible) {
		background.enabled = visible;
		dolosTalk.enabled = visible;
	}
	#endregion
}
