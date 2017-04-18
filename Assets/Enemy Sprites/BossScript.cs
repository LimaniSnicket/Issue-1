using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

	public bool gamePaused = false;

	public bool talkPhase = true;
	public bool battlephase = false;

	//checking to get text boxes working
	public bool textOne = true;
	public bool textTwo = false;
	public bool textThree = false;
	public bool textFour = false;
	public bool textFive = false;
	public bool textSix = false;
	public bool textSeven = false;
	public bool textEight = false;
	public Sprite[] textPic;
	public SpriteRenderer activeText;

	//boss components and shit like that
	public float bossHealth = 20f;
	public float shieldOneStrength = 20f;
	public float shieldTwoStrength = 30f;
	public bool bossVulnerable = false;
	public bool shieldOneActive = false;
	public bool shieldTwoActive = false;

	public float startPosX;
	public float startPosY;
	public float vulnerablePosX;
	public float vulnerablePosY;
	public float bossTimer = 0;

	//bools for recognizing player melee attacks
	public bool playerIsAttacking = false;
	public bool playerIsntAttacking = true;
	public float playerAttackingTime = 2f;

	//timers for various stuff
	public float vulnerableTime = 0f;

	//sprites for shields
	public Sprite[] shieldPic;
	public SpriteRenderer activeShield;

	//sprites for the boss
	public Sprite[] bossSprite;
	public SpriteRenderer bossPose;


	//getting shooting in this bitch whoop
	public GameObject player;
	public GameObject projectile;
	public GameObject shootingPoint;

	//getting that grunt spawning in this bitch whoop
	public GameObject lowEnemy;
	public float enemyTimer;
	public float enemyCoolDown = 5f; 

	public float distanceFromPlayer;
	public float maxRange = 10f;
	public float projectileSpeed = 2f;
	public float projectileTimer;
	public float projectileCoolDown = 1f; 
	//end of shooting stuff 

	//bools for elemental weaknesses and stuff like that
	bool fireHover = true;
	bool windHover = false;
	bool lightHover = false;
	bool iceHover = false;
	public bool fireHeal;
	public bool windHeal;
	public bool lightHeal;
	public bool iceHeal;


	// Use this for initialization
	void Start () {
		activeText.sprite = textPic [0];
		activeShield.sprite = shieldPic [0];
		bossPose.sprite = bossSprite [0];

		player = GameObject.FindGameObjectWithTag("Player");//finding the player so it can shoot him in the face
	}
	
	// Update is called once per frame
	void Update () {

		if (gamePaused == false && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = true;
		} else if (gamePaused == true && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = false;
		}
			
			//for the shooting ai
			distanceFromPlayer = transform.position.x - player.transform.position.x;

			ShootPlayer ();

			Vector3 currentPos = transform.position;
		if (gamePaused == false) {
			if (talkPhase == false && bossHealth >= 10) {
				bossTimer = bossTimer + Time.deltaTime;
			} else if (bossHealth <= 10) {
				bossTimer = bossTimer + 2 * Time.deltaTime; //makes the boss movement super duper speed up once it's health is below a certain point
			}

		}

			if (bossTimer >= 20) {
				bossTimer = 0; //resets boss timer so boss movement doesn't stop
			}

			//cycling through text in the beginning of the stage
			if (textOne == true && (Input.GetKeyUp (KeyCode.Return))) {
				activeText.sprite = textPic [1];
				textTwo = true;
				textOne = false;
			} else if (textTwo == true && (Input.GetKeyUp (KeyCode.Return))) {
				activeText.sprite = textPic [2];
				textTwo = false;
				textThree = true;
			} else if (textThree == true && (Input.GetKeyUp (KeyCode.Return))) {
				activeText.sprite = textPic [3];
				textThree = false;
				textFour = true;
			} else if (textFour == true && (Input.GetKeyUp (KeyCode.Return))) {
				activeText.sprite = textPic [4];
				textFour = false;
				textFive = true;
			} else if (textFive == true && (Input.GetKeyUp (KeyCode.Return))) {
				activeText.sprite = textPic [5];
				textFive = false;
				textSix = true;
			} else if (textSix == true && (Input.GetKeyUp (KeyCode.Return))) {
				activeText.sprite = textPic [6];
				textSix = false;
				textSeven = true;
			} else if (textSeven == true && (Input.GetKeyUp (KeyCode.Return))) {
				activeText.sprite = textPic [7];
				textSeven = false;
				textEight = true;
			} else if (textEight == true && (Input.GetKeyUp (KeyCode.Return))) {
				talkPhase = false;
				battlephase = true;
				currentPos.x = startPosX;
				currentPos.y = startPosY;
			}
			//getting shields to work only after all the text boxes have been cycled through
			if (battlephase && shieldOneStrength > 0) {
				shieldOneActive = true;
				bossVulnerable = false;
				activeShield.sprite = shieldPic [1];
				bossPose.sprite = bossSprite [1];

			} else if (shieldOneStrength <= 0 && shieldTwoActive == false) {
				shieldOneActive = false;
				vulnerableTime = vulnerableTime + Time.deltaTime;
				activeShield.sprite = shieldPic [0];
				bossVulnerable = true;
			}

			if (battlephase && shieldTwoStrength > 0 && vulnerableTime >= 3) {
				shieldTwoActive = true;
				activeShield.sprite = shieldPic [2];
				vulnerableTime = 0;
				bossVulnerable = false;
				bossPose.sprite = bossSprite [2];
			} else if (shieldTwoStrength <= 0) {
				shieldTwoActive = false;
				activeShield.sprite = shieldPic [0];
			}

			if (bossTimer >= 0 && bossTimer <= 5) { // yo chit if you could get the boss to spawn within a random range instead of specific points that would be rad
				currentPos.x = 7.83f;
				currentPos.y = -1.54f;
				fireHeal = true;
				windHeal = false;
				lightHeal = false;
				iceHeal = false;
			} else if (bossTimer >= 5 && bossTimer <= 10) {
				currentPos.x = 0f;
				currentPos.y = 2.61f;
				fireHeal = false;
				windHeal = true;
				lightHeal = false;
				iceHeal = false;
			} else if (bossTimer >= 10 && bossTimer <= 15) {
				currentPos.x = -3.94f;
				currentPos.y = -0.04f;
				fireHeal = false;
				windHeal = false;
				lightHeal = true;
				iceHeal = false;
			} else if (bossTimer >= 15 && bossTimer < 20) {
				currentPos.x = 6.05f;
				currentPos.y = 3.17f;
				fireHeal = false;
				windHeal = false;
				lightHeal = false;
				iceHeal = true;
			} 

			//player attacking timer for more precise attacking
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

			transform.position = currentPos;

			if (bossHealth <= 0) {
				Application.LoadLevel ("Win Screen"); //winning this bitch woot
			}

	}

	void OnCollisionEnter2D (Collision2D gameObjectHittingme){

		if (bossVulnerable == true && (gameObjectHittingme.gameObject.tag == "bullet")) {
			bossHealth = bossHealth - 3f;
			Destroy (gameObjectHittingme.gameObject);
		} 

	}

	void OnTriggerEnter2D (Collider2D triggerHittingMe){ //test triggers for shields since I don't know how else to do it rippo

		if (battlephase == true && shieldOneActive == true && fireHover == true && fireHeal == false && (triggerHittingMe.gameObject.tag == "bullet")){
			shieldOneStrength = shieldOneStrength - 10f;
			Destroy (triggerHittingMe.gameObject);
		} else if (battlephase == true && shieldTwoActive == true && fireHover == true && fireHeal == false && (triggerHittingMe.gameObject.tag == "bullet")){
			shieldTwoStrength = shieldTwoStrength - 15f;
			Destroy (triggerHittingMe.gameObject);
		} else if (battlephase == true && shieldOneActive == true && windHover == true && windHeal == false && (triggerHittingMe.gameObject.tag == "bullet")){
			shieldOneStrength = shieldOneStrength - 10f;
			Destroy (triggerHittingMe.gameObject);
		} else if (battlephase == true && shieldTwoActive == true && windHover == true && windHeal == false && (triggerHittingMe.gameObject.tag == "bullet")){
			shieldTwoStrength = shieldTwoStrength - 15f;
			Destroy (triggerHittingMe.gameObject);
		} else if (battlephase == true && shieldOneActive == true && lightHover == true && lightHeal == false && (triggerHittingMe.gameObject.tag == "bullet")){
			shieldOneStrength = shieldOneStrength - 10f;
			Destroy (triggerHittingMe.gameObject);
		} else if (battlephase == true && shieldTwoActive == true && lightHover == true && lightHeal == false && (triggerHittingMe.gameObject.tag == "bullet")){
			shieldTwoStrength = shieldTwoStrength - 15f;
			Destroy (triggerHittingMe.gameObject);
		} else if (battlephase == true && shieldOneActive == true && iceHover == true && iceHeal == false && (triggerHittingMe.gameObject.tag == "bullet")){
			shieldOneStrength = shieldOneStrength - 10f;
			Destroy (triggerHittingMe.gameObject);
		} else if (battlephase == true && shieldTwoActive == true && iceHover == true && iceHeal == false && (triggerHittingMe.gameObject.tag == "bullet")){
			shieldTwoStrength = shieldTwoStrength - 15f;
			Destroy (triggerHittingMe.gameObject);
		} //this code essentially gets all the elemental healing working properly


		if (triggerHittingMe.gameObject.tag == "bullet" && fireHover == true && fireHeal == true && shieldOneActive == true){
			Debug.Log ("Fire Heal");
			shieldOneStrength = shieldOneStrength + 5f;
			Destroy (triggerHittingMe.gameObject);
		} else if (triggerHittingMe.gameObject.tag == "bullet" && fireHover == true && fireHeal == true && shieldTwoActive == true){
			Debug.Log ("Fire Heal");
			shieldTwoStrength = shieldTwoStrength + 5f;
			Destroy (triggerHittingMe.gameObject);
		} else if (triggerHittingMe.gameObject.tag == "bullet" && windHover == true && windHeal == true && shieldOneActive == true){
			Debug.Log ("Wind Heal");
			shieldOneStrength = shieldOneStrength + 5f;
			Destroy (triggerHittingMe.gameObject);
		} else if (triggerHittingMe.gameObject.tag == "bullet" && windHover == true && windHeal == true && shieldTwoActive == true){
			Debug.Log ("Wind Heal");
			shieldTwoStrength = shieldTwoStrength + 5f;
			Destroy (triggerHittingMe.gameObject);
		} else if (triggerHittingMe.gameObject.tag == "bullet" && lightHover == true && lightHeal == true && shieldOneActive == true){
			Debug.Log ("Light Heal");
			shieldOneStrength = shieldOneStrength + 5f;
			Destroy (triggerHittingMe.gameObject);
		} else if (triggerHittingMe.gameObject.tag == "bullet" && lightHover == true && lightHeal == true && shieldTwoActive == true){
			Debug.Log ("Light Heal");
			shieldTwoStrength = shieldTwoStrength + 5f;
			Destroy (triggerHittingMe.gameObject);
		} else if (triggerHittingMe.gameObject.tag == "bullet" && iceHover == true && iceHeal == true && shieldOneActive == true){
			Debug.Log ("Ice Heal");
			shieldOneStrength = shieldOneStrength + 5f;
			Destroy (triggerHittingMe.gameObject);
		} else if (triggerHittingMe.gameObject.tag == "bullet" && iceHover == true && iceHeal == true && shieldTwoActive == true){
			Debug.Log ("Ice Heal");
			shieldTwoStrength = shieldTwoStrength + 5f;
			Destroy (triggerHittingMe.gameObject);
		}

		if (triggerHittingMe.gameObject.tag == "Right Attack" && playerIsAttacking == true && shieldOneActive == true) {
			Debug.Log ("Right Hitbox works");
			shieldOneStrength = shieldOneStrength - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Left Attack" && playerIsAttacking == true && shieldOneActive == true) {
			Debug.Log ("Left Hitbox works");
			shieldOneStrength = shieldOneStrength - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Up Attack" && playerIsAttacking == true && shieldOneActive == true) {
			Debug.Log ("Up Hitbox works");
			shieldOneStrength = shieldOneStrength - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Down Attack" && playerIsAttacking == true && shieldOneActive == true) {
			Debug.Log ("Down Hitbox works");
			shieldOneStrength = shieldOneStrength - 10f;
		}
		if (triggerHittingMe.gameObject.tag == "Right Attack" && playerIsAttacking == true && shieldTwoActive == true) {
			Debug.Log ("Right Hitbox works");
			shieldTwoStrength = shieldTwoStrength - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Left Attack" && playerIsAttacking == true && shieldTwoActive == true) {
			Debug.Log ("Left Hitbox works");
			shieldTwoStrength = shieldTwoStrength - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Up Attack" && playerIsAttacking == true && shieldTwoActive == true) {
			Debug.Log ("Up Hitbox works");
			shieldTwoStrength = shieldTwoStrength - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Down Attack" && playerIsAttacking == true && shieldTwoActive == true) {
			Debug.Log ("Down Hitbox works");
			shieldTwoStrength = shieldTwoStrength - 10f;
		} 
		if (triggerHittingMe.gameObject.tag == "Right Attack" && playerIsAttacking == true && bossVulnerable == true) {
			Debug.Log ("Right Hitbox works");
			bossHealth = bossHealth - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Left Attack" && playerIsAttacking == true && bossVulnerable == true) {
			Debug.Log ("Left Hitbox works");
			bossHealth = bossHealth - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Up Attack" && playerIsAttacking == true && bossVulnerable == true) {
			Debug.Log ("Up Hitbox works");
			bossHealth = bossHealth - 10f;
		} else if (triggerHittingMe.gameObject.tag == "Down Attack" && playerIsAttacking == true && bossVulnerable == true) {
			Debug.Log ("Down Hitbox works");
			bossHealth = bossHealth - 10f;
		}



	}

	void ShootPlayer(){
		if (gamePaused == false) {

			if (bossVulnerable == false) { //projectile timer starts ticking up at game start; will fire at player as soon as player enters maxRange
				projectileTimer += Time.deltaTime;
			} else if (bossVulnerable == true) {
				projectileTimer += 3f * Time.deltaTime; // speeds up shooting rate if shields are down maybe???
			}

			if (projectileTimer >= projectileCoolDown && shieldOneActive == true) {
				Vector2 projectileDirection = player.transform.position - transform.position;
				distanceFromPlayer = Mathf.Abs (transform.position.x - player.transform.position.x);     //Must add mathf.abs to get absolute value otherwise distancefromplayer will always be less than maxrange in negative

				if (distanceFromPlayer <= maxRange) {
					print ("playerWithinRange");         
					GameObject projectileClone;
					projectileClone = Instantiate (projectile, shootingPoint.transform.position, shootingPoint.transform.rotation) as GameObject;        //instantiates the projectile at shooting point gameobject
					projectileClone.GetComponent<Rigidbody2D> ().velocity = projectileDirection * projectileSpeed;               

					projectileTimer = 0;        

				} 

			}

			if (projectileTimer >= projectileCoolDown && shieldTwoActive == true) {
				Vector2 projectileDirection = player.transform.position - transform.position;
				distanceFromPlayer = Mathf.Abs (transform.position.x - player.transform.position.x);     //Must add mathf.abs to get absolute value otherwise distancefromplayer will always be less than maxrange in negative

				if (distanceFromPlayer <= maxRange) {
					print ("playerWithinRange");         
					GameObject projectileClone;
					projectileClone = Instantiate (projectile, shootingPoint.transform.position, shootingPoint.transform.rotation) as GameObject;        //instantiates the projectile at shooting point gameobject
					projectileClone.GetComponent<Rigidbody2D> ().velocity = projectileDirection * projectileSpeed;               

					projectileTimer = 0;       

				} 

			}

			if (projectileTimer >= projectileCoolDown && bossVulnerable == true) {
				Vector2 projectileDirection = player.transform.position - transform.position;
				distanceFromPlayer = Mathf.Abs (transform.position.x - player.transform.position.x);     //Must add mathf.abs to get absolute value otherwise distancefromplayer will always be less than maxrange in negative

				if (distanceFromPlayer <= maxRange) {
					print ("playerWithinRange");         
					GameObject projectileClone;
					projectileClone = Instantiate (projectile, shootingPoint.transform.position, shootingPoint.transform.rotation) as GameObject;        //instantiates the projectile at shooting point gameobject
					projectileClone.GetComponent<Rigidbody2D> ().velocity = projectileDirection * projectileSpeed;               

					projectileTimer = 0;       

				} 

			}
		}

	}

}
