using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControllerScript : MonoBehaviour {

	public GameObject zombieA;
	public GameObject zombieB;
	public GameObject zombieC;
	public GameObject zombieD;
	public GameObject extraZombieModel;
	public GameObject correct;


	public List<Transform> startingPositions = new List<Transform>();

	private bool waveIsGoing = false;
	private bool ending = false;
	private float timer = 0;
	List<GameObject> extraZombies = new List<GameObject>();
	private bool waitForExtras = false;

	// Use this for initialization
	void Start () {
		
	}

	public void Correct() {
			correct.SetActive (true);
		zombieA.GetComponent<ZombieAnimationScript> ().Die();
		zombieB.GetComponent<ZombieAnimationScript> ().Die();
		zombieC.GetComponent<ZombieAnimationScript> ().Die();
		zombieD.GetComponent<ZombieAnimationScript> ().Die();
		waitForExtras = true;
	}
	// Update is called once per frame
	void Update () {
		
		//DEBUG KEY CODES:
		if (waitForExtras) {
			timer += Time.deltaTime;

			if (timer > 1) {
				correct.SetActive (false);
			}
			if (!checkIfExtrasAlive ()) {
				waitForExtras = false;

				EndWave (true);
			}
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			Debug.Log ("q pressed");
			zombieA.GetComponent<ZombieAnimationScript> ().Die();
			zombieB.GetComponent<ZombieAnimationScript> ().Die();
			zombieC.GetComponent<ZombieAnimationScript> ().Die();
			zombieD.GetComponent<ZombieAnimationScript> ().Die();
		}


		if (Input.GetKeyDown (KeyCode.R)) {
			StartWave (4.0F);
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			EndWave (false);
		}

		if (Input.GetKeyDown (KeyCode.Y)) {
			AddZombie (1);
		}

		if (ending) {
			timer += Time.deltaTime;
				if (timer > 3) {
				correct.SetActive (false);

					timer = 0;
					ending = false;
					zombieA.GetComponent<ZombieAnimationScript> ().ResetPosition ();
					zombieB.GetComponent<ZombieAnimationScript> ().ResetPosition ();
					zombieC.GetComponent<ZombieAnimationScript> ().ResetPosition ();
					zombieD.GetComponent<ZombieAnimationScript> ().ResetPosition ();

					foreach (GameObject zombie in extraZombies) {
						Destroy (zombie);
					}

					extraZombies.Clear ();
				}
		}
	}

	public void StartWave(float waveSpeed) {
		Debug.Log ("Starting wave...");
		waveIsGoing = true;
		//Get random starting positions for each zombie
		List<Transform> waveStartPos = GenerateStartPos(4);
					

		//Zombie A set starting position and moving
		//zombieA.transform.position = waveStartPos[0].position;

		SetSpeed (waveSpeed);

		zombieA.GetComponent<ZombieAnimationScript> ().StartAtPosition (waveStartPos[0].position);
		zombieB.GetComponent<ZombieAnimationScript> ().StartAtPosition (waveStartPos[1].position);
		zombieC.GetComponent<ZombieAnimationScript> ().StartAtPosition (waveStartPos[2].position);
		zombieD.GetComponent<ZombieAnimationScript> ().StartAtPosition (waveStartPos[3].position);

	}

	public void EndWave(bool c) {
		

		waveIsGoing = false;

		zombieA.GetComponent<ZombieAnimationScript> ().Die();
		zombieB.GetComponent<ZombieAnimationScript> ().Die();
		zombieC.GetComponent<ZombieAnimationScript> ().Die();
		zombieD.GetComponent<ZombieAnimationScript> ().Die();

		ending = true;

	}

	public void SetSpeed(float newSpeed) {
		zombieA.GetComponent<ZombieAnimationScript> ().SetSpeed (newSpeed);
		zombieB.GetComponent<ZombieAnimationScript> ().SetSpeed (newSpeed);
		zombieC.GetComponent<ZombieAnimationScript> ().SetSpeed (newSpeed);
		zombieD.GetComponent<ZombieAnimationScript> ().SetSpeed (newSpeed);
	}
	   
	public void AddZombie(int num) {
		List<Transform> zombieStartPos = GenerateStartPos(num);
		Debug.Log ("Positions : " + zombieStartPos.Count);
		for (int i = 0; i < num; i++) {
			Debug.Log ("Add zombie " + i + " out of " + num);
			GameObject extraZombie = (GameObject)Instantiate (extraZombieModel);
			extraZombie.transform.position = zombieStartPos [i].position;
			extraZombie.GetComponent<ZombieAnimationScript> ().StartMoving ();
			extraZombies.Add (extraZombie);
			}
	}

	private List<Transform> GenerateStartPos(int num) {
		List<Transform> tempPositions = new List<Transform>(startingPositions);
		List<Transform> waveStartPos = new List<Transform>();

		for (int i = 0; i < num; i++) {
			int posId = Random.Range (0, tempPositions.Count);
			Debug.Log (posId);
			waveStartPos.Add (tempPositions [posId]);
			tempPositions.RemoveAt (posId);
		}


		return waveStartPos;
	}

	public bool checkIfExtrasAlive() {
		int alive = 0;
		GameObject[] extras = GameObject.FindGameObjectsWithTag ("Extra");
		for (int i = 0; i < extras.Length; i++) {
			if (!extras [i].GetComponent<target> ().dead) {
				alive++;
			}
		}
		Debug.Log ("Alive : " + alive + " out of " +  extras.Length + " " + (alive > 1));
		return (alive > 1);
	}
}
