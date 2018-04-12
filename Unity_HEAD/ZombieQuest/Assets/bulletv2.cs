using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletv2 : MonoBehaviour {

	public GameObject bloodFx;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player") {
			Instantiate (bloodFx, transform);
			Debug.Log ("Bullet hit " + other.name);
			other.SendMessage ("hit");
			Destroy (gameObject);
		}
	}
}