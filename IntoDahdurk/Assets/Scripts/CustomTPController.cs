using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

//Based on Standard Assets ThirdPersonUSerControl, made network aware
public class CustomTPController : NetworkBehaviour {
	//PUBLIC VARIABLES
	public GameObject cameraPrefab;
	public Transform lookAt;
	public float walkMultiplier;

	//PRIVATE VARIABLES
	CustomTPCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
	private Vector3 otherPos;
	private DistortControl dc;


	private void Start()
	{
		// get the third person character ( this should never be null due to require component )
		m_Character = GetComponent<CustomTPCharacter>();
		if (isLocalPlayer) 
		{
			GameObject camera = (GameObject) Instantiate(cameraPrefab, this.transform.position, Quaternion.identity);
			camera.transform.parent = this.transform;
			m_Cam = camera.transform;
			m_Cam.GetComponent<CustomTPCamera> ().TargetLookAt = lookAt;
			dc = m_Cam.GetComponent<DistortControl> ();
		}
	}


	private void Update()
	{
		if (isLocalPlayer) 
		{
			if (!m_Jump) 
			{
				m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
			}

			//increase distortion based on distance from other player
			if (NetworkServer.connections.Count < 2) 
			{
				dc.UpdateDistance (0f);
			}
		} 
	}

	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
		if (isLocalPlayer) {
			// read inputs
			float h = CrossPlatformInputManager.GetAxis ("Horizontal");
			float v = CrossPlatformInputManager.GetAxis ("Vertical");
			bool crouch = Input.GetKey (KeyCode.C);

			// calculate move direction to pass to character
			if (m_Cam != null) {
				// calculate camera relative direction to move:
				m_CamForward = Vector3.Scale (m_Cam.forward, new Vector3 (1, 0, 1)).normalized;
				m_Move = v * m_CamForward + h * m_Cam.right;
			} else {
				// we use world-relative directions in the case of no main camera
				m_Move = v * Vector3.forward + h * Vector3.right;
			}
			#if !MOBILE_INPUT
			// walk speed multiplier
			if (!Input.GetButton ("Sprint")) 
			{
				m_Move *= walkMultiplier;
			}
			#endif

			// pass all parameters to the character control script
			m_Character.Move (m_Move, crouch, m_Jump);
			m_Jump = false;
		}
	}
}
