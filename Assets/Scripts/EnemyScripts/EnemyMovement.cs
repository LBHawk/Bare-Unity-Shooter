using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    //public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    //Animator anim;                      // Reference to the animator component.
    Transform player;                  // Reference to the player
    //float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    NavMeshAgent nav;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    void Awake()
    {
        // Create a layer mask for the floor layer.
        //floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        //anim = GetComponent<Animator>();
        //enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
        }
        else
        {
            // disable the nav mesh agent.
            nav.enabled = false;
        }
    }
}
