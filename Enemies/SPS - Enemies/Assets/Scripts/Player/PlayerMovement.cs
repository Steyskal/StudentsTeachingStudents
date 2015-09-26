using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6.0f;
	public float jumpHeight = 5.0f;
	public float distanceToGround;

	Vector3 movement;
	Animator animate;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLenght = 100.0f;

	// Similar to Start() but gets called even if the script is not enabled
	void Awake(){
		floorMask = LayerMask.GetMask ("Floor");
		animate = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		distanceToGround = GetComponent<Collider>().bounds.extents.y;
	}

	void FixedUpdate(){
		float horizontalMovement = Input.GetAxisRaw ("Horizontal");
		float verticalMovement = Input.GetAxisRaw ("Vertical");

		Move (horizontalMovement, verticalMovement);
		Turning ();
		Animating (horizontalMovement, verticalMovement);
	}

	bool IsGrounded(){
		// otherwise the ray is cast at the very bottom of the player (underneath the floor)
		Vector3 offset = new Vector3 (0.0f, 0.2f, 0.0f);
		return Physics.Raycast (transform.position + offset, -Vector3.up, distanceToGround + 0.1f);
	}
		
	void Move(float horizontalMovement, float verticalMovement){
		Vector3 jump = new Vector3 (horizontalMovement, jumpHeight, verticalMovement);
		if (Input.GetButtonDown ("Jump") && IsGrounded()) {
			playerRigidbody.velocity = jump;
		}

		movement.Set (horizontalMovement, 0.0f, verticalMovement);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning(){
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLenght, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0.0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating(float horizontalMovement, float verticalMovement){
		bool walking = horizontalMovement != 0.0f || verticalMovement != 0.0f;
		animate.SetBool ("IsWalking", walking);
	}
}