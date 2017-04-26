using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour {

	public AudioClip crossSound;
	public GameObject poof;
	public float poofLifetime;

	private GameObject curPoof;
	private float timer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (curPoof != null) {
			timer += Time.deltaTime;
			if (timer >= poofLifetime) {
				Destroy (curPoof);
				curPoof = null;
			}
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("trigger");
		if (other.gameObject.tag == "Player") {
			Debug.Log ("isPlayer");
			if(crossSound != null)
				AudioSource.PlayClipAtPoint (crossSound, transform.position);
			curPoof = (GameObject)Instantiate (poof, this.transform, false) as GameObject;
			timer = 0f;
		}
	}
}
