using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {


	//adding a public bool so I can make certain behaviors only happen during the final boss stage
	public bool finalBattle;
	public bool levelThree;
	public bool levelTwo;
	public bool levelOne;

	public float moveSpeed = 7f;

	//experimenting with attacks
	public bool playerIsAttacking = false;
	public bool playerIsntAttacking = true;
	public float AttackingTime = 2f;


	public bool playerIsCrouching = false;
	public bool playerIsntCrouching = true;
	public float CrouchingTime = 2f;
	public bool playerIsJumping;

	public bool playerIsBoosted = false;
	public bool playerIsShooting = false;


	//need these for animation - distinguishes between up shoot and down shoot
	public bool upAim;
	public bool downAim;


	//getting some control bools to make spriting easier and show what the player is doing in the inspector at all times
	public bool playerIsMoving = false; //have an idle sprite when this is false, moving sprites when true
	public bool playerIsDead = false; //game over works, lit
	public float timeSinceDeath = 2f;


	//bullets????
	public GameObject fireBulletLeft;
	public GameObject windBulletLeft; 
	public GameObject lightBulletLeft;
	public GameObject iceBulletLeft;
	public GameObject fireBulletRight;
	public GameObject windBulletRight; 
	public GameObject lightBulletRight;
	public GameObject iceBulletRight;
	public GameObject fireBulletUp;
	public GameObject windBulletUp; 
	public GameObject lightBulletUp;
	public GameObject iceBulletUp;
	public GameObject fireBulletDown;
	public GameObject windBulletDown; 
	public GameObject lightBulletDown;
	public GameObject iceBulletDown;
	//bullets hell yeah


	//elemental menu bools
	public bool fireHover = true;
	public bool windHover = false;
	public bool lightHover = false;
	public bool iceHover = false;


	//health and mana goes here i guess
	public float playerMana = 20;
	public float playerHealth = 20;


	//player sprites
	public bool facingRight = true;

	public bool hittingGround;
	public float jump_Height;
	public float timeCharged = 1f;
	Rigidbody2D playerSprite;

	public Sprite[] testSprite;
	public SpriteRenderer showSprite;

	public Animator playerAnim;



	//audio goes here i guess
	public AudioClip fireShoot;
	public AudioClip windShoot;
	public AudioClip lightShoot;
	public AudioClip iceShoot;

	public AudioClip attack;
	public AudioClip jump;
	public AudioClip boost;

	private AudioSource source;

	//display sprites
	public Sprite[] leftCharge;
	public SpriteRenderer chargeDisplay;

	public Sprite[] currentHP;
	public SpriteRenderer hpDisplay;

	public Sprite[] currentMana;
	public SpriteRenderer manaDisplay;

	// Use this for initialization
	void Start () {
		
		playerSprite = GetComponent<Rigidbody2D> ();

		chargeDisplay.sprite = leftCharge [0];
		hpDisplay.sprite = currentHP [0];
		manaDisplay.sprite = currentMana [0];

		playerAnim = GetComponent<Animator> ();

		source = GetComponent<AudioSource> ();

		
	}
	
	// Update is called once per frame
	void Update () {

		//Movement code here
		Vector3 currentPos = transform.position;

		//ANIMATION BOOLS DONT TOUCH PLEASE

		playerAnim.SetBool ("FireHover", fireHover);
		playerAnim.SetBool ("WindHover", windHover);
		playerAnim.SetBool ("LightHover", lightHover);
		playerAnim.SetBool ("IceHover", iceHover);
		playerAnim.SetBool ("Moving", playerIsMoving);
		playerAnim.SetBool ("Facing Right", facingRight);
		playerAnim.SetBool ("Grounded", hittingGround);
		playerAnim.SetBool ("Boosted", playerIsBoosted);
		playerAnim.SetBool ("Attacking", playerIsAttacking);
		playerAnim.SetBool ("Dead", playerIsDead);
		playerAnim.SetBool ("Crouch", playerIsCrouching);
		playerAnim.SetBool ("Up Aim", upAim);
		playerAnim.SetBool ("Down Aim", downAim);
		playerAnim.SetBool ("Shooting", playerIsShooting);


		if (Input.GetKey (KeyCode.D) && playerHealth > 0) {
			currentPos.x += moveSpeed * Time.deltaTime;
			facingRight = true;
			playerIsMoving = true;
		} else if (Input.GetKey (KeyCode.A) && playerHealth > 0) {
			currentPos.x -= moveSpeed * Time.deltaTime;
			facingRight = false;
			playerIsMoving = true;
		} else {
			playerIsMoving = false;
		}




		if (playerIsCrouching) {
			CrouchingTime = CrouchingTime - 5f*Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.S) && playerHealth > 0 && CrouchingTime > 0 && playerIsAttacking == false && hittingGround == true) {
			playerIsCrouching = true;
			playerIsMoving = false;
		} else {
			playerIsCrouching = false;
			playerIsntCrouching = true;
		}

		if (playerIsntCrouching == true && (Input.GetKeyUp (KeyCode.S))) {  // couldn't figure out how to change hitboxes so now crouching grants a lil bit of immunity from projectiles whoop
			CrouchingTime = 5f;
		}

		if (hittingGround == true && (Input.GetKey (KeyCode.W)) && playerHealth > 0) { //key command that triggers jumping
			playerSprite.AddForce (transform.up * jump_Height, ForceMode2D.Impulse);
			hittingGround = false;
			playerIsJumping = true;
		} 
		if (hittingGround == false) {
			playerIsJumping = false;

		} 
		if (hittingGround == false && facingRight == true && (Input.GetKeyUp (KeyCode.LeftShift))) {
			playerSprite.AddForce (new Vector2 (10 * timeCharged, 10 * timeCharged), ForceMode2D.Impulse);
			playerIsBoosted = true;
			chargeDisplay.sprite = leftCharge [0];

		} else if (hittingGround == false && facingRight == false && (Input.GetKeyUp (KeyCode.LeftShift))) {
			playerSprite.AddForce (new Vector2 (-10 * timeCharged, 10 * timeCharged), ForceMode2D.Impulse);
			playerIsBoosted = true;
			chargeDisplay.sprite = leftCharge [0];
		}

		if (playerIsJumping == true && (Input.GetKeyUp (KeyCode.LeftShift))) {
			source.PlayOneShot(boost);
		}
		if (Input.GetKeyDown (KeyCode.W)){
			source.PlayOneShot (jump);
		}

		if (hittingGround == true) {
			playerIsBoosted = false;
		}

	
		if (Input.GetKey (KeyCode.LeftShift)) { //charging the mid-air boost, letting go resets it to default value

			timeCharged = timeCharged + .2f*Time.deltaTime;

		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
		    timeCharged = 1f;
			chargeDisplay.sprite = leftCharge [0];
		}
		if (timeCharged > 1.5f) { //setting up charge strength display
			timeCharged = 1.5f;
			chargeDisplay.sprite = leftCharge [5];
		} else if (timeCharged > 1.01f && timeCharged < 1.1f) {
			chargeDisplay.sprite = leftCharge [1];
		} else if (timeCharged > 1.101f && timeCharged < 1.2f) {
			chargeDisplay.sprite = leftCharge [2];
		} else if (timeCharged > 1.201f && timeCharged < 1.3f) {
			chargeDisplay.sprite = leftCharge [3];
		} else if (timeCharged > 1.301f && timeCharged < 1.4f) {
			chargeDisplay.sprite = leftCharge [4];
		} else if (timeCharged > 1.401f && timeCharged < 1.5f) {
			chargeDisplay.sprite = leftCharge [5];
		}
			

		//End of player movement code

		//more experimenting with attacks
		if (playerIsAttacking) {
			AttackingTime = AttackingTime - 5f*Time.deltaTime;
		}


		if (AttackingTime > 0 && (Input.GetKey (KeyCode.Space)) && playerHealth > 0 && playerIsBoosted == false) {
			playerIsAttacking = true;
			playerIsntAttacking = false;
			//source.PlayOneShot (attack);
		} else {
			playerIsAttacking = false;
			playerIsntAttacking = true;
		}


		if (playerIsntAttacking == true && (Input.GetKeyUp (KeyCode.Space))) {
			AttackingTime = 2f;
		}


		if (facingRight == true && (Input.GetKey(KeyCode.RightArrow))) {
			playerIsShooting = true;
		} else if (facingRight == false && (Input.GetKey(KeyCode.LeftArrow))) {
			playerIsShooting = true;
		} else if (Input.GetKey(KeyCode.UpArrow)) {
			playerIsShooting = true;
			upAim = true;
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			playerIsShooting = true;
			downAim = true;
		} else {
			playerIsShooting = false;
			upAim = false;
			downAim = false;

		}




		//Bullets that go to the right, lit
		if (playerIsCrouching == false) {
			if (fireHover == true && (Input.GetKeyUp (KeyCode.RightArrow)) && playerMana > 4 && playerHealth > 0 && facingRight == true) {
				GameObject newObject = Instantiate (fireBulletRight) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (10, 0), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (fireShoot);
			} else if (windHover == true && (Input.GetKeyUp (KeyCode.RightArrow)) && playerMana > 4 && playerHealth > 0 && facingRight == true) {
				GameObject newObject = Instantiate (windBulletRight) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (10, 0), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (windShoot);
			} else if (lightHover == true && (Input.GetKeyUp (KeyCode.RightArrow)) && playerMana > 4 && playerHealth > 0 && facingRight == true) {
				GameObject newObject = Instantiate (lightBulletRight) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (10, 0), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (lightShoot);
			} else if (iceHover == true && (Input.GetKeyUp (KeyCode.RightArrow)) && playerMana > 4 && playerHealth > 0 && facingRight == true) {
				GameObject newObject = Instantiate (iceBulletRight) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (10, 0), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (iceShoot);
			}

			//bullets that go left, kill me pls this is so blocky code-wise
			if (fireHover == true && (Input.GetKeyUp (KeyCode.LeftArrow)) && playerMana > 4 && playerHealth > 0 && facingRight == false) {
				GameObject newObject = Instantiate (fireBulletLeft) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (-10, 0), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (fireShoot);
			} else if (windHover == true && (Input.GetKeyUp (KeyCode.LeftArrow)) && playerMana > 4 && playerHealth > 0 && facingRight == false) {
				GameObject newObject = Instantiate (windBulletLeft) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (-10, 0), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (windShoot);
			} else if (lightHover == true && (Input.GetKeyUp (KeyCode.LeftArrow)) && playerMana > 4 && playerHealth > 0 && facingRight == false) {
				GameObject newObject = Instantiate (lightBulletLeft) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (-10, 0), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (lightShoot);
			} else if (iceHover == true && (Input.GetKeyUp (KeyCode.LeftArrow)) && playerMana > 4 && playerHealth > 0 && facingRight == false) {
				GameObject newObject = Instantiate (iceBulletLeft) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (-10, 0), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (iceShoot);
			}


			//bullets that go up, double lit
			if (fireHover == true && (Input.GetKeyUp (KeyCode.UpArrow)) && playerMana > 4 && playerHealth > 0) {
				GameObject newObject = Instantiate (fireBulletUp) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (0, 10), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (fireShoot);
			} else if (windHover == true && (Input.GetKeyUp (KeyCode.UpArrow)) && playerMana > 4 && playerHealth > 0) {
				GameObject newObject = Instantiate (windBulletUp) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (0, 10), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (windShoot);
			} else if (lightHover == true && (Input.GetKeyUp (KeyCode.UpArrow)) && playerMana > 4 && playerHealth > 0) {
				GameObject newObject = Instantiate (lightBulletUp) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (0, 10), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (lightShoot);
			} else if (iceHover == true && (Input.GetKeyUp (KeyCode.UpArrow)) && playerMana > 4 && playerHealth > 0) {
				GameObject newObject = Instantiate (iceBulletUp) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y + .1f;
				bulletSprite.AddForce (new Vector2 (0, 10), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (iceShoot);
			}
			//bullets that go down hopefully
			if (fireHover == true && (Input.GetKeyUp (KeyCode.DownArrow)) && playerMana > 4 && playerHealth > 0) {
				GameObject newObject = Instantiate (fireBulletDown) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y - .3f;
				bulletSprite.AddForce (new Vector2 (0, -10), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (fireShoot);
			} else if (windHover == true && (Input.GetKeyUp (KeyCode.DownArrow)) && playerMana > 4 && playerHealth > 0) {
				GameObject newObject = Instantiate (windBulletDown) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y - .3f;
				bulletSprite.AddForce (new Vector2 (0, -10), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (windShoot);
			} else if (lightHover == true && (Input.GetKeyUp (KeyCode.DownArrow)) && playerMana > 4 && playerHealth > 0) {
				GameObject newObject = Instantiate (lightBulletDown) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y - .3f;
				bulletSprite.AddForce (new Vector2 (0, -10), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (lightShoot);
			} else if (iceHover == true && (Input.GetKeyUp (KeyCode.DownArrow)) && playerMana > 4 && playerHealth > 0) {
				GameObject newObject = Instantiate (iceBulletDown) as GameObject;
				SpriteRenderer objSprite = newObject.GetComponent<SpriteRenderer> ();
				Rigidbody2D bulletSprite = newObject.GetComponent<Rigidbody2D> ();
				Vector3 newObjPos = newObject.transform.position;
				newObjPos.x = currentPos.x;
				newObjPos.y = currentPos.y - .3f;
				bulletSprite.AddForce (new Vector2 (0, -10), ForceMode2D.Impulse);
				newObject.transform.position = newObjPos;
				playerMana = playerMana - 4;
				source.PlayOneShot (iceShoot);
			}
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

		if (playerMana < 20) {
			playerMana = playerMana + .8f*Time.deltaTime; //player mana regeneration!!!!
		} 
			
		if (playerHealth > 20) {
			playerHealth = 20;  //caps player hp at 20
			hpDisplay.sprite = currentHP [0];

		} else if (playerHealth < 20 && playerHealth >= 19){
			hpDisplay.sprite = currentHP [1];
		} else if (playerHealth < 19 && playerHealth >= 18){
			hpDisplay.sprite = currentHP [2];
		} else if (playerHealth < 18 && playerHealth >= 17){
			hpDisplay.sprite = currentHP [3];
		} else if (playerHealth < 17 && playerHealth >= 16){
			hpDisplay.sprite = currentHP [4];
		} else if (playerHealth < 16 && playerHealth >= 15){
			hpDisplay.sprite = currentHP [5];
		} else if (playerHealth < 15 && playerHealth >= 14){
			hpDisplay.sprite = currentHP [6];
		} else if (playerHealth < 14 && playerHealth >= 13){
			hpDisplay.sprite = currentHP [7];
		} else if (playerHealth < 13 && playerHealth >= 12){
			hpDisplay.sprite = currentHP [8];
		} else if (playerHealth < 12 && playerHealth >= 11){
			hpDisplay.sprite = currentHP [9];
		} else if (playerHealth < 11 && playerHealth >= 10){
			hpDisplay.sprite = currentHP [10];
		} else if (playerHealth < 10 && playerHealth >= 9){
			hpDisplay.sprite = currentHP [11];
		} else if (playerHealth < 9 && playerHealth >= 8){
			hpDisplay.sprite = currentHP [12];
		} else if (playerHealth < 8 && playerHealth >= 7){
			hpDisplay.sprite = currentHP [13];
		} else if (playerHealth < 7 && playerHealth >= 6){
			hpDisplay.sprite = currentHP [14];
		} else if (playerHealth < 6 && playerHealth >= 5){
			hpDisplay.sprite = currentHP [15];
		} else if (playerHealth < 5 && playerHealth >= 4){
			hpDisplay.sprite = currentHP [16];
		} else if (playerHealth < 4 && playerHealth >= 3){
			hpDisplay.sprite = currentHP [17];
		} else if (playerHealth < 3 && playerHealth >= 2){
			hpDisplay.sprite = currentHP [18];
		} else if (playerHealth < 2 && playerHealth >= 1){
			hpDisplay.sprite = currentHP [19];
		} else if (playerHealth < 1) {
			playerIsDead = true;
			hpDisplay.sprite = currentHP [20];

		}
		if (playerIsDead) { //shows death sprite for a bit and locks player action
			playerIsMoving = false;
			playerIsShooting = false;
			playerIsAttacking = false;
			timeSinceDeath = timeSinceDeath - 2 * Time.deltaTime;
		}
		if (timeSinceDeath <= 0 && levelOne == true) {
			Application.LoadLevel ("Game Over You suck"); //loads different game over scene so the player can go back to where they left off
		} else if (timeSinceDeath <= 0 && levelTwo == true){
			Application.LoadLevel ("Game Over2");
		} else if (timeSinceDeath <= 0 && levelThree == true){
			Application.LoadLevel ("game over3");
		} else if (timeSinceDeath <= 0 && finalBattle == true){
			Application.LoadLevel ("GameOverFinal");
		}
	

		//Mana display code
		if (playerMana > 20) {
			playerMana = 20;  //caps player mana at 20
			manaDisplay.sprite = currentMana [0];

		} else if (playerMana < 20 && playerMana >= 19){
			manaDisplay.sprite = currentMana [1];
		} else if (playerMana < 19 && playerMana >= 18){
			manaDisplay.sprite = currentMana [2];
		} else if (playerMana < 18 && playerMana >= 17){
			manaDisplay.sprite = currentMana [3];
		} else if (playerMana < 17 && playerMana >= 16){
			manaDisplay.sprite = currentMana [4];
		} else if (playerMana < 16 && playerMana >= 15){
			manaDisplay.sprite = currentMana [5];
		} else if (playerMana < 15 && playerMana >= 14){
			manaDisplay.sprite = currentMana [6];
		} else if (playerMana < 14 && playerMana >= 13){
			manaDisplay.sprite = currentMana [7];
		} else if (playerMana < 13 && playerMana >= 12){
			manaDisplay.sprite = currentMana [8];
		} else if (playerMana < 12 && playerMana >= 11){
			manaDisplay.sprite = currentMana [9];
		} else if (playerMana < 11 && playerMana >= 10){
			manaDisplay.sprite = currentMana [10];
		} else if (playerMana < 10 && playerMana >= 9){
			manaDisplay.sprite = currentMana [11];
		} else if (playerMana < 9 && playerMana >= 8){
			manaDisplay.sprite = currentMana [12];
		} else if (playerMana < 8 && playerMana >= 7){
			manaDisplay.sprite = currentMana [13];
		} else if (playerMana < 7 && playerMana >= 6){
			manaDisplay.sprite = currentMana [14];
		} else if (playerMana < 6 && playerMana >= 5){
			manaDisplay.sprite = currentMana [15];
		} else if (playerMana < 5 && playerMana >= 4){
			manaDisplay.sprite = currentMana [16];
		} else if (playerMana < 4 && playerMana >= 3){
			manaDisplay.sprite = currentMana [17];
		} else if (playerMana < 3 && playerMana >= 2){
			manaDisplay.sprite = currentMana [18];
		} else if (playerMana < 2 && playerMana >= 1){
			manaDisplay.sprite = currentMana [19];
		} else if (playerMana < 1) {
			manaDisplay.sprite = currentMana [20];

		}



		transform.position = currentPos;




		
	}
	void OnCollisionEnter2D (Collision2D gameObjectHittingme)
	{

		if (gameObjectHittingme.gameObject.tag == "Floor") { //player can only jump off certain surfaces
			Debug.Log ("hitting ground");
			hittingGround = true;

		}

		if (playerIsAttacking == false && (gameObjectHittingme.gameObject.tag == "Enemy")) { //taking damage from enemies 
			Debug.Log ("hurt");
			playerHealth = playerHealth - 5;

		}


	}

	void OnTriggerEnter2D (Collider2D triggerHittingMe){ //test triggers

	
		if (playerHealth < 20 && (triggerHittingMe.gameObject.tag == "Basic Potion")) { //heal when collide, they disappear after one use
			Debug.Log ("healed");
			playerHealth = playerHealth + 3;
			Destroy (triggerHittingMe.gameObject);

		}


		if (triggerHittingMe.gameObject.tag == "DEATH PANEL") { //instant death boxes send player HP to 0
			Debug.Log ("Ripperoni");
			playerHealth = 0;

		}

		if (triggerHittingMe.gameObject.tag == "Enemy Projectile" && playerIsCrouching == false) {
			Debug.Log ("getting shot");
			playerHealth = playerHealth - 2;
			Destroy (triggerHittingMe.gameObject);
		} else if (triggerHittingMe.gameObject.tag == "Enemy Projectile" && playerIsCrouching == true) {
			playerHealth = playerHealth;
			Debug.Log ("blocked");
		}
	}


}
