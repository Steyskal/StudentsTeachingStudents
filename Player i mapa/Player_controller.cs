using UnityEngine;
using System.Collections;

public class Player_controller : MonoBehaviour {

	public float speed;
	Rigidbody playerRigidbody;

	Vector3 movement;
	float lastH;
	float lastV;
	Animator anim;

	void Awake(){

		playerRigidbody = GetComponent <Rigidbody> ();
		anim = GetComponent <Animator> ();

	}

	void FixedUpdate(){

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		if (moveHorizontal != 0) {
			lastH = moveHorizontal;
			lastV = 0;
		}

		if (moveVertical != 0) {
			lastV = moveVertical;
			lastH = 0;
		}

		Move ();
		Rotate ();
		Animating ();
	}
	
	void Move(){

		movement.Set (lastH, 0.0f, lastV);
		movement = movement.normalized * speed;

		playerRigidbody.MovePosition (transform.position + movement);

	}

	void Rotate(){

		if(lastH > 0) playerRigidbody.rotation= Quaternion.Euler(0.0f, 0, 0.0f);
		if(lastH < 0) playerRigidbody.rotation= Quaternion.Euler(0.0f, 180, 0.0f);

		if(lastV > 0) playerRigidbody.rotation= Quaternion.Euler(0.0f, 270, 0.0f);
		if(lastV < 0) playerRigidbody.rotation= Quaternion.Euler(0.0f, 90, 0.0f);

	}

	void Animating(){

		bool walking = false;
		
		if (lastH != 0 || lastV != 0)
			walking = true;

		anim.SetBool ("IsWalking", walking);

	}
}
