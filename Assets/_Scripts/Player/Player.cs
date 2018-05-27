using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour {

	public Gear gear;
	public Collider2D col;
	public Vector2 deathPos;

	public static Player player;

	public PlayerMovement movement;
	public ThrowGear throwGear;

	void Start() {
		player = this;
		movement = GetComponent<PlayerMovement>();
		throwGear = GetComponent<ThrowGear>();
	}

	void Update() {

		if (transform.position.y < -10) {
			Die();
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}

		if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		other.gameObject.GetComponent<IContact<Player>>()?.OnCollision(this);
	}

	void OnTriggerEnter2D(Collider2D other) {
		other.GetComponent<IContact<Player>>()?.OnTrigger(this);
	}

	public void Die() {
		StopAllCoroutines();
		throwGear.StopAllCoroutines();
		throwGear.SetLine(Vector2.zero, Vector2.zero);
		movement.rb2d.velocity = Vector2.zero;
		transform.position = deathPos;
		movement.grounded = false;
		throwGear.PickUpGear();
	}

}

public interface IContact<T>{
	void OnCollision(T t);
	void OnTrigger(T t);
}