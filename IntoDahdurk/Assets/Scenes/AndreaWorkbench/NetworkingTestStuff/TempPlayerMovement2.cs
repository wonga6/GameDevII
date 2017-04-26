using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement2 : MonoBehaviour {

	// PUBLIC VARIABLES

	Animator anim;

	// FUNCTIONS

	#region Unity Functions
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.Play("Idle");
	}
	
	// Update is called once per frame
	void Update () {

		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);
	}
	#endregion
}
