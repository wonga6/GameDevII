using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class DistortControl : MonoBehaviour {

	private MotionBlur blur;
	private Bloom bloom;
	private VignetteAndChromaticAberration vignette;
	private Vortex vortex;
	private float distance;


	// Use this for initialization
	void Start () {
		distance = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateDistance(float distance)
	{
		
	}
}
