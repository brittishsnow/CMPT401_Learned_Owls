using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {


	public GameObject questionHandler;


	public Canvas canvas;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if (OVRInput.Get(OVRInput.Button.Start) && canvas.enabled) {
			canvas.enabled = false;
			questionHandler.SendMessage ("loadGame");
		}
	}


}
