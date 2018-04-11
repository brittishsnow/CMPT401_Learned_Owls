using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotkeys : MonoBehaviour {

	public GameObject questionHandler;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R) || OVRInput.Get(OVRInput.Button.Four)) {
			restartGame ();
		}
		if (Input.GetKeyDown (KeyCode.G) || OVRInput.Get(OVRInput.Button.One)) {
			resetGun ();
		}
		if (Input.GetKeyDown (KeyCode.T) || OVRInput.Get(OVRInput.Button.Three)) {
			resetPlayer ();
		}
	}

	private void restartGame() {
		questionHandler.SendMessage("restart");
	}

	private void resetGun() {
		questionHandler.SendMessage("resetGun");
	}

	private void resetPlayer() {
		questionHandler.SendMessage("resetPlayer");
	}
		
}
