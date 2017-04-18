using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemies : MonoBehaviour {

	public GameObject enemyProjectile;
	public GameObject player;

	public float maxX;
	public float minX;

	public float moveSpeed;

	public bool gamePaused = false;

	//public bool basicEnemyAttack = false;
	public float basicEnemyHealth;
	public bool basicEnemyDead = false;
	public bool playerIsAttacking = false;
	public bool playerIsntAttacking = true;
	public float playerAttackingTime = 2f;

	public bool enemyHurt = false;

	public bool playerInRange;
	public bool playerFacingRight = true;

	public Sprite[] dashingSprite;
	public SpriteRenderer showSprite;


	// Use this for initialization
	void Start () {

		showSprite.sprite = dashingSprite [0];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;
		Vector3 playerPos = player.transform.position;

		if (gamePaused == false && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = true;
		} else if (gamePaused == true && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = false;
		}

		if (gamePaused == false) {


			if (basicEnemyHealth < 1) {
				basicEnemyDead = true;
				Destroy (gameObject);
			}

			if (playerIsAttacking) {
				playerAttackingTime = playerAttackingTime - 5f * Time.deltaTime;
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


			currentPos.x += moveSpeed * Time.deltaTime;
			if (currentPos.x > maxX && enemyHurt == false) {
				currentPos.x = maxX;
				moveSpeed = -moveSpeed;
				showSprite.sprite = dashingSprite [1];
			} else if (currentPos.x < minX && enemyHurt == false) {
				currentPos.x = minX;
				moveSpeed = -moveSpeed;
				showSprite.sprite = dashingSprite [0];
			}
			if (currentPos.x > maxX && enemyHurt == true) {
				currentPos.x = maxX;
				moveSpeed = -moveSpeed;
				showSprite.sprite = dashingSprite [2];
			} else if (currentPos.x < minX && enemyHurt == true) {
				currentPos.x = minX;
				moveSpeed = -moveSpeed;
				showSprite.sprite = dashingSprite [3];
			}

		
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



		if (gameObjectHittingme.gameObject.tag == "bullet") { //taking damage from player bullets 
			Debug.Log ("suck it asshat");
			basicEnemyHealth = basicEnemyHealth - 5;
			//playerIsAttacking = true;
			Destroy (gameObjectHittingme.gameObject);
			enemyHurt = true;

		} else {
			enemyHurt = false;
		}


	}
	void OnTriggerEnter2D (Collider2D triggerHittingMe){ //test triggers

		if (triggerHittingMe.gameObject.tag == "Player") {
			Debug.Log ("Shoot");
			playerInRange = true;
		} else {
			playerInRange = false;
		}

		if (triggerHittingMe.gameObject.tag == "Right Attack" && playerIsAttacking == true && playerFacingRight == true) {
			Debug.Log ("Right Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
			enemyHurt = true;
		} else if (triggerHittingMe.gameObject.tag == "Left Attack" && playerIsAttacking == true && playerFacingRight == false) {
			Debug.Log ("Left Hitbox works");
			basicEnemyHealth = basicEnemyHealth - 5;
			enemyHurt = true;
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






	}

}
