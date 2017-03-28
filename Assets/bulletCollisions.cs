using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollisions : MonoBehaviour {



	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		


		
	}

	void OnCollisionEnter2D (Collision2D gameObjectHittingme){


		if (gameObjectHittingme.gameObject.tag == "bullet"){
			Destroy (gameObjectHittingme.gameObject);
		}

	

		if (gameObjectHittingme.gameObject.tag == "Bullet Trail"){
			Destroy (gameObjectHittingme.gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D triggerHittingMe){ //test triggers


		if (triggerHittingMe.gameObject.tag == "Enemy Projectile") {
			Destroy (triggerHittingMe.gameObject);
		}
	}



}
