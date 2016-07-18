using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D body;
	public float speed;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	// but FixedUpdate is called immediately before each physics update
	// so we generally want to perform physics-related tasks here
	void FixedUpdate () {
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 move = new Vector2 (0, moveVertical);
		body.AddForce (move * speed);
	
	}
}
