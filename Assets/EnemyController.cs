using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float speed = 5.0f;
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

			Debug.Log ("got ball");
			Debug.Log (this.ballLastXPos);
			Debug.Log (this.ballLastYPos);

		} else {
			Debug.Log ("no ball :/");

		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject ball = GameObject.Find("Ball");

		if (ball) {
			this.ballCurrentXPos = ball.transform.position.x;
			this.ballCurrentYPos = ball.transform.position.y;

			// calculate our predicted intersection spot and move towards it
			// we know two points of line that make up the ball's current
			// path.
			//
			// we know the x position that we want the ball to intersect with
			// 	(the left side of this objects rigidbody2d)
			//
			// so we need to find the corresponding y position and begin moving towards it
			//
			// there may be ways to optimize this somehow
			//
			// when we try to make the AI stronger, we can adjust the paddle's y position
			// to strike in different ways
			// 
			// we can also just increase the speed at which the enemy paddle moves


			// get the slope
			float m = (this.ballCurrentYPos - this.ballLastYPos) / (this.ballCurrentXPos - this.ballLastXPos);

			// we can use point-slope form
			// y1 - y2 = m(x1 - x2)
			// y1 = m(x1 - x2) + y2 
			// y1 - m(x1 - x2) = y2
			// y2 = y1 - m(x1 - x2)
			// where 
			// should give us our desired y position.
			// if that y position is beyond the range of motion of enemy's paddle,
			// I think that the ball will hit a wall first.
			// if that's true, then if m is positive, it will be hitting the top first
			// and if m is negative, it will be hitting the bottom first

			float desiredYPos = this.ballCurrentYPos - (m * (this.ballCurrentXPos - this.permenantXPos));
			float currentYPos = transform.position.y;

			if (currentYPos != desiredYPos) {
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
