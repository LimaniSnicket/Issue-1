using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCam : MonoBehaviour {

	public float maxY;
	public float minY;
	public float maxX;
	public float minX;

	public float camSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;

		if (Input.GetKey (KeyCode.RightArrow) && currentPos.x < maxX) {
			currentPos.x += camSpeed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.LeftArrow) && currentPos.x > minX) {
			currentPos.x -= camSpeed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.UpArrow) && currentPos.y < maxY){
			currentPos.y += camSpeed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.DownArrow) && currentPos.y > minY){
			currentPos.y -= camSpeed * Time.deltaTime;
		}

		transform.position = currentPos;

		
	}
}
