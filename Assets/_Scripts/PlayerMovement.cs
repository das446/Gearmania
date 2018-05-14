using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float speed;
	public Rigidbody2D rb2d;
	public bool grounded;
	[SerializeField] float jumpForce;
	[SerializeField] public Player player;

	void MoveH() {
		float x = Input.GetAxis("Horizontal");
		rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
		if (x < 0) {
			transform.eulerAngles = new Vector3(0, 180, 0);
		} else if (x > 0) {
			transform.eulerAngles = Vector3.zero;
		}
	}

	void Jump() {
		rb2d.AddForce(Vector3.up * jumpForce);
		grounded = false;
	}

	void Update() {
		MoveH();

		if (Input.GetButtonDown("Jump") && grounded) {
			//Debug.Log("Jump");
			Jump();
		}
	}
}