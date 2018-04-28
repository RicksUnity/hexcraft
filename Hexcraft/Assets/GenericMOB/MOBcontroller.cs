using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MOBcontroller : MonoBehaviour {
	private Rigidbody rb;


	// Player Constants
	private GameObject player;
	private Vector3 playerPos;


	// Spawning constants
	Vector3 wanderBox = new Vector3(30f, 2f, 30f);
	Vector3 target;

	// MOB Constants 
	private float wanderspeed = 12f;
	private float jumpForce = 4f;
	private float awarenessRadius = 10f;
	private float cheekyPush = 3f;
	private bool Jummped = false;

	// MOB Variables 
	private bool chasing = false;
	private int NewTargetTimer = 0;
	new Vector3 spawnPos;




	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> (); // rigid body needed for physics
		spawnPos = transform.position; // Initial spawn postion
		target = WanderPoint (spawnPos, wanderBox);  // Initial spawn box

		//  ----- creating object for the player, chase mechanism -----
		player = GameObject.Find ("Player");

	}
	// ---------- MOB Trigger -------------
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			target = playerPos;
			chasing = true;
			print (chasing);
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			spawnPos = transform.position;
			target = WanderPoint (spawnPos, wanderBox);
			chasing = false;
			print (chasing);
		}
	}



	// -------- Update is called once per frame -----------
	void Update () {
		// counter for New wander target
		NewTargetTimer ++;

		//  Updating the player postion
		playerPos = player.transform.position;

		// ---- Flat Variables for target proximity calculations. ---- 
		Vector3 FlatTarget = new Vector3(target.x, 0f, target.z);
		Vector3 FlatPosition = new Vector3(transform.position.x, 0f, transform.position.z);
		float distance = Vector3.Distance (target, transform.position);  // Variable in force calculations.

		// Checking for target Poximity
		float flatdistance = Vector3.Distance (FlatTarget, FlatPosition);

		// - - - - Checks chasing, makes new target when in range.
		//if (chasing = false) {
			if (flatdistance < 3f) {
				target = WanderPoint (spawnPos, wanderBox);
			}
		
		// -------  MOB Player chasing ---------
		if (chasing == true) {
			target = playerPos;

		}
			

		// Keep MOB pointing at target
		Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.z);
		transform.LookAt (targetPosition);

		// Flat Vector3 towards target "Heading"
		Vector3 Heading = ((FlatTarget - FlatPosition)/flatdistance);

		// creating force towards target posion
		if (MobOnGround.onGround == true) {
			rb.AddForce ((Heading) * wanderspeed);
		} 

		// Jumping Mechanics
		if (MobOnGround.onGround == true && MobBlocked.pathBlocked == true)
		{
			rb.AddForce (0, jumpForce, 0, ForceMode.Impulse);
			MobOnGround.onGround = false;
			MobBlocked.pathBlocked = false;
			Jummped = true;
		}
		// Cheaky push triggered after jumping. 
		if (rb.velocity.y < -0.1f & Jummped == true) {
			rb.AddForce ((Heading * cheekyPush), ForceMode.Impulse);
			Jummped = false;
		}
			
		// - - - -  Refreshing wandering target point
		if (NewTargetTimer % 500 == 0 & chasing == false) {
			target = WanderPoint (spawnPos, wanderBox); // Creates new wander point
		}
	}

	// Function creates random point within a 2D box
	private Vector3 WanderPoint(Vector3 center, Vector3 size)
	{ 
		return center + new Vector3 (
			(Random.value - 0.5f) * size.x,
			(0f),
			(Random.value - 0.5f) * size.z);
	}


	// Wire spheres drawn on MOB
	public void OnDrawGizmos()
	{
		// Initial spawn position
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (spawnPos, wanderBox);
		//Target Marker
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (target, 0.5f);
		//Chase Radius 
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, awarenessRadius);

	}
}
