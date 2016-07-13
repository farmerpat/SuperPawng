using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	private Rigidbody2D body;
	private Vector2 movementForce;
	private AudioSource audioSource;
	private float collisionTolerance = 0.05f;

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
			// randomize the second param so it doesnt sound so repetetive
			audioSource.PlayOneShot (audioSource.clip, 1f);

			//Collider2D collider = collision.collider;
			ContactPoint2D cp = collision.contacts[0];
			//Debug.Log ("collision contacts[0]:");
			//Debug.Log (cp);

			Collider2D collider = collision.collider;

			Vector2 cpp = collision.contacts[0].point;
			//Debug.Log ("contacts[0].point");
			//Debug.Log (cpp);

			Vector2 hitBoxCenter = collider.bounds.center;
			//Debug.Log ("hitBoxCenter:");
			//Debug.Log (hitBoxCenter);

			float hitBoxTop = collider.bounds.max.y;
			float hitBoxBottom = collider.bounds.min.y;
			float hitBoxLeft = collider.bounds.min.x;
			float hitBoxRight = collider.bounds.max.x;

			// if cpp.y == hitBoxTop, it hit the top
			// if cpp.y == hitBoxBottom, it hit the bottom
			// if cpp.x == hitBoxLeft, it hit the back
			// if cpp.x == hitBoxRight, it hit the front

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


			// see if it hit the top or bottom or front or back

			//body = GetComponent<Rigidbody2D> ();

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
