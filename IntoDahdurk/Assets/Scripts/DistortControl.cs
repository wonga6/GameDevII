using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class DistortControl : MonoBehaviour {

	//camera effects
	private MotionBlur blur;
	private Twirl twirl;

	//finding other player
	private Transform otherPlayer = null;

	//distortion controls
	public float safeRadius;
	public float maxDistortDistance;
	public float blurCap;
	public float twirlCap;
	private float distortRange;
	private AudioSource sound;


	// Use this for initialization
	void Start () 
	{
		blur = GetComponent<MotionBlur> ();
		twirl = GetComponent<Twirl> ();

		blur.blurAmount = 0f;
		twirl.angle = 0f;

		distortRange = maxDistortDistance - safeRadius;

		sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//make sure there are two players and reference each other
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		if (players.Length == 2) 
		{
			if (players [0] == transform.parent.gameObject) 
			{
				otherPlayer = players [1].transform;
			}
			else 
			{
				otherPlayer = players [0].transform;
			}
		} 
		else
			otherPlayer = null;

		//Check parameters to apply distort
		if (otherPlayer != null) 
		{
			if (transform.parent.GetComponent<CustomTPController> ().thresholdCrossed
			    && otherPlayer.GetComponent<CustomTPController> ().thresholdCrossed) 
			{
				Debug.Log ("Both have Crossed");
				UpdateDistance ();
			}
		}
	}

	//determine distance between players; apply distortion accordingly
	public void UpdateDistance()
	{
		float distance = (transform.parent.position - otherPlayer.position).magnitude;
		Debug.Log (distance);
		if (distance > safeRadius) 
		{
			float distortAmount = (distance - safeRadius) / distortRange;
			Debug.Log (distortAmount);
			blur.blurAmount = blurCap * distortAmount;
			twirl.angle = twirlCap * distortAmount;

			if (!sound.isPlaying)
				sound.Play ();
		}

		else if (sound.isPlaying)
			sound.Stop();
	}
}
