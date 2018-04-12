using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour {

	public GameObject questionHandler;
	public GameObject zombieController;
	public bool correctTarget = false;
	public bool isExtra = true;
	public bool dead = false;

	private bool nextRound = false;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (nextRound && !zombieController.GetComponent<ZombieControllerScript> ().checkIfExtrasAlive()) {
			timer += Time.deltaTime;
			if (timer > 5) {
				timer = 0;
				nextRound = false;
				questionHandler.SendMessage ("loadNextQuestion");
			}
		}
	}

	void OnTriggerEnter(Collider bullet) {
		if (bullet.CompareTag ("bullet")) {
			hit ();
		}
		if (bullet.CompareTag("Player")) {
			endGame();
		}
	}

	public void revive() {
		dead = false;
	}

	public void hit() {
		this.SendMessage ("Die");
		if (!dead) {
			Debug.Log ("Not Dead Yet");
			if (!isExtra) {
				Debug.Log ("Not an extra");
				if (correctTarget) {
					correctAnswer ();
				} else {
					wrongAnswer ();
				}
			}
		}
		dead = true;
	}

	void correctAnswer() {
		Debug.Log ("Correct!");
		zombieController.SendMessage ("Correct");
		nextRound = true;
	}

	void wrongAnswer() {
		Debug.Log ("Wrong!");
		zombieController.SendMessage ("Wrong");
		zombieController.SendMessage ("AddZombie", 8);

	}

	public void isCorrect(bool b) {
		correctTarget = b;
	}

	void endGame () {
		questionHandler.SendMessage ("endGame");
	}

}
