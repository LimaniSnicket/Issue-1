using UnityEngine;
using System.Collections;

public class Jump_Thing : MonoBehaviour {

    bool hittingGround;
    public float jump_Height = 1;
	public float timeCharged = 1f;
	Rigidbody2D playerSprite;

	// Use this for initialization
	void Start () {
		playerSprite = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	if (hittingGround == true && (Input.GetKey(KeyCode.W))){ //key command that triggers jumping
			playerSprite.AddForce(transform.up*jump_Height,ForceMode2D.Impulse);
	hittingGround = false;
	}

		if (hittingGround == false && (Input.GetKeyUp (KeyCode.LeftShift))) {
			playerSprite.AddForce (new Vector2 (10 * timeCharged, 10 * timeCharged), ForceMode2D.Impulse);

		}else if (Input.GetKeyDown (KeyCode.LeftShift)) {

			timeCharged = timeCharged + Time.deltaTime;

		}

	}


	void OnCollisionEnter2D (Collision2D gameObjectHittingme)
	{

		if (gameObjectHittingme.gameObject.tag == "Floor") {
			Debug.Log ("hitting ground");
			hittingGround = true;

		}

		 
	}


	}