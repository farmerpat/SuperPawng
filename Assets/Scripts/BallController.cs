﻿using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	private Rigidbody2D body;
	private Vector2 movementForce;
	private AudioSource[] sounds;
	private AudioSource ballBeepOne;
	private AudioSource ballBeepTwo;
	private float collisionTolerance = 0.05f;
	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;

	public float speed;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
		movementForce = new Vector2 (-0.5f, -0.25f);
		sounds = GetComponents<AudioSource> ();
		ballBeepOne = sounds [0];
		ballBeepTwo = sounds [1];

	}
	
	// when dealing with a RigidBody, FixedUpdate should
	// be used instead of Update
	void FixedUpdate () {
		body.velocity = movementForce * speed;

	}

	void OnCollisionEnter2D (Collision2D collision) {
		float yComponent = movementForce.y;
		float xComponent = movementForce.x;

		if (collision.gameObject.CompareTag ("BackgroundLeft")) {
			// play fail sound
			Debug.Log ("CPU score");

			movementForce.x = 0.0f;
			movementForce.y = 0.0f;

		} else if (collision.gameObject.CompareTag("BackgroundRight")) {
			// play goal sound instead
			this.playBallBeepTwo ();
			Debug.Log ("Player score");

			xComponent *= -1.0f;
			movementForce.x = xComponent;

		} else if (collision.gameObject.CompareTag("BackgroundTop")) {
			this.playBallBeepTwo ();
			yComponent *= -1.0f;
			movementForce.y = yComponent;

		} else if (collision.gameObject.CompareTag("BackgroundBottom")) {
			this.playBallBeepTwo ();
			yComponent *= -1.0f;
			movementForce.y = yComponent;

		} else if (collision.gameObject.CompareTag ("Player")) {
			// randomize the second param so it doesnt sound so repetetive
			this.playBallBeepOne ();

			//ContactPoint2D cp = collision.contacts[0];
			Collider2D collider = collision.collider;
			Vector2 cpp = collision.contacts[0].point;
			Vector2 hitBoxCenter = collider.bounds.center;

			float hitBoxTop = collider.bounds.max.y;
			float hitBoxBottom = collider.bounds.min.y;
			float hitBoxLeft = collider.bounds.min.x;
			float hitBoxRight = collider.bounds.max.x;

			// we should make this abs and comp a fn to clean it up
			if (Mathf.Abs(cpp.y - hitBoxTop) < collisionTolerance) {
				Debug.Log ("i r hit top");
			}

			if (cpp.y == hitBoxBottom) {
				Debug.Log ("i r hit bottom");
			}

			if (cpp.x == hitBoxLeft) {
				Debug.Log ("i r hit back");
			}

			if (Mathf.Abs(cpp.x - hitBoxRight) < collisionTolerance) {
				Debug.Log ("i r hit front");
			}

			// want the result:
			// 	to have a magnitude that is affected by the
			// velocity of the paddle
			//
			// reverse the directions
			xComponent *= -1.0f;
			//yComponent *= -1.0f;

			movementForce.x = xComponent;
			//movementForce.y = yComponent;

		}
	}

	private void playBallBeepOne () {
		float vol = Random.Range(volLowRange, volHighRange);
		ballBeepOne.PlayOneShot (ballBeepOne.clip, vol);

	}

	private void playBallBeepTwo () {
		float vol = Random.Range(volLowRange, volHighRange);
		ballBeepTwo.PlayOneShot (ballBeepTwo.clip, vol);

	}
}
