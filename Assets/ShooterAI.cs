using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour {

    public GameObject player;
    public GameObject projectile;
    public GameObject shootingPoint;

	public bool gamePaused = false;

    public float distanceFromPlayer;
    public float maxRange = 10f;
    public float projectileSpeed = 2f;
    public float projectileTimer;
    public float projectileCoolDown = 1f;           //should hopefully fire once per second


    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	
	void Update () {
        distanceFromPlayer = transform.position.x - player.transform.position.x;

		if (gamePaused == false && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = true;
		} else if (gamePaused == true && Input.GetKeyUp (KeyCode.P)) {
			gamePaused = false;
		}

        ShootPlayer();
	}

    void ShootPlayer()
    {
        projectileTimer += Time.deltaTime;          //projectile timer starts ticking up at game start; will fire at player as soon as player enters maxRange

        if (projectileTimer >= projectileCoolDown && gamePaused == false)
        {
            Vector2 projectileDirection = player.transform.position - transform.position;
            //projectileDirection.Normalize();
            distanceFromPlayer = Mathf.Abs(transform.position.x - player.transform.position.x);     //Must add mathf.abs to get absolute value otherwise distancefromplayer will always be less than maxrange in negative

			if (distanceFromPlayer <= maxRange && gamePaused == false)
            {
                    print("playerWithinRange");         //MOFOKING WORKS BABY
                    GameObject projectileClone;
                    projectileClone = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation) as GameObject;        //instantiates the projectile at shooting point gameobject
                    projectileClone.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileSpeed;               

                    projectileTimer = 0;        //resets timer so shooter can't spam 
                
            }

        }
    }
}
