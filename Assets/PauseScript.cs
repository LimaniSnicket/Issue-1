using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

	public bool gamePaused = false;

	public bool fireHover = true;
	public bool windHover = false;
	public bool lightHover = false;
	public bool iceHover = false;

	public Sprite[] pauseScreen;
	public SpriteRenderer pauseActive;

	public Sprite[] pauseText;
	public SpriteRenderer activeText;

	// Use this for initialization
	void Start () {

		pauseActive.sprite = pauseScreen [0];
		activeText.sprite = pauseText [0];
		
	}
	
	// Update is called once per frame
	void Update () {

		if (gamePaused == false && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = true;
			activeText.sprite = pauseText [1];
			//Physics.gravity = new Vector3(0, -1.0F, 0);
		} else if (gamePaused == true && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = false;
			activeText.sprite = pauseText [0];
			//Physics.gravity = new Vector3(0, 1.0F, 0);
		}


		//elemental hover 
		if (fireHover == true && (Input.GetKeyUp (KeyCode.RightShift)) && gamePaused == false) {
			windHover = true;
			fireHover = false;
		} else if (windHover == true && (Input.GetKeyUp (KeyCode.RightShift)) && gamePaused == false) {
			windHover = false;
			lightHover = true;
		} else if (lightHover == true && (Input.GetKeyUp (KeyCode.RightShift)) && gamePaused == false) {
			lightHover = false;
			iceHover = true;
		} else if (iceHover == true && (Input.GetKeyUp (KeyCode.RightShift)) && gamePaused == false) {
			fireHover = true;
			iceHover = false;
		}


		if (gamePaused == false) {
			pauseActive.sprite = pauseScreen [0];
		} else if (gamePaused == true && fireHover == true){
			pauseActive.sprite = pauseScreen [1];
		} else if (gamePaused == true && windHover == true){
			pauseActive.sprite = pauseScreen [2];
		} else if (gamePaused == true && lightHover == true){
			pauseActive.sprite = pauseScreen [3];
		} else if (gamePaused == true && iceHover == true){
			pauseActive.sprite = pauseScreen [4];
		}

		
	}
}
