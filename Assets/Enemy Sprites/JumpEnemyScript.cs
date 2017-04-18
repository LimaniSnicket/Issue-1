using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyScript : MonoBehaviour {

	public bool hittingGround = true;
	public float jump_Height = 1;
	Rigidbody2D enemySprite;

	public float maxX;
	public float minX;

	public float moveSpeed;

	public bool gamePaused = false;


	public bool enemyAttack = false;
	public float basicEnemyHealth = 10;
	public bool basicEnemyDead = false;
	public bool playerIsAttacking = false;
	public bool enemyHurt = false;


	//bools for elemental weaknesses and stuff like that
	bool fireHover = true;
	bool windHover = false;
	bool lightHover = false;
	bool iceHover = false;

	public bool fireWeakness;
	public bool windWeakness;
	public bool lightWeakness;
	public bool iceWeakness;

	public bool fireHeal;
	public bool windHeal;
	public bool lightHeal;
	public bool iceHeal;


	public bool playerFacingRight = true;

	public Sprite[] jumpingSprite;
	public SpriteRenderer showSprite;

	// Use this for initialization
	void Start () {
		enemySprite = GetComponent<Rigidbody2D> ();
		showSprite.sprite = jumpingSprite [0];
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 currentPos = transform.position;

		if (gamePaused == false && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = true;
		} else if (gamePaused == true && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = false;
		}

		if (gamePaused == false) {


			if (hittingGround == true) { //key command that triggers jumping
				enemySprite.AddForce (transform.up * jump_Height, ForceMode2D.Impulse);
				hittingGround = false;
			}

			if (basicEnemyHealth < 1) {
				basicEnemyDead = true;
				enemyAttack = false;
				//showSprite.sprite = jumpingSprite [2];
				Destroy (gameObject);
			}
			

			if (Input.GetKey (KeyCode.Space)) {
				playerIsAttacking = true;
			} else {
				playerIsAttacking = false;
			}

			currentPos.x += moveSpeed * Time.deltaTime;
			if (currentPos.x > maxX) {
				currentPos.x = maxX;
				moveSpeed = -moveSpeed;
			}
			if (currentPos.x < minX) {
				currentPos.x = minX;
				moveSpeed = -moveSpeed;
			}

			//elemental hover + Bullet stuff
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

			if (enemyHurt) {
				showSprite.sprite = jumpingSprite [2];
			} 
			//making sure enemy knows if player is facing them or not
			if (Input.GetKey (KeyCode.D)) {
				playerFacingRight = true;
			} else if (Input.GetKey (KeyCode.A)) {
				playerFacingRight = false;
			}

			transform.position = currentPos;

		}


	}


	void OnCollisionEnter2D (Collision2D gameObjectHittingme)
	{

		if (gameObjectHittingme.gameObject.tag == "Floor") {
			//Debug.Log ("hitting ground");
			hittingGround = true;

		}

		if (gameObjectHittingme.gameObject.tag == "Player" && playerIsAttacking == false) {
			showSprite.sprite = jumpingSprite [1];
		} else {
			showSprite.sprite = jumpingSprite [0];
		}


		//all the fire weaknesses & buffs go here

		if (gameObjectHittingme.gameObject.tag == "bullet" && fireHover == true && fireWeakness == false && fireHeal == false) { //taking damage from player bullets 
			Debug.Log ("suck it asshat");
			basicEnemyHealth = basicEnemyHealth - 5;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;
		} else if (gameObjectHittingme.gameObject.tag == "bullet" && fireHover == true && fireWeakness == true && fireHeal == false){
			Debug.Log ("Fire Weakness");
			basicEnemyHealth = basicEnemyHealth - 8;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;
		}  else if (gameObjectHittingme.gameObject.tag == "bullet" && fireHover == true && fireHeal == true && fireWeakness == false){
			Debug.Log ("Fire Heal");
			basicEnemyHealth = basicEnemyHealth + 5;
			Destroy (gameObjectHittingme.gameObject);
		} else {
			enemyHurt = false;
		}


		//all the wind weaknesses & buffs go here

		if (gameObjectHittingme.gameObject.tag == "bullet" && windHover == true && windWeakness == false && windHeal == false) { //taking damage from player bullets 
			Debug.Log ("suck it asshat");
			basicEnemyHealth = basicEnemyHealth - 5;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;
		} else if (gameObjectHittingme.gameObject.tag == "bullet" && windHover == true && windWeakness == true && windHeal == false){
			Debug.Log ("Wind Weakness");
			basicEnemyHealth = basicEnemyHealth - 8;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;
		}  else if (gameObjectHittingme.gameObject.tag == "bullet" && windHover == true && windHeal == true && windWeakness == false){
			Debug.Log ("Wind Heal");
			basicEnemyHealth = basicEnemyHealth + 5;
			Destroy (gameObjectHittingme.gameObject);
		} else {
			enemyHurt = false;
		}

		//all the light weaknesses & buffs go here

		if (gameObjectHittingme.gameObject.tag == "bullet" && lightHover == true && lightWeakness == false && lightHeal == false) { //taking damage from player bullets 
			Debug.Log ("suck it asshat");
			basicEnemyHealth = basicEnemyHealth - 5;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;
		} else if (gameObjectHittingme.gameObject.tag == "bullet" && lightHover == true && lightWeakness == true && lightHeal == false){
			Debug.Log ("Light Weakness");
			basicEnemyHealth = basicEnemyHealth - 8;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;
		}  else if (gameObjectHittingme.gameObject.tag == "bullet" && lightHover == true && lightHeal == true && lightWeakness == false){
			Debug.Log ("Light Heal");
			basicEnemyHealth = basicEnemyHealth + 5;
			Destroy (gameObjectHittingme.gameObject);
		} else {
			enemyHurt = false;
		}


		//all the ice weaknesses & buffs go here

		if (gameObjectHittingme.gameObject.tag == "bullet" && iceHover == true && iceWeakness == false && iceHeal == false) { //taking damage from player bullets 
			Debug.Log ("suck it asshat");
			basicEnemyHealth = basicEnemyHealth - 5;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;
		} else if (gameObjectHittingme.gameObject.tag == "bullet" && iceHover == true && iceWeakness == true && iceHeal == false){
			Debug.Log ("Ice Weakness");
			basicEnemyHealth = basicEnemyHealth - 8;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;
		}  else if (gameObjectHittingme.gameObject.tag == "bullet" && iceHover == true && iceHeal == true && iceWeakness == false){
			Debug.Log ("Ice Heal");
			basicEnemyHealth = basicEnemyHealth + 5;
			Destroy (gameObjectHittingme.gameObject);
		} else {
			enemyHurt = false;
		}



	}
	void OnTriggerEnter2D (Collider2D triggerHittingMe){ //test triggers

		if (triggerHittingMe.gameObject.tag == "Right Attack" && playerIsAttacking == true && playerFacingRight == true) {
			Debug.Log ("Right Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
			showSprite.sprite = jumpingSprite [2];
		} else if (triggerHittingMe.gameObject.tag == "Left Attack" && playerIsAttacking == true && playerFacingRight == false) {
			Debug.Log ("Left Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
			showSprite.sprite = jumpingSprite [2];
		} else if (triggerHittingMe.gameObject.tag == "Up Attack" && playerIsAttacking == true) {
			Debug.Log ("Up Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
			enemyHurt = true;
		} else if (triggerHittingMe.gameObject.tag == "Down Attack" && playerIsAttacking == true) {
			Debug.Log ("Down Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
			enemyHurt = true;
		} else {
			enemyHurt = false;
		}

		//different directional melee attacks




	}



}