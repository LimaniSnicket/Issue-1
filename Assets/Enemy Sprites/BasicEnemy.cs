using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

	public float maxX;
	public float minX;

	public float moveSpeed;


	public GameObject player;

	public bool basicEnemyAttack = false;
	public float basicEnemyHealth = 10;
	public bool basicEnemyDead = false;
	public bool playerIsAttacking = false;
	public bool playerInRange = false;

	public Sprite[] enemySprite;
	public SpriteRenderer showSprite;




	// Use this for initialization
	void Start () {
		showSprite.sprite = enemySprite [0];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;

		if (currentPos.x > maxX && playerInRange == false) {
			currentPos.x = maxX;
			moveSpeed = -moveSpeed;
		}
		if (currentPos.x < minX && playerInRange == false) {
			currentPos.x = minX;
			moveSpeed = -moveSpeed;
		}



		if (basicEnemyHealth < 1) {
			basicEnemyDead = true;
			basicEnemyAttack = false;
			Destroy(gameObject);
			//showSprite.sprite = enemySprite [2];
		}




		if (Input.GetKey (KeyCode.Space)) {
			playerIsAttacking = true;
		} else {
			playerIsAttacking = false;
		}

		transform.position = currentPos;
		
	}

	void OnCollisionEnter2D (Collision2D gameObjectHittingme)
	{



		if (gameObjectHittingme.gameObject.tag == "bullet") { //taking damage from player bullets 
			Debug.Log ("suck it asshat");
			basicEnemyHealth = basicEnemyHealth - 5;
			//playerIsAttacking = true;
			Destroy (gameObjectHittingme.gameObject);

		}

		if (playerIsAttacking == true && (gameObjectHittingme.gameObject.tag == "Player")) { //taking damage from player melee 
			Debug.Log ("get fucked");
			basicEnemyHealth = basicEnemyHealth - 5;

		}

			if (gameObjectHittingme.gameObject.tag == "wall") {
				Debug.Log ("The wall");

			}


	
	}
	void OnTriggerEnter2D (Collider2D triggerHittingMe){ //test triggers

		if (triggerHittingMe.gameObject.tag == "Player") {
			Debug.Log ("I'm triggered");
			playerInRange = true;

		} else {
			playerInRange = false;
		}




	}


}
