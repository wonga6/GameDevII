﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Based on a tutorial on 3DBuzz: https://www.3dbuzz.com/training/view/3rd-person-character-system/simple-character-system
public class CustomFPCamera : MonoBehaviour {
	//PUBLIC VARIABLES
	//camera positioning offset
	public Vector3 camPosition;
	//sensitivity settings
	public float mouseXSensitivity = 5f;
	public float mouseYSensitivity = 5f;
	public float joystickXSensitivity = 5f;
	public float joystickYSensitivity = 5f;
	//for smoothing camera motion
	public float xSmooth = 0.05f;
	public float ySmooth = 0.1f;
	//limits for vertical camera motion
	public float yStart = 0f;
	public float yMax = 80f;
	public float yMin = -40f;
	//public static float deadzone = 0.1f;

	//PRIVATE VARIABLES
	private float mouseX = 0f;
	private float mouseY = 0f;
	private float velX = 0f;
	private float velY = 0f;
	private float velZ = 0f;
	private Vector3 rotation = Vector3.zero;
	private Vector3 desiredRotation = Vector3.zero;


	// Use this for initialization
	void Awake () 
	{
		UpdatePosition ();
	}


	//Called after all other update functions are called
	void FixedUpdate ()
	{
		HandlePlayerInput ();
		CalculateDesiredPosition ();
		UpdatePosition ();
	}


	//Reads mouse/joystick input
	//mouseX - horizontal rotation around player, unbounded
	//mouseY - vertical positioning of camera, bounded in a certain range
	void HandlePlayerInput()
	{
		//get mouse input on click OR joystick input
		if (Input.GetMouseButton(1) && (Mathf.Abs(Input.GetAxis ("Mouse X")) > 0f || Mathf.Abs(Input.GetAxis ("Mouse Y")) >0f)) {
			mouseX += Input.GetAxis ("Mouse X") * mouseXSensitivity;
			mouseY += Input.GetAxis ("Mouse Y") * mouseYSensitivity;
		} else if (Mathf.Abs(Input.GetAxis ("CameraX")) > 0f || Mathf.Abs(Input.GetAxis ("CameraY")) >0f) 
		{
			mouseX += Input.GetAxis ("CameraX") * joystickXSensitivity;
			mouseY += Input.GetAxis ("CameraY") * joystickYSensitivity;
		}
		//clamp mouseY rotation
		mouseY = ClampAngle(mouseY, yMin, yMax);
	}


	//Calculates where the camera will be positioned in 3D space
	void CalculateDesiredPosition()
	{

	}

	//update change in camera position
	void UpdatePosition()
	{
		desiredRotation = new Vector3(-mouseY, mouseX, 0f); //mouseX is horizontal so it spins on the Y-axis; and vice versa
		//interpolate between current position and desired position
		float rotX = Mathf.SmoothDampAngle (rotation.x, desiredRotation.x, ref velX, xSmooth);
		float rotY = Mathf.SmoothDampAngle (rotation.y, desiredRotation.y, ref velY, ySmooth);
		float rotZ = Mathf.SmoothDampAngle (rotation.z, desiredRotation.z, ref velZ, xSmooth);
		//put it together
		rotation = new Vector3(rotX, rotY, rotZ);
		transform.eulerAngles = rotation;
	}


	//Called whenever camera must return to start distance
	public void setStart()
	{
		transform.position = transform.parent.position + camPosition;
		mouseX = 0f;
		mouseY = yStart;
	}

	//keeps rotation input between 360 and -360 degrees
	public float ClampAngle(float angle, float min, float max)
	{
		do {
			if(angle < -360f)
				angle += 360f;
			if(angle > 360f)
				angle -= 360f;
		} while (angle < -360f || angle > 360f);

		return Mathf.Clamp (angle, min, max);
	}
}

