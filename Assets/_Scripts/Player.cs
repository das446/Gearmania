using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour {

	public Gear gear;
	[SerializeField] Collider2D col;
	public Vector2 deathPos;

	public static Player player;

	public PlayerMovement movement;
	public ThrowGear throwGear;

	void Start() {
		player = this;
		movement = GetComponent<PlayerMovement>();
		throwGear = GetComponent<ThrowGear>();
		StartCoroutine(RepeatedHeal());
	}

	void Update() {

		if (transform.position.y < -10) {
			Die();
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag!="Gear"){
			movement.grounded = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Projectile" || other.gameObject.tag == "Enemy") {
			Die();
		}
	}

	public void Die() {
		StopAllCoroutines();
		throwGear.lr.SetPosition(0, Vector2.zero);
		throwGear.lr.SetPosition(1, Vector2.zero);
		throwGear.PickUpGear();
		movement.rb2d.velocity = Vector2.zero;
		transform.position = deathPos;
		movement.grounded = false;
	}

	public IEnumerator RepeatedHeal() {
		while (true) {
			yield return new WaitForSeconds(1);
			if(throwGear.holdingGear){
				gear.Heal(1);
			}

		}
	}

}

public interface IPlayerContact {
	void OnCollision(Player p);
	void OnTrigger(Player p);
}