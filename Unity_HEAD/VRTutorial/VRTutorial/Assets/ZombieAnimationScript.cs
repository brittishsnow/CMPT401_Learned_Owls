using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationScript : MonoBehaviour {

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
		anim.speed = speed;
		//anim.SetTrigger (walkTrigger);
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		SetCanMove (false);
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo currentAnim = anim.GetCurrentAnimatorStateInfo (0);


		//anim.SetTrigger (walkHash);
		//if (Input.GetKeyDown (KeyCode.Q)) {



		if (canMove) {
			Move ();
		}
	}

	void Move () {
		//Move Towards Player Character
		float speedModifier = 1.0F; //syncs movement to animation
		float moveStep = speed * Time.deltaTime * speedModifier;
		transform.position = Vector3.MoveTowards (transform.position, playerCharacter.position, moveStep);


		//Rotate Towards Player Character
		float rotateModifier = 1.0F;
		float rotateStep = speed * Time.deltaTime * rotateModifier;
		Vector3 targetDirection = playerCharacter.position - transform.position;
		Vector3 newDirection = Vector3.RotateTowards (transform.forward, targetDirection, rotateStep, 0.0F);
		Debug.DrawRay (transform.position, newDirection, Color.red);
		transform.rotation = Quaternion.LookRotation (newDirection);
	}

	public void Die () {
		if(!isDead) {
		Debug.Log ("dying...");
			CapsuleCollider collider = GetComponent<CapsuleCollider> ();
			collider.enabled = false;
		anim.SetTrigger (dieTrigger);
		//yield return new WaitForSeconds (1);
		SetCanMove (false);
		isDead = true;
		//anim.StopPlayback ();
		}
	}

	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
		anim.speed = speed;
	}

	public void SetCanMove(bool toSet) {
		canMove = toSet;
		if (!canMove) {
			//anim.StopPlayback ();
		}
	}

	public void ResetPosition() {
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		anim.SetTrigger (idleTrigger);
	}

	public void StartAtPosition(Vector3 pos) {
		transform.position = pos;
		transform.rotation = initialRotation;
		StartMoving ();
	}

	public void StartMoving() {
		SetCanMove (true);
		Debug.Log ("moving zombie, triggering anim");
		CapsuleCollider collider = GetComponent<CapsuleCollider> ();
		collider.enabled = true;
		anim.StopPlayback ();
		anim.SetTrigger (walkTrigger);
		isDead = false;
	}
}
