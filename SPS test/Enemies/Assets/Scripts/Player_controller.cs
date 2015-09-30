using UnityEngine;
using System.Collections;

public class Player_controller : MonoBehaviour {

	public float speed;				// Brzina kojom će se igrač kretati
	public float lastSpeed;
	Rigidbody playerRigidbody;		// Referenca na Rigidbody komponentu lika

	Vector3 movement;				// Vektor u koji spremamo smjer kretanja lika
	float lastH;					// Varijabla u koju spremamo kretanje po X osi
	float lastV;					// Varijabla u koju spremamo kretanje po Z osi
	Animator anim;					// Referenca na Animator komponentu lika

	void Awake(){					// Funkcija koja se poziva na početku učitavanja skripte

		playerRigidbody = GetComponent <Rigidbody> ();	// Pripajanje komponente Rigidbody na "playerRigidbody" varijablu
		anim = GetComponent <Animator> ();				// Pripajanje komponente Animator na "anim" varijablu

	}

	void FixedUpdate(){				// Funkcija koja se poziva na svakom kadru(frame-u) i koristi se kada radimo sa Rigidbody komponentama

		// Spremamo unesene osi kretanja
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		// Ako smo pomaknuli lika po osi X, spremamo tu vrijednost u varijablu "lastH", a kretanje po Z osi prekidamo
		if (moveHorizontal != 0) {
			lastH = moveHorizontal;
			lastV = 0;
		}

		// Ako smo pomaknuli lika po osi Z, spremamo tu vrijednost u varijablu "lastV", a kretanje po X osi prekidamo
		if (moveVertical != 0) {
			lastV = moveVertical;
			lastH = 0;
		}

		Move ();			// Pozivamo funciju za kretanje lika
		Rotate ();			// Pozivamo funkciju za rotatiranje lika
		Animating ();		// Pozivamo funkciju za animacije lika
	}
	
	void Move(){			// Funkcija koja prema varijablama "lastH" i "lastV" pomičemo lika

		movement.Set (lastH, 0.0f, lastV);			// Postavlja X, Y, Z komponete prema unesenim vrijednostima i na taj  
													// način stvara smjera vektora u 3D prostoru
		movement = movement.normalized * speed;		// Pretvaramo varijablu(vektor) "movement" u jedinični vektor i množimo ga sa varijablom "speed"

		// Trenutnoj poziciji lika dodajemo vektor "movement" i na taj način mijenjamo smjer kretanja lika
		playerRigidbody.MovePosition (transform.position + movement);

	}

	void Rotate(){			// Funkcija koja s obzirom na smjer kretanja lika okreće istog prema tom smjeru

		// Rotiramo lika po osi Y ovisno o smjeru kretanja
		if(lastH > 0) playerRigidbody.rotation= Quaternion.Euler(0.0f, 0, 0.0f);
		if(lastH < 0) playerRigidbody.rotation= Quaternion.Euler(0.0f, 180, 0.0f);

		if(lastV > 0) playerRigidbody.rotation= Quaternion.Euler(0.0f, 270, 0.0f);
		if(lastV < 0) playerRigidbody.rotation= Quaternion.Euler(0.0f, 90, 0.0f);

	}

	void Animating(){		// Funkcija koja pokreće i zaustavlja animacije lika

		bool walking = false;

		// Ako smo napravili bilo kakav pomak lika, varijabla "walking" postaje true
		if (lastH != 0 || lastV != 0)
			walking = true;

		// Ako je varijabla "walking" true, uvjet "IsWalking" je ispunjen i događa se prijelaz iz jednog stanja u drugi.
		// U ovom slučaju se događa prijelaz iz stanja Idle u stanje(animaciju) Eat.
		anim.SetBool ("IsWalking", walking);

	}

	public void Death(){
		lastSpeed = speed;
		// ... stop the player
		speed = 0.0f;

		// ... set the Dead parameter inside Animation Controller
		anim.SetTrigger("Dead");

		// ... call a method after certain amount of time
		Invoke ("Die", 2.0f);


	}

	void Die(){
		// ... remove players collision property
		GetComponent<SphereCollider>().isTrigger = true;

		Invoke ("PlayerRespawn", 2.0f);

		// ... destroy this game object after a certain amount of time
		//Destroy(gameObject, 2.0f);
	}

	void PlayerRespawn(){
		lastH = 0.0f;
		lastV = 0.0f;
		speed = lastSpeed;
		anim.SetTrigger("Respawn");
		GetComponent<SphereCollider>().isTrigger = false;

		GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		gameManager.PlayerRespawn();

	}

}
