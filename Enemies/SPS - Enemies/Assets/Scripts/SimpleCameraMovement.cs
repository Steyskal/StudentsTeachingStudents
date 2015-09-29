using UnityEngine;
using System.Collections;

public class SimpleCameraMovement : MonoBehaviour {
	public Transform target;
	
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position + offset;
		//transform.position = Vector3.Lerp(transform.position, target.transform.position + offset,0.11f);

	}
}
