using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingEnemies : MonoBehaviour {

	public GameObject player;

	public float maxX;
	public float minX;

	public float moveSpeed;


	public bool basicEnemyAttack = false;
	public float basicEnemyHealth;
	public bool basicEnemyDead = false;
	public bool playerIsAttacking = false;
	public bool playerIsntAttacking = true;
	public float playerAttackingTime = 2f;
	public bool enemyHurt = false;

	public bool playerInRange;

	Rigidbody2D enemySprite;

	public Sprite[] dashingSprite;
	public SpriteRenderer showSprite;

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

	//for accurate hitboxes depending on which direction player is facing
	public bool playerFacingRight = true;



	// Use this for initialization
	void Start () {
		enemySprite = GetComponent<Rigidbody2D> ();
		showSprite.sprite = dashingSprite [0];
	}

	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;
	
		if (basicEnemyHealth < 1) {
			basicEnemyDead = true;
			basicEnemyAttack = false;
			Destroy(gameObject);
		}

		currentPos.x += moveSpeed * Time.deltaTime;
		if (currentPos.x > maxX && enemyHurt == false) {
			currentPos.x = maxX ;
			moveSpeed = -moveSpeed;
			showSprite.sprite = dashingSprite [3];
		} else if (currentPos.x < minX && enemyHurt == false) {
			currentPos.x = minX;
			moveSpeed = -moveSpeed;
			showSprite.sprite = dashingSprite [0];
		}
		if (currentPos.x > maxX && enemyHurt == true) {
			currentPos.x = maxX ;
			moveSpeed = -moveSpeed;
			showSprite.sprite = dashingSprite [2];
		} else if (currentPos.x < minX && enemyHurt == true) {
			currentPos.x = minX;
			moveSpeed = -moveSpeed;
			showSprite.sprite = dashingSprite [4];
		}


		//player attacking timer for more precise attacking
		if (playerIsAttacking) {
			playerAttackingTime = playerAttackingTime - 5f*Time.deltaTime;
		}
		if (playerAttackingTime > 0 && (Input.GetKey (KeyCode.Space))) {
			playerIsAttacking = true;
			playerIsntAttacking = false;
		} else {
			playerIsAttacking = false;
			playerIsntAttacking = true;
		}
		if (playerIsntAttacking == true && (Input.GetKeyUp (KeyCode.Space))) {
			playerAttackingTime = 2f;
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



		//making sure enemy knows if player is facing them or not
		if (Input.GetKey (KeyCode.D)) {
			playerFacingRight = true;
		} else if (Input.GetKey (KeyCode.A)) {
			playerFacingRight = false;
		}



		transform.position = currentPos;
		//player.transform.position = playerPos;
	}
	void OnCollisionEnter2D (Collision2D gameObjectHittingme)
	{

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

		if (triggerHittingMe.gameObject.tag == "Player") {
			Debug.Log ("I'm triggered");
			playerInRange = true;

		} else {
			playerInRange = false;
		}
		if (triggerHittingMe.gameObject.tag == "Right Attack" && playerIsAttacking == true && playerFacingRight == true) {
			Debug.Log ("Right Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
		} else if (triggerHittingMe.gameObject.tag == "Left Attack" && playerIsAttacking == true && playerFacingRight == false) {
			Debug.Log ("Left Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
		} else if (triggerHittingMe.gameObject.tag == "Up Attack" && playerIsAttacking == true) {
			Debug.Log ("Up Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
		} else if (triggerHittingMe.gameObject.tag == "Down Attack" && playerIsAttacking == true) {
			Debug.Log ("Down Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
		}

		//different directional melee attacks



	}

}
