using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationScriptV2 : MonoBehaviour {

	Animator anim;

	public float speed;
	public Transform playerCharacter;
	private bool canMove = false;
	private bool isDead = true;
	private Vector3 initialPosition;
	private Quaternion initialRotation;

	int walkTrigger = Animator.StringToHash("WalkTrigger");
	int dieTrigger = Animator.StringToHash("DieTrigger");
	int idleTrigger = Animator.StringToHash("IdleTrigger");
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Awake() {
		anim = GetComponent<Animator> ();
		gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
		anim.speed = speed;
		//anim.SetTrigger (walkTrigger);
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

	}



	public void Die () {
		if(!isDead) {
			Debug.Log ("dying...");
			CapsuleCollider collider = GetComponent<CapsuleCollider> ();
			collider.enabled = false;
			anim.SetTrigger (dieTrigger);
			//yield return new WaitForSeconds (1);
			isDead = true;

			gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false; //stop moving towards player
		}
	}

	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
		anim.speed = speed;
	}


	public void ResetPosition() {
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		StopMoving();
	}

	public void StartAtPosition(Vector3 pos) {
		transform.position = pos;
		transform.rotation = initialRotation;
		StartMoving ();
	}

	public void StartMoving() {
		Debug.Log ("moving zombie, triggering anim");
		
		CapsuleCollider collider = GetComponent<CapsuleCollider> ();
		collider.enabled = true;
		gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = true;
		anim.StopPlayback ();
		anim.SetTrigger (walkTrigger);
		isDead = false;
	}
	
	public void StopMoving() {
		//anim.StopPlayback ();
		anim.SetTrigger (idleTrigger);
		gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
	}
}