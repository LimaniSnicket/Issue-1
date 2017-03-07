using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	bool hitEnemy = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		

	}


	void OnCollisionEnter2D (Collision2D gameObjectHittingme)
	{

		if (hitEnemy == false && (Input.GetKey (KeyCode.Space))) {
			//GetComponent<Rigidbody2D>().
			hitEnemy = true;
		} else {
			hitEnemy = false;
		}

		if (hitEnemy == true && (gameObjectHittingme.gameObject.tag == "Enemy")){
			//			Debug.Log("HI");
			//			GetComponent<Rigidbody2D>().AddForce(transform.up*3,ForceMode2D.Impulse);
			Debug.Log ("hitting enemy");
			Destroy (gameObjectHittingme.gameObject);
		}
			}

	}


