using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerController2D : MonoBehaviour {

	//Set Public Variables
	public GameObject gameCamera;
	public float walkSpeed = 10;
	public float gravity = -30;
	public float jumpHeight = 2;

	private CharacterController2D _controller;

	// Use this for initialization
	void Start () {

		_controller = gameObject.GetComponent<CharacterController2D> ();
		gameCamera.GetComponent<CameraFollow2D> ().startCameraFollow (this.gameObject);


	}
	
	// Update is called once per frame
	void Update () {

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
