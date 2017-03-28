using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScene : MonoBehaviour {

	public bool infoScreen;
	public bool gameOverOne;
	public bool gameOverTwo;
	public bool gameOverThree;
	public bool gameOverFinal;

	public float countdown = 0;

	public Sprite[] countdownText;
	public SpriteRenderer activeText;

	// Use this for initialization
	void Start () {
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);
		activeText.sprite = countdownText [0];
	}
	
	// Update is called once per frame
	void Update () {
		countdown = countdown + Time.deltaTime;

		if (infoScreen == true && (Input.GetKeyDown (KeyCode.Return))) {
			Application.LoadLevel ("Start");
		}

		if (countdown < 12 && gameOverOne == true && (Input.GetKey (KeyCode.Return))){
			Application.LoadLevel ("Page 1");
		} else if (countdown < 12 && gameOverTwo == true && (Input.GetKey (KeyCode.Return))){
			Application.LoadLevel ("Page 2");
		} else if (countdown < 12 && gameOverThree == true && (Input.GetKey (KeyCode.Return))){
			Application.LoadLevel ("Page 3");
		} else if (countdown < 12 && gameOverFinal == true && (Input.GetKey (KeyCode.Return))){
			Application.LoadLevel ("Final Battle");
		}

		if (countdown < 1 && countdown >= 0) {
			activeText.sprite = countdownText [0];
		} else if (countdown < 2 && countdown >= 1) {
			activeText.sprite = countdownText [1];
		} else if (countdown < 3 && countdown >= 2) {
			activeText.sprite = countdownText [2];
		} else if (countdown < 4 && countdown >= 3) {
			activeText.sprite = countdownText [3];
		} else if (countdown < 5 && countdown >= 4) {
			activeText.sprite = countdownText [4];
		} else if (countdown < 6 && countdown >= 5) {
			activeText.sprite = countdownText [5];
		} else if (countdown < 7 && countdown >= 6) {
			activeText.sprite = countdownText [6];
		} else if (countdown < 8 && countdown >= 7) {
			activeText.sprite = countdownText [7];
		} else if (countdown < 9 && countdown >= 8) {
			activeText.sprite = countdownText [8];
		} else if (countdown < 10 && countdown >= 9) {
			activeText.sprite = countdownText [9];
		} else if (countdown < 11 && countdown >= 10) {
			activeText.sprite = countdownText [10];
		} else if (countdown < 12 && countdown >= 11) {
			activeText.sprite = countdownText [10];
		} else if (countdown >= 12 && gameOverOne == true) {
			Application.LoadLevel ("Start");
		} else if (countdown >= 12 && gameOverTwo == true) {
			Application.LoadLevel ("Start");
		} else if (countdown >= 12 && gameOverThree == true) {
			Application.LoadLevel ("Start");
		} else if (countdown >= 12 && gameOverFinal == true) {
			Application.LoadLevel ("Start");
		}


	
	}
}
