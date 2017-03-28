using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : MonoBehaviour {

	public bool levelOne;
	public bool levelTwo;
	public bool levelThree;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D (Collision2D objectHittingMe){ //loading up levels 

		if (objectHittingMe.gameObject.tag == "Player" && levelOne == true) {
			Debug.Log ("cont.");
			Application.LoadLevel ("Page 2");

		} else if(objectHittingMe.gameObject.tag == "bullet" && levelOne == true) {  
			Application.LoadLevel ("Page 2");
			Destroy (objectHittingMe.gameObject);
		}

		if (objectHittingMe.gameObject.tag == "Player" && levelTwo == true) {
			Debug.Log ("cont.");
			Application.LoadLevel ("Page 3");

		} else if(objectHittingMe.gameObject.tag == "bullet" && levelTwo == true) { 
			Application.LoadLevel ("Final Battle");
			Destroy (objectHittingMe.gameObject);
		}

		if (objectHittingMe.gameObject.tag == "Player" && levelThree == true) {
			Debug.Log ("cont.");
			Application.LoadLevel ("Final Battle");

		} else if(objectHittingMe.gameObject.tag == "bullet" && levelThree == true) { 
			Application.LoadLevel ("Final Battle");
			Destroy (objectHittingMe.gameObject);
		}
	}
}

