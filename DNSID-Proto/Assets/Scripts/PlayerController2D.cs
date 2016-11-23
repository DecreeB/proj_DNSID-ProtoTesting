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

	//Sets the player's default state, grid listed above for conveniences
	private int playerState = 0;

	//How visible is the player?
	private int playerVisible = 100;

	//Stops players from doing things while hidden
	private bool playerHidden = false;



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
				playerVisible = 50;
			} else {
				playerState = 0;
				playerVisible = 100;
			}
		}

		//SWITCH TO HIDDEN STATE
		if (Input.GetKeyDown (KeyCode.E) && _controller.isGrounded){
			if (playerState != 2) {
				playerState = 2;
				playerVisible = 0;
				playerHidden = true;
			} else {
				playerState = 0;
				playerVisible = 100;
				playerHidden = false;
			}
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

			break;
		}

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

		//Friction
		//velocity.x *= 0.10f;

		velocity.y += gravity * Time.deltaTime;

		_controller.move (velocity * Time.deltaTime);

	}

}
