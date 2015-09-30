using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public Transform startingPosition;



	public static int playerLives = 3;
	public static bool playerRespawned = false;

	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player01");
		startingPosition = GameObject.FindGameObjectWithTag("StartingPosition").transform;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayerRespawn(){
		playerLives--;
		Debug.Log("Lives: " + playerLives);

		if(playerLives <= 0){
			Debug.Log ("Game Over!");
			return;
		}

		player.transform.position = startingPosition.position;
		playerRespawned = true;
	}
}
