using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

	public float moveSpeed = 5f;
	public float timeCharged = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;
		Vector3 currentScale = transform.localScale;

		if (Input.GetKey (KeyCode.D)) {
			currentPos.x += moveSpeed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.A)) {
			currentPos.x -= moveSpeed * Time.deltaTime;
		}

		//if (Input.GetKeyDown (KeyCode.LeftShift)) {
		//	moveSpeed = moveSpeed / Time.deltaTime;
		//} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			//moveSpeed = 5f;
		//}


		transform.localScale = currentScale;
		transform.position = currentPos;
		
	}
}
