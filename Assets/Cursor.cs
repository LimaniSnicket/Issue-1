using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

	public bool infoHover = true;
	public bool playHover = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;

		if (Input.GetKey (KeyCode.RightArrow)) {
			currentPos.x = 3f;
			playHover = true;
			infoHover = false;

		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			currentPos.x = -3f;
			playHover = false;
			infoHover = true;
		}
		transform.position = currentPos;

		if (playHover == true && (Input.GetKeyDown (KeyCode.Return))) {
			Application.LoadLevel ("Page 1");
		} else if (infoHover == true && (Input.GetKeyDown (KeyCode.Return))){
			Application.LoadLevel ("Info Scene");
		}

	}
}
