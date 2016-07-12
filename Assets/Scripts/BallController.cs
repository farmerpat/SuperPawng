using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	private Rigidbody2D body;
	private Vector2 movementForce;
	private AudioSource audioSource;

	public float speed;
	//private AudioClip ballBeepOne;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		movementForce = new Vector2 (-1.0f, -0.5f);
		audioSource = GetComponent<AudioSource> ();
		//ballBeepOne = audioSource.clip;

	}
	
	// when dealing with a RigidBody, FixedUpdate should
	// be used instead of Update
	void FixedUpdate () {
		// USING TRANSLATE IS PROBABLY THE WINNING MOVE FOR PONG PHYSICS
		//body.AddForce(movementForce * speed);
		transform.Translate(movementForce * speed * Time.deltaTime); 

	}

	void OnCollisionEnter2D (Collision2D collision) {
		float yComponent = movementForce.y;
		float xComponent = movementForce.x;

		if (collision.gameObject.CompareTag ("BackgroundLeft")) {
			Debug.Log ("CPU score");

			movementForce.x = 0.0f;
			movementForce.y = 0.0f;

			//xComponent *= -1.0f;
			//yComponent *= -1.0f;


			//movementForce.x = xComponent;
			//movementForce.y = yComponent;


		} else if (collision.gameObject.CompareTag("BackgroundRight")) {
			Debug.Log ("Player score");
			//movementForce.x = 0.0f;
			//movementForce.y = 0.0f;
			xComponent *= -1.0f;
			movementForce.x = xComponent;

		} else if (collision.gameObject.CompareTag("BackgroundTop")) {
			yComponent *= -1.0f;
			movementForce.y = yComponent;

		} else if (collision.gameObject.CompareTag("BackgroundBottom")) {
			yComponent *= -1.0f;
			movementForce.y = yComponent;

		} else if (collision.gameObject.CompareTag ("Player")) {
			//audioSource.PlayOneShot (ballBeepOne, 1f);
			audioSource.PlayOneShot (audioSource.clip, 1f);
			// we want to maintain y velocity and reverse the x 
			//float xComponent = movementForce.x;
			//float yComponent = movementForce.y;

			// want the result:
			// 	to have a magnitude that is affected by the
			// velocity of the paddle



			// reverse the directions
			xComponent *= -1.0f;
			//yComponent *= -1.0f;

			movementForce.x = xComponent;
			//movementForce.y = yComponent;

		}
	}
}
