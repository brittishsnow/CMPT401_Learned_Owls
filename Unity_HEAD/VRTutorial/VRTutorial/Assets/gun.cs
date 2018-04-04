using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

	public GameObject bullet;
	public GameObject exitPoint;
	public int bulletSpeed = 100;
	public int direction = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) || OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f || OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f) {
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
		switch (direction) {
		case 1: 
			bulletInstance.GetComponent<Rigidbody> ().velocity = gameObject.transform.rotation * new Vector3 (bulletSpeed, 0, 0);
			break;
		case 2: 
			bulletInstance.GetComponent<Rigidbody> ().velocity = gameObject.transform.rotation * new Vector3 (0, bulletSpeed, 0);
			break;
		case 3: 
			bulletInstance.GetComponent<Rigidbody> ().velocity = gameObject.transform.rotation * new Vector3 (0, 0, bulletSpeed);
			break;
		case -1: 
			bulletInstance.GetComponent<Rigidbody> ().velocity = gameObject.transform.rotation * new Vector3 (-bulletSpeed, 0, 0);
			break;
		case -2: 
			bulletInstance.GetComponent<Rigidbody> ().velocity = gameObject.transform.rotation * new Vector3 (0, -bulletSpeed, 0);
			break;
		case -3: 
			bulletInstance.GetComponent<Rigidbody> ().velocity = gameObject.transform.rotation * new Vector3 (0, 0, -bulletSpeed);
			break;
		default:
			bulletInstance.GetComponent<Rigidbody> ().velocity = gameObject.transform.rotation * new Vector3 (bulletSpeed, 0, 0);
			break;
		}
	}


}
