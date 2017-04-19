using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour {

	public AudioClip crossSound;
	public GameObject poof;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("trigger");
		if (other.gameObject.tag == "Player") {
			Debug.Log ("isPlayer");
			if(crossSound != null)
				AudioSource.PlayClipAtPoint (crossSound, transform.position);
			Instantiate (poof, this.transform, false);
		}
	}
}
