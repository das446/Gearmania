using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float speed;
	public Rigidbody2D rb2d;
	private bool grounded;
	[SerializeField] float jumpForce;
	[SerializeField] public Player player;
	public float distToGround;
	BoxCollider2D collider;

	public bool Grounded {
		get {
			int x = ~(1 << 8);
			Vector3 t = transform.position + Vector3.down * distToGround * 0.9f;
			return Physics2D.Raycast(t, Vector3.down, 0.2f, x) ||
				Physics2D.Raycast(t + Vector3.right * 0.5f, Vector3.down, 0.2f, x) ||
				Physics2D.Raycast(t = Vector3.left * 0.5f, Vector3.down, 0.2f, x);
		}

	}

	void Start() {
		collider = GetComponent<BoxCollider2D>();
		distToGround = collider.bounds.extents.y;
	}

	void MoveH() {
		float x = Input.GetAxis("Horizontal");
		if (x == 0) {
			x = Input.GetAxis("Horizontal2");
		}
		rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
		if (x < 0) {
			transform.eulerAngles = new Vector3(0, 180, 0);
		} else if (x > 0) {
			transform.eulerAngles = Vector3.zero;
		}
	}

	void Jump() {
		rb2d.AddForce(Vector3.up * jumpForce);
		//Grounded = false;
		this.PlaySound("Jump", randomPitch : true);
	}

	void Update() {
		MoveH();
		if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Jump2")) && Grounded) {
			//Debug.Log("Jump");
			Jump();
		} else if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Jump2"))) {
			Debug.Log("Not Grounded");
		}
	}
}