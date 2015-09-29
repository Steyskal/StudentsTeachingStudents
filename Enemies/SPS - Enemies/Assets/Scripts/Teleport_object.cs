using UnityEngine;
using System.Collections;

public class Teleport_object : MonoBehaviour {

	Vector3 vek;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other){

		vek.Set (GameObject.Find ("Player").transform.position.x, 0.702f, 34.8f);

		other.transform.position= vek;

	}

}
