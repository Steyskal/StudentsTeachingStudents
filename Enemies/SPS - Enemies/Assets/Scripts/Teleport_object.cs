using UnityEngine;
using System.Collections;

public class Teleport_object : MonoBehaviour {

	Vector3 vek;
	float z_var;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other){

		if (GameObject.Find ("Player").transform.position.z > 0)
			z_var = -33;

		if (GameObject.Find ("Player").transform.position.z < 0)
			z_var = 33;

		vek.Set (GameObject.Find ("Player").transform.position.x, 0.702f, z_var);
		other.transform.position= vek;

	}

}
