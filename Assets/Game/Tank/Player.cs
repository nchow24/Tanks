using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {
	GameObject GM;
	private GameManager GMScript;

	private Rigidbody2D myRigidBody;
	[SerializeField]
	private float movementSpeed;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		GM = GameObject.Find("GameManager");
		GMScript = GM.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxisRaw ("Horizontal");
		HandleMovement (horizontal);
		checkBounds ();

        // Cheat code to fill up on ammo
        if (Input.GetKeyDown("b"))
        {
            for (int i = 0; i < 9; i++) {
                GMScript.tankInfos[GMScript.playerTurn].ammo[i] = 99;
            }
        }

        // Cheat code to kill self
        if (Input.GetKeyDown("k"))
        {
            GMScript.tankInfos[GMScript.playerTurn].damageTank(1000);
            GMScript.nextTurn();
        }

        // Cheat code to fill up on money, should be in shop screen
        if (Input.GetKeyDown("m"))
        {
            GMScript.tankInfos[GMScript.playerTurn].funds = 99999;
        }
    }

	private void HandleMovement(float horizontal){
		myRigidBody.velocity = new Vector2 (horizontal * movementSpeed, myRigidBody.velocity.y);
		Collider2D temp = GameObject.FindGameObjectWithTag ("Terrain").GetComponent<Collider2D>();
		//if (myRigidBody.IsTouching(temp))
			//myRigidBody.gravityScale = 0;
		//else
			//myRigidBody.gravityScale = 1;

		// If fuel = 0, can't move
		if (GMScript.tankInfos [GMScript.playerTurn].fuel <= 0)
			myRigidBody.velocity = new Vector2 (0, myRigidBody.velocity.y);

		// This segment prevents the player from moving other tanks
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		float width = this.GetComponent<SpriteRenderer> ().bounds.size.x;
		foreach (GameObject player in players){
			float otherPosition = player.transform.position.x;
			if((Mathf.Abs(transform.position.x - otherPosition) < width*0.9) && (this.transform != player.transform)){
				// Case where other player is to the right
				if (player.transform.position.x > transform.position.x) {
					myRigidBody.velocity = new Vector2 (Mathf.Min(horizontal * movementSpeed,0), myRigidBody.velocity.y);
				}
				// Case where other player is to the left
				else {
					myRigidBody.velocity = new Vector2 (Mathf.Max(horizontal * movementSpeed,0), myRigidBody.velocity.y);
				}
			}
		}

		if (Mathf.Abs(myRigidBody.velocity.x) > 0)
			GMScript.tankInfos [GMScript.playerTurn].fuel = Mathf.Max(0, GMScript.tankInfos [GMScript.playerTurn].fuel - 0.2f);
	}

	// This method prevents the tanks from tipping over too much
	// also prevents tank from moving off-screen
	private void checkBounds(){
		float angle = transform.eulerAngles.z;

		while ((angle > 42 && angle < 180) || (angle < 318 && angle > 180)) {
			angle -= 1;
			transform.eulerAngles = new Vector3 (0,0,angle);
		}

		float max = GameObject.FindGameObjectWithTag ("Terrain").GetComponent<SpriteRenderer>().bounds.max.x;
		float min = GameObject.FindGameObjectWithTag ("Terrain").GetComponent<SpriteRenderer>().bounds.min.x;
		float extents = this.GetComponent<SpriteRenderer> ().bounds.extents.x;

		if ((transform.position.x + extents) > max) {
			transform.position = new Vector3 (max - extents, transform.position.y, transform.position.z);
		} else if ((transform.position.x - extents) < min) {
			transform.position = new Vector3 (min + extents, transform.position.y, transform.position.z);
		}
	}
}
