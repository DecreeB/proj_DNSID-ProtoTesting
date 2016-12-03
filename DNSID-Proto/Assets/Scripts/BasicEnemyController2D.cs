using UnityEngine;
using System.Collections;
using Prime31;

/*
 * ENEMY STATE INFO
 * 
 * 0 = Idle
 * 1 = On Guard
 * 2 = Aware
 * 3 = Chase
 * */

public class BasicEnemyController2D : MonoBehaviour {

	//SET PUBLIC VARIABLES
	public int gravity = -50;
	public float walkSpeed = 10f;
	public float jumpHeight = 5f;
	public float spot1X = -21f;
	public float spot1Y = 0f;
	public float spot2X = 21f;
	public float spot2Y = 0f;
	public int waitTime = 0;
	public float awareWait = 1f;

	//SET PRIVATE VARIABLES
	private CharacterController2D _controller;
	private PolygonCollider2D _lineOfSight;
	private int enemyState = 1;
	public static Vector3 currentRot;

	//enemyDirection 1 = right and -1 = left
	private int enemyDirection = -1;
	private bool isWaiting = false;


	// Use this for initialization
	void Start () {

		_controller = gameObject.GetComponent<CharacterController2D> ();

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.F)) {
			if (enemyState != 1) {
				enemyState = 1;
			} else {
				enemyState = 0;
			}
		}






		//SETS ENEMY STATES
		switch (enemyState)
		{
		case 0:
			defaultMove (0, 0);
			break;
		case 1:
			defaultMove (walkSpeed, jumpHeight);
			break;
		}

	}

	void defaultMove (float walkSpeed, float jumpHeight) {

		Vector3 velocity = _controller.velocity;

		velocity.x = 0;

		velocity.x = walkSpeed * enemyDirection;

		velocity.y += gravity * Time.deltaTime;


		//PATROLLING MOVEMENT
		if (enemyDirection == -1 && this.transform.position.x <= spot1X) {
			enemyState = 0;
			isWaiting = true;
			Invoke ("idleWait", waitTime);
			Invoke ("flipChar", waitTime);
			enemyDirection *= -1;
		} else if (enemyDirection == 1 && this.transform.position.x >= spot2X) {
			enemyState = 0;
			isWaiting = true;
			Invoke ("idleWait", waitTime);
			Invoke ("flipChar", waitTime);
			enemyDirection *= -1;
		}

		_controller.move (velocity * Time.deltaTime);

	}


	void idleWait (){

		enemyState = 1;
		isWaiting = false;

	}


	void flipChar () {

		_lineOfSight = GetComponentInChildren<PolygonCollider2D> () as PolygonCollider2D;
		currentRot.x = _lineOfSight.transform.eulerAngles.x;
		currentRot.y = _lineOfSight.transform.eulerAngles.y;
		currentRot.z = _lineOfSight.transform.eulerAngles.z;
		currentRot.z += + 180;
		_lineOfSight.transform.eulerAngles = currentRot;

	}


	void onTriggerEnter2D (Collider2D cols) {

		if (cols.tag == "Player") {
			aware ();
		}

	}

	void aware(){

		Debug.Log ("Aware");
		Invoke ("Chase", awareWait);

	}

	void chasePlayer(){

		Debug.Log ("Chase");

	}


}
