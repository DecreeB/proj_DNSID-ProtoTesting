using UnityEngine;
using System.Collections;

public class LineOfSightController : MonoBehaviour {

	public bool isSeen = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D (Collider2D cols){

		Debug.Log ("Enter Trigger");
		if (cols.tag == "Player") {
			isSeen = true;

		}

	}


}
