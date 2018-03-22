using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

	public GameObject bullet;
	public GameObject exitPoint;
	public int bulletSpeed = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Pressed left click.");
			shoot ();
		}

		if (Input.GetMouseButtonDown (1)) {
			Debug.Log ("Pressed right click.");
		}

		if (Input.GetMouseButtonDown (2)) {
			Debug.Log ("Pressed middle click.");
		}
	}

	private void shoot() {
		Vector3 pos = gameObject.transform.position;
		GameObject bulletInstance = GameObject.Instantiate (bullet, exitPoint.transform.position, gameObject.transform.rotation);
		bulletInstance.GetComponent<Rigidbody> ().velocity = gameObject.transform.rotation * new Vector3 (-bulletSpeed,0,0);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Bullet striked something");
		Destroy (gameObject);
	}
}
