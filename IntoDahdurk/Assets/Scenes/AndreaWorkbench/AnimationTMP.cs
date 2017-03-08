using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTMP : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		anim.Play("Walk");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
