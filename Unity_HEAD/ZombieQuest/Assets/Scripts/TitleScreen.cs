using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

	public float time = 4;
	public GameObject questionHandler;

	private float timer = 0;
	public Canvas canvas;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > time) {
			canvas.enabled = false;
			questionHandler.SendMessage ("loadGame");
		}
	}


}
