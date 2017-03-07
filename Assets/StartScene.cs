using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Application.LoadLevel ("Page 1");
		}
	
	}
}
