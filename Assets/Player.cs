using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 5f;
	public float jumpSpeed = 3f;

	public AudioClip jumpSound;


	public int TimeSinceWin;
	public int TimeSinceHurt;

	public bool onGround = true;
	public bool pJump = false;
	public bool pPunch = false;
	public bool jKick = false;
	public bool pHurt = false;
	public bool pBlock = false;
	public bool pDead = false;
	public bool pWin = false;

	public Sprite playerBase;
	public Sprite playerJump;
	public Sprite playerPunch;
	public Sprite jumpKick;
	public Sprite playerHurt;
	public Sprite playerBlock;
	public Sprite playerDead;
	public Sprite playerWin;
	private SpriteRenderer spriteRenderer; 


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>(); 
		if (spriteRenderer.sprite == null) 
			spriteRenderer.sprite = playerBase; 
	}

	// Update is called once per frame
	void Update () {

		Vector3 currentPos = transform.position;
		Vector3 currentScale = transform.localScale;

		AudioSource ourAudio = GetComponent<AudioSource> ();
		ourAudio.Play ();

		if (Input.GetKey (KeyCode.W) ) {
			
			onGround = false;
			pJump = true;

		} else {
			pJump = false;
			onGround = true;
		}

		if (Input.GetKey (KeyCode.D)) {
			currentPos.x += moveSpeed * Time.deltaTime;
			//currentScale.x = -currentScale.x;
			//onGround = true;
			//pJump = false;
			pPunch = false;

		}
		else if (Input.GetKey (KeyCode.A)) {
			currentPos.x -= moveSpeed * Time.deltaTime;
			//currentScale.x = -currentScale.x;
			//onGround = true;
			//pJump = false;
			pPunch = false;
		}

		if (Input.GetKey (KeyCode.S)) {
			pPunch = false;
			jKick = false;
			pJump = false;
			onGround = false;
			pBlock = true;
			pHurt = false;


		} else {
			pBlock = false;

		}

		if (Input.GetKey (KeyCode.Space)) {
			pPunch = true;
			jKick = false;
			pJump = false;
			onGround = false;
			pHurt = false;

		
		} else {
			pPunch = false;

		}

		if (onGround) {
			spriteRenderer.sprite = playerBase;

		} 
		if (pPunch) {
			spriteRenderer.sprite = playerPunch;
		} 
			
		if (pJump) {
			spriteRenderer.sprite = playerJump;
			//ourAudio.PlayOneShot (jumpSound);
		}

		if (pBlock) {
			spriteRenderer.sprite = playerBlock;
		}

		if (pHurt) {
			spriteRenderer.sprite = playerHurt;
			TimeSinceHurt++;
		}

		if (pWin) {
			spriteRenderer.sprite = playerWin;
			TimeSinceWin++;
		}
		if (TimeSinceWin > 30) {
			Application.LoadLevel ("Victory Screen");
		}

		if (TimeSinceHurt > 30) {
			Application.LoadLevel ("GameOver");
		}




		transform.localScale = currentScale;
		transform.position = currentPos;
	}
	void OnCollisionEnter2D (Collision2D gameObjectHittingme)
	{
		

		if ((gameObjectHittingme.gameObject.tag == "Treasure")) {
			Debug.Log ("victory");
			pWin = true;

		} else {
			pWin = false;
		}

		if (pBlock == false && (gameObjectHittingme.gameObject.tag == "Enemy")) {
			Debug.Log ("hurt");
			pHurt = true;
		} else {
			pHurt = false;
		}
	}

}
