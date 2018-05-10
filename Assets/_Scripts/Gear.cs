using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] Rigidbody2D rb2d;
	public Collider2D col;
	[SerializeField] bool grounded;
	[SerializeField] float jumpForce;
	public PlatformEffector2D effector;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Projectile" || other.gameObject.tag == "Enemy") {
			Destroy(other.gameObject);
		}

	}
}