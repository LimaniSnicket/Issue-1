using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour {

	public float maxY;
	public float minY;

	public float maxX;
	public float minX;

	public bool movingX;
	public bool movingY;

	public float moveSpeed;

	public bool gamePaused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;

		if (gamePaused == false && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = true;
		} else if (gamePaused == true && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = false;
		}

		if (gamePaused == false){

		currentPos.y += moveSpeed * Time.deltaTime;
		if (currentPos.y > maxY && movingY == true) {
			currentPos.y = maxY ;
			moveSpeed = -moveSpeed;
		} else if (currentPos.y < minY && movingY == true) {
			currentPos.y = minY;
			moveSpeed = -moveSpeed;
		}

		transform.position = currentPos;

		}
	}
}
