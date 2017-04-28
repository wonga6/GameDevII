using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class DistortControl : MonoBehaviour {

	public bool thresholdCrossed = false;

	//camera effects
	private MotionBlur blur;
	private Twirl twirl;

	//finding other player
	private Transform otherPlayer = null;
	private bool twoPlayers = false;

	//distortion controls
	public float safeRadius;
	public float maxDistortDistance;
	public float blurCap;
	public float twirlCap;
	private float distance = 0f;
	private float distortRange;


	// Use this for initialization
	void Start () 
	{
		blur = GetComponent<MotionBlur> ();
		twirl = GetComponent<Twirl> ();

		blur.blurAmount = 0f;
		twirl.angle = 0f;

		distortRange = maxDistortDistance - safeRadius;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//make sure there are two players and reference each other
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		if (players.Length == 2) 
		{
			twoPlayers = true;
			if (players [0] == this.gameObject)
			{
				otherPlayer = players [1].transform;
			}
			else
			{
				otherPlayer = players [0].transform;
			}
		} 
		else 
		{
			Debug.Log ("Missing Player");
			twoPlayers = false;
		}

		//Check parameters to apply distort
		if (otherPlayer != null) 
		{
			bool otherThresholdCrossed = otherPlayer.GetComponentInChildren<DistortControl>().thresholdCrossed;
			if (this.thresholdCrossed && otherThresholdCrossed)
				UpdateDistance ();
				
		}
	}

	//determine distance between players; apply distortion accordingly
	public void UpdateDistance()
	{
		float distance = (transform.parent.position - otherPlayer.position).magnitude;
		if (distance > safeRadius) 
		{
			float distortAmount = (distance - safeRadius) / distortRange;
			Debug.Log (distortAmount);
			blur.blurAmount = blurCap * distortAmount;
			twirl.angle = twirlCap * distortAmount;
		}
	}
}
