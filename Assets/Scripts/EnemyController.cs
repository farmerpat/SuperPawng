using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float speed = 5.0f;
	public float goalPositionTolerance = 0.3f;

	private float ballLastXPos;
	private float ballLastYPos;
	private float ballCurrentXPos;
	private float ballCurrentYPos;
	private float desiredYPos = 0.0f;
	private float permenantXPos = 0.0f;
	private Rigidbody2D body;
	private Vector2 movementForce;

	// Use this for initialization
	void Start () {
		this.body = GetComponent<Rigidbody2D> ();
		this.movementForce = new Vector2 (0.0f, 0.0f);
		this.permenantXPos = transform.position.x;

		// get the ball pos and init ballLastXPos and ballLastYPos
		GameObject ball = GameObject.Find("Ball");

		if (ball) {
			this.ballLastXPos = ball.transform.position.x;
			this.ballLastYPos = ball.transform.position.y;

		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject ball = GameObject.Find("Ball");

		if (ball) {
			this.ballCurrentXPos = ball.transform.position.x;
			this.ballCurrentYPos = ball.transform.position.y;

			// there may be ways to optimize this somehow
			//
			// when we try to make the AI stronger, we can adjust the paddle's y position
			// to strike in different ways
			// we could do this by randomly selecting which postion the paddle is to try
			// and hit the ball with (top middle bottom).
			//
			// we can also just increase the speed at which the enemy paddle moves


			// get the slope
			float m = (this.ballCurrentYPos - this.ballLastYPos) / (this.ballCurrentXPos - this.ballLastXPos);
			float desiredYPos = this.ballCurrentYPos - (m * (this.ballCurrentXPos - this.permenantXPos));
			float currentYPos = transform.position.y;

			// a better way to slow this down might be do decrease the magnitude of the value
			// we set movementForce.y to here after it gets pretty close to its destination
			if (Mathf.Abs(currentYPos - desiredYPos)  > goalPositionTolerance) {
				// move it closer by changing movementForce.y
				if (currentYPos < desiredYPos) {
					movementForce.y = 1.0f;
				} else {
					movementForce.y = -1.0f;

				}
			} else {
				// stop moving
				movementForce.y = 0.0f;

			}

			this.ballLastXPos = this.ballCurrentXPos;
			this.ballLastYPos = this.ballCurrentYPos;

		}

		body.velocity = movementForce * speed;
	}
}
