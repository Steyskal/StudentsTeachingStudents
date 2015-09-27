using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Transform player;               // Reference to the player's position.
	Animator animator;				// Reference to the player's animator.
	NavMeshAgent nav;               // Reference to the nav mesh agent.

	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		animator = GetComponent<Animator>();
		nav = GetComponent <NavMeshAgent> ();
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// ... set the destination of the nav mesh agent to the player.
		nav.SetDestination (player.position);

		// ... set the Speed parameter inside Animator Controler
		animator.SetFloat("Speed", nav.velocity.magnitude);
	}
}
