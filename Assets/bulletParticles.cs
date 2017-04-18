using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletParticles : MonoBehaviour {

	public GameObject fireParticle;
	//public GameObject windParticle; 
	//public GameObject lightParticle;
	//public GameObject iceParticle;
	//bullets hell yeah


	//elemental menu bools
	public bool fireHover = true;
	public bool windHover = false;
	public bool lightHover = false;
	public bool iceHover = false;


	//health and mana goes here i guess
	public float playerMana = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;

		if (fireHover == true && (Input.GetKeyUp (KeyCode.RightArrow)) && playerMana > 4) {
			GameObject newObject = Instantiate (fireParticle) as GameObject;
			SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
			Rigidbody2D bulletParticle = newObject.GetComponent<Rigidbody2D> ();
			Vector3 newObjPos = newObject.transform.position;
			newObjPos.x = currentPos.x -.1f;
			newObjPos.y = currentPos.y -.1f;
			bulletParticle.AddForce (new Vector2 (1, 0), ForceMode2D.Impulse);
			newObject.transform.position = newObjPos;
			playerMana = playerMana - 4;
		} else if (fireHover == true && (Input.GetKeyUp (KeyCode.LeftArrow)) && playerMana > 4) {
			GameObject newObject = Instantiate (fireParticle) as GameObject;
			SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
			Rigidbody2D bulletParticle = newObject.GetComponent<Rigidbody2D> ();
			Vector3 newObjPos = newObject.transform.position;
			newObjPos.x = currentPos.x - .1f;
			newObjPos.y = currentPos.y - .1f;
			bulletParticle.AddForce (new Vector2 (-1, 0), ForceMode2D.Impulse);
			newObject.transform.position = newObjPos;
			playerMana = playerMana - 4;
		} else if (fireHover == true && (Input.GetKeyUp (KeyCode.UpArrow)) && playerMana > 4) {
			GameObject newObject = Instantiate (fireParticle) as GameObject;
			SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
			Rigidbody2D bulletParticle = newObject.GetComponent<Rigidbody2D> ();
			Vector3 newObjPos = newObject.transform.position;
			newObjPos.x = currentPos.x;
			newObjPos.y = currentPos.y + .1f;
			bulletParticle.AddForce (new Vector2 (0, 1), ForceMode2D.Impulse);
			newObject.transform.position = newObjPos;
			playerMana = playerMana - 4;
		}


		if (fireHover == true && (Input.GetKeyUp (KeyCode.RightShift))) {
			windHover = true;
			fireHover = false;
		} else if (windHover == true && (Input.GetKeyUp (KeyCode.RightShift))) {
			windHover = false;
			lightHover = true;
		} else if (lightHover == true && (Input.GetKeyUp (KeyCode.RightShift))) {
			lightHover = false;
			iceHover = true;
		} else if (iceHover == true && (Input.GetKeyUp (KeyCode.RightShift))) {
			fireHover = true;
			iceHover = false;
		}

		if (playerMana < 20) {
			playerMana = playerMana + .4f*Time.deltaTime; //player mana regeneration!!!!
		} 

		transform.position = currentPos;


	}
	void OnCollisionEnter2D (Collision2D gameObjectHittingme)
	{


		if (gameObjectHittingme.gameObject.tag == "bullet"){
			Destroy (gameObjectHittingme.gameObject);
		}

		if (gameObjectHittingme.gameObject.tag == "Enemy Projectile"){
			Destroy (gameObjectHittingme.gameObject);
		}
	}
}
