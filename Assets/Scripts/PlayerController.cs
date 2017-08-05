using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D body;
    public float speed;
	private float moveVertical = 0.0f;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D> ();
    }

	void Update () {
        moveVertical = Input.GetAxis ("Vertical");
	}

    // Update is called once per frame
    // but FixedUpdate is called immediately before each physics update
    // so we generally want to perform physics-related tasks here
    void FixedUpdate () {
        Vector2 move = new Vector2 (0, moveVertical);
        body.AddForce (move * speed);

    }
}
