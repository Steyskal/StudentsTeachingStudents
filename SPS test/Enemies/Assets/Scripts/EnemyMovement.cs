using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Transform player;               // Reference to the player's position.
	Animator animator;				// Reference to the player's animator.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	
	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player01").transform;
		animator = GetComponent<Animator>();
		nav = GetComponent <NavMeshAgent> ();
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// ... set the destination of the nav mesh agent to the player.
		nav.SetDestination(player.position);
		// ... set the Speed parameter inside Animator Controller
		animator.SetFloat("Speed", nav.velocity.magnitude);
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Player01"){
			// ... stop the nav mesh agent
			nav.Stop();
			// ... set the PlayerDead parameter inside Animator Controller
			animator.SetTrigger("PlayerDead");
			// ... call players Death function
			Player_controller playerController = player.gameObject.GetComponent<Player_controller>();
			playerController.Death();
		}
	}
}
