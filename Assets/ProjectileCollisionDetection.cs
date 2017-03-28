using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionDetection : MonoBehaviour {

	public bool playerProjectile;
	public bool enemyProjectile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D gameObjectHittingme){

		if (playerProjectile == true && gameObjectHittingme.gameObject.tag == "Enemy") {
			Destroy(gameObject);
		}
		if (enemyProjectile == true && gameObjectHittingme.gameObject.tag == "Player") {
			Destroy(gameObject);
		}

		if (gameObjectHittingme.gameObject.tag == "Floor") {
			Destroy(gameObject);
		} else if(gameObjectHittingme.gameObject.tag == "wall"){
			Destroy(gameObject);
		}  else if(gameObjectHittingme.gameObject.tag == "DEATH PANEL"){
			Destroy(gameObject);
		}

	}

	void OnTriggerEnter2D (Collider2D triggerHittingme){
		if (triggerHittingme.gameObject.tag == "wall"){
			Destroy (gameObject);
		} else if (triggerHittingme.gameObject.tag == "Floor"){
			Destroy (gameObject);
		}

		if (triggerHittingme.gameObject.tag == "Crouch shield" && (Input.GetKey(KeyCode.S))){
			Destroy (gameObject);
		}
	}
}
