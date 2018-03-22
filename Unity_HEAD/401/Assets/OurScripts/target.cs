using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour {

	public bool correctTarget = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider bullet) {
		if (bullet.CompareTag ("bullet")) {
			if (correctTarget) {
				correctAnswer ();
			} else {
				wrongAnswer ();
			}
		}
	}

	void correctAnswer() {
		Debug.Log ("Correct!");
	}

	void wrongAnswer() {
		Debug.Log ("Wrong!");
	}
}
