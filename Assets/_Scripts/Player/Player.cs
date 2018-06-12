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

	public Animator animator;

	void Start() {
		player = this;
		movement = GetComponent<PlayerMovement>();
		throwGear = GetComponent<ThrowGear>();
		animator = GetComponent<Animator>();
	}

	void Update() {

		if (transform.position.y < -10) {
			Die(true);
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if(Input.GetKeyDown(KeyCode.N)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		other.gameObject.GetComponent<IContact<Player>>()?.OnCollision(this);
	}
	void OnCollisionExit2D(Collision2D other) {
		other.gameObject.GetComponent<IExitContact<Player>>()?.OnExitCollision(this);
	}

	void OnTriggerEnter2D(Collider2D other) {
		other.GetComponent<IContact<Player>>()?.OnTrigger(this);
	}

	void OnTriggerExit2D(Collider2D other) {
		other.GetComponent<IExitContact<Player>>()?.OnExitTrigger(this);
	}

	IEnumerator DieHelper() {

		yield return new WaitForSeconds(1);
		transform.position = deathPos;
		//movement.Grounded = false;
		throwGear.DropGear();
		throwGear.PickUpGear();
		movement.enabled = true;
		throwGear.enabled = true;
	}

	public void Die(bool fall = false) {
		Debug.Log("Die");
		StopAllCoroutines();
		throwGear.StopAllCoroutines();
		throwGear.SetLine(Vector2.zero, Vector2.zero);
		movement.rb2d.velocity = Vector2.zero;
		movement.enabled = false;
		throwGear.enabled = false;
		this.PlaySound("OnHit");
		if (fall) {
			transform.position = deathPos;
			throwGear.DropGear();
			throwGear.PickUpGear();
			movement.enabled = true;
			throwGear.enabled = true;
		} else {
			animator.SetTrigger("Die");
			StartCoroutine(DieHelper());
		}
	}

}

public interface IContact<T> {
	void OnCollision(T t);
	void OnTrigger(T t);
}

public interface IExitContact<T> {
	void OnExitCollision(T t);
	void OnExitTrigger(T t);
}