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
		if (other.gameObject.tag == "Player") {
			AudioSource.PlayClipAtPoint (crossSound, transform.position);
			Instantiate (poof, this.transform);
		}
	}
}
