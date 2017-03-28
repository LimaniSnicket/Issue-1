using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elementCursor : MonoBehaviour {

	public bool fireHover = true;
	public bool windHover = false;
	public bool lightHover = false;
	public bool iceHover = false;

	public Sprite[] elementText;
	public SpriteRenderer hoverElement;

	public float cursorDistance;


	// Use this for initialization
	void Start () {

		hoverElement.sprite = elementText [0];
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;

		//if (fireHover) {
			//currentPos.x = -10f;
		//}
		if (fireHover == true && (Input.GetKeyUp (KeyCode.RightShift))) {
			currentPos.x += cursorDistance;
			windHover = true;
			fireHover = false;
			hoverElement.sprite = elementText [1];

		} else if (windHover == true && (Input.GetKeyUp (KeyCode.RightShift))) {
			currentPos.x += cursorDistance;
			windHover = false;
			lightHover = true;
			hoverElement.sprite = elementText [2];

		} else if (lightHover == true && (Input.GetKeyUp (KeyCode.RightShift))) {
			currentPos.x += cursorDistance;
			lightHover = false;
			iceHover = true;
			hoverElement.sprite = elementText [3];

		} else if (iceHover == true && (Input.GetKeyUp (KeyCode.RightShift))){
			currentPos.x -= 3 * cursorDistance;
			fireHover = true;
			iceHover = false;
			hoverElement.sprite = elementText [0];

		}


		transform.position = currentPos;
		
	}
}
