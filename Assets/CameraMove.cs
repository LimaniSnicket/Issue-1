using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public bool talkPhase = true;

	public float maxY = 1f;

	public bool finalBattle;
	public GameObject player;


	public bool textOne = true;
	public bool textTwo = false;
	public bool textThree = false;
	public bool textFour = false;
	public bool textFive = false;
	public bool textSix = false;
	public bool textSeven = false;
	public bool textEight = false;


	public float cameraPan;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;
		if (finalBattle){
		currentPos.y += cameraPan * Time.deltaTime;

		if (textOne == true && (Input.GetKeyUp (KeyCode.Return))) {
			textTwo = true;
			textOne = false;
		} else if (textTwo == true && (Input.GetKeyUp (KeyCode.Return))) {
			textTwo = false;
			textThree = true;
		} else if (textThree == true && (Input.GetKeyUp (KeyCode.Return))){
			textThree = false;
			textFour = true;
		} else if (textFour == true && (Input.GetKeyUp (KeyCode.Return))){
			textFour = false;
			textFive = true;
		} else if (textFive == true && (Input.GetKeyUp (KeyCode.Return))){
			textFive = false;
			textSix = true;
		} else if (textSix == true && (Input.GetKeyUp (KeyCode.Return))){
			textSix = false;
			textSeven = true;
		} else if (textSeven == true && (Input.GetKeyUp (KeyCode.Return))){
			textSeven = false;
			textEight = true;
		} else if (textEight == true && (Input.GetKeyUp (KeyCode.Return))) {
			talkPhase = false;

		}
		}

		if (talkPhase == true && finalBattle == true) {
			currentPos.y = -2.59f;
		} 

		if (talkPhase == false  && currentPos.y < 1 && finalBattle == true){
			currentPos.y = maxY;
		} else if (currentPos.y >= 1){
			currentPos.y = 1f;
		}

		if (finalBattle == false) {
			currentPos = player.transform.position;
		}

		transform.position = new Vector3 (currentPos.x, currentPos.y, -7.2f);//currentPos;
	}
}
