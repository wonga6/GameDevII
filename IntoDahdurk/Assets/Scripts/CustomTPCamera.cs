using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Based on a tutorial on 3DBuzz: https://www.3dbuzz.com/training/view/3rd-person-character-system/simple-character-system
public class CustomTPCamera : MonoBehaviour {
	//PUBLIC VARIABLES
	public Transform TargetLookAt; // initially set to body mesh of character
	public float distance = 5f;
	public float mouseXSensitivity = 5f;
	public float mouseYSensitivity = 5f;
	public float xSmooth = 0.05f;
	public float ySmooth = 0.1f;
	public float yMax = 80f;
	public float yMin = -40f;
	public static float deadzone = 0.1f;

	//PRIVATE VARIABLES
	private float mouseX = 0f;
	private float mouseY = 0f;
	private float velX = 0f;
	private float velY = 0f;
	private float velZ = 0f;
	private Vector3 position = Vector3.zero;
	private Vector3 desiredPosition = Vector3.zero;


	// Use this for initialization
	void Awake () 
	{
		Reset ();
		UpdatePosition ();
		Debug.Log ("created");
	}


	//Called after all other update functions are called
	void LateUpdate ()
	{
		Debug.Log ("Updating");
		//if (isLocalPlayer) {
			Debug.Log ("isclient");
			if (TargetLookAt == null)
				return;

			HandlePlayerInput ();
			CalculateDesiredPosition ();
			UpdatePosition ();
		//}
	}


	//Reads mouse/joystick input
	//mouseX - horizontal rotation around player, unbounded
	//mouseY - vertical positioning of camera, bounded in a certain range
	void HandlePlayerInput()
	{
		//get mouse input
		mouseX += Input.GetAxis("Mouse X") * mouseXSensitivity;
		mouseY += Input.GetAxis ("Mouse Y") * mouseYSensitivity;

		//clamp mouseY rotation
		mouseY = ClampAngle(mouseY, yMin, yMax);
	}


	//Calculates where the camera will be positioned in 3D space
	void CalculateDesiredPosition()
	{
		Vector3 direction = new Vector3 (0f, 0f, distance);
		Quaternion rotation = Quaternion.Euler (mouseY, mouseX, 0f); //mouseX is horizontal so it spins on the Y-axis; and vice versa
		desiredPosition = TargetLookAt.position +rotation * direction;
		
	}

	//update change in camera position
	void UpdatePosition()
	{
		//interpolate between current position and desired position
		float posX = Mathf.SmoothDamp (position.x, desiredPosition.x, ref velX, xSmooth);
		float posY = Mathf.SmoothDamp (position.y, desiredPosition.y, ref velY, ySmooth);
		float posZ = Mathf.SmoothDamp (position.z, desiredPosition.z, ref velZ, xSmooth);
		//put it together
		position = new Vector3 (posX, posY, posZ);
		transform.position = position;

		//set camera to realign with TargetLookAt
		transform.LookAt (TargetLookAt);
	}


	//Called whenever camera must return to start distance
	public void Reset()
	{
		mouseX = 0f;
		mouseY = 10f;
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

