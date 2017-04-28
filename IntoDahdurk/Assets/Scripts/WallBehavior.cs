using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour {

	public GameObject poof;
	public float poofLifetime;
	private AudioSource sound;

	private GameObject curPoof;
	private float timer = 0f;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> ();
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
			sound.Play ();
			curPoof = (GameObject)Instantiate (poof, this.transform, false) as GameObject;
			timer = 0f;
		}
	}
}
