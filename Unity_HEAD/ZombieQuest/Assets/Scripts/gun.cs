using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class gun : MonoBehaviour {

	public GameObject bullet;
	public GameObject exitPoint;
	public OVRGrabber rightHand;
	public OVRGrabber leftHand;
	public bool auto = false;
	public float fireRate = 0.5f;
	public int bulletSpeed = 100;
	public int direction = 1;
	AudioSource gunShot;
	
	private bool trigger = false;
	private bool prevTrigger = false;
	private float timer = 0;
	

	// Use this for initialization
	void Start () {
		gunShot = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		trigger = false;
		if(GetComponent<OVRGrabbable>().isGrabbed) {
			if((GetComponent<OVRGrabbable>().grabbedBy == rightHand && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f) || (GetComponent<OVRGrabbable>().grabbedBy == leftHand && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f)) {
				Debug.Log ("Pressed left click.");
				if(auto && timer > fireRate) {
					shoot ();
				}
				else if (!auto && timer > fireRate && !prevTrigger){
					shoot ();
				}
				trigger = true;
			}
		}
		prevTrigger = trigger;


		if (Input.GetMouseButtonDown (1)) {
			Debug.Log ("Pressed right click.");
		}

		if (Input.GetMouseButtonDown (2)) {
			Debug.Log ("Pressed middle click.");
		}
		
		timer += Time.deltaTime;
	}

	private void shoot() {
		timer = 0;
		Vector3 pos = gameObject.transform.position;
		gunShot.Play();
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
