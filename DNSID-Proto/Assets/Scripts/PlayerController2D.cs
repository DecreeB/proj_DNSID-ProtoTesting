using UnityEngine;
using System.Collections;
using Prime31;

/* 
 * Player State Info
 * 0 = Default
 * 1 = Crouch
 * 2 = Hide
 * 3 = 
 * */

public class PlayerController2D : MonoBehaviour {

	//SET PUBLIC VARIABLES HERE
	public GameObject gameCamera;
	public int gravity = -50;
	public float walkSpeed = 10f;
	public float crouchSpeed = 5f;
	public float jumpHeight = 5f;
	public float crouchJumpHeight = 2f;

	//SET PRIVATE VARIABLES HERE

	//_controller is used for quick access to gameObject.GetComponent<CharacterController2D>
	private CharacterController2D _controller;
	private CharacterController2D _Shit;

	//Sets the player's default state, grid listed above for conveniences
	private int playerState = 0;

	//How visible is the player?
	private float playerVisible = 1f;

	//Stops players from doing things while hidden
	private bool playerHidden = false;
	private bool enableHide = false;

	private BoxCollider2D playerVisibilityScale;



	// Use this for initialization
	void Start () {

		_controller = gameObject.GetComponent<CharacterController2D> ();
		gameCamera.GetComponent<CameraFollow2D> ().startCameraFollow (this.gameObject);

	}
	
	//FixedUpdate is called once per frame
	void FixedUpdate () {

		//SWITCH TO CROUCH STATE
		if (Input.GetKeyDown (KeyCode.LeftControl) && playerHidden == false && _controller.isGrounded) {
			if (playerState != 1) {
				playerState = 1;
				playerVisible = 0.5f;
			} else {
				playerState = 0;
				playerVisible = 1f;
			}
		}


		//SWITCH TO HIDDEN STATE
		if (Input.GetKeyDown (KeyCode.E) && enableHide == true && _controller.isGrounded){
			if (playerState != 2) {
				playerState = 2;
				playerVisible = 0f;
				playerHidden = true;
			} else {
				playerState = 0;
				playerVisible = 1f;
				playerHidden = false;
			}
			Debug.Log ("SWAG");
		}



		//SETS PLAYER STATES
		switch (playerState)
		{
		case 0:
			defaultMove (walkSpeed, jumpHeight);
			break;
		case 1:
			defaultMove (crouchSpeed, crouchJumpHeight);
			break;
		case 2:
			defaultMove (0, 0);
			break;
		}


		//SETS PLAYERVISIBILITY BOX COLLIDER


	}


	//PLAYER MOVEMENT
	void defaultMove (float walkSpeed, float jumpHeight) {

		Vector3 velocity = _controller.velocity;

		velocity.x = 0;

		//Left and Right Movement
		if (Input.GetAxis("Horizontal") > 0) {
			velocity.x = walkSpeed;
		} else if (Input.GetAxis("Horizontal") < 0){
			velocity.x = walkSpeed * -1;
		}

		//Jump Stuff
		if (Input.GetAxis("Jump") > 0 && _controller.isGrounded) {
			velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
		}

		velocity.y += gravity * Time.deltaTime;

		_controller.move (velocity * Time.deltaTime);

	}

	//HIDEABLE OBJECTS STUFF
	void OnTriggerEnter2D (Collider2D cols){

		if (cols.tag == "Hideable Objects") {
			enableHide = true;
		}

	}

	void OnTriggerExit2D (Collider2D cols){

		if (cols.tag == "Hideable Objects") {
			enableHide = false;
		}
	}
}
