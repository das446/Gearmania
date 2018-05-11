using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] Rigidbody2D rb2d;
	[SerializeField] bool grounded;
	[SerializeField] float jumpForce;
	[SerializeField] GameObject hand;
	[SerializeField] Gear gear;
	[SerializeField] Collider2D col;
	public Vector2 deathPos;
	public bool holdingGear = true;
	public LineRenderer lr;
	public static Player player;
	public float throwDist;
	public float gearThrowSpeed;

	void Start() {
		player = this;
	}

	void Update() {

		if(transform.position.y < -10){
			Die();
		}

		MoveH();

		if (Input.GetButtonDown("Jump") && grounded) {
			//Debug.Log("Jump");
			Jump();
		}

		if(gear.broken){return;}

		if (Input.GetButtonDown("Fire1") && holdingGear) {
			DropGear();
		} else if (Input.GetButtonDown("Fire1") && Vector2.Distance(transform.position, gear.transform.position) < 1.5f) {
			PickUpGear();
		} else if (Input.GetButtonDown("Fire1") && Vector2.Distance(transform.position, gear.transform.position) >= 1.5f) {
			StartCoroutine(RetrieveGear());
		}

		if (Input.GetButtonDown("Fire2") && holdingGear) {
			StartCoroutine(ThrowGear());
		}

	}

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

	void OnCollisionEnter2D(Collision2D other) {
		grounded = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Projectile" || other.gameObject.tag == "Enemy") {
			Die();
		}
	}

	void DropGear(bool effector = true) {
		gear.transform.parent = null;
		gear.col.isTrigger = false;
		gear.effector.enabled = true;
		holdingGear = false;
	}

	IEnumerator ThrowGear() {
		DropGear(effector: false);
		bool right = transform.eulerAngles.y == 0;
		Vector2 target = right ? transform.position + Vector3.right * throwDist : transform.position - Vector3.right * throwDist;
		Vector2 cur = gear.transform.position;

		while (Vector2.Distance(cur, target) > 0.25f) {
			cur = Vector3.MoveTowards(cur, target, gearThrowSpeed * Time.deltaTime);
			gear.transform.position = cur;
			yield return null;
		}
		yield return StartCoroutine(RetrieveGear(drawGrapple: false));

	}

	void PickUpGear() {
		gear.transform.parent = hand.transform;
		gear.col.isTrigger = true;
		gear.transform.localPosition = Vector3.zero;
		gear.effector.enabled = false;
		holdingGear = true;
	}

	IEnumerator RetrieveGear(bool drawGrapple = true) {

		Vector2 target = gear.transform.position;
		Vector2 cur = transform.position;
		float speed = 40; //maybe make it take constant time by reducing speed when it's closer

		if (drawGrapple) {
			//move line to gear
			while (Vector2.Distance(cur, target) > 0.5f) {
				cur = Vector3.MoveTowards(cur, target, speed * Time.deltaTime);
				lr.SetPosition(0, transform.position);
				lr.SetPosition(1, cur);
				yield return null;
			}
		}

		cur = target;
		lr.SetPosition(1, cur);
		target = transform.position;
		gear.col.isTrigger = true;

		//pull gear to player
		while (Vector2.Distance(cur, hand.transform.position) > 0.5f) {
			cur = Vector3.MoveTowards(cur, hand.transform.position, speed * Time.deltaTime);
			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, cur);
			gear.transform.position = cur;
			yield return null;
		}
		lr.SetPosition(0, Vector2.zero);
		lr.SetPosition(1, Vector2.zero);
		PickUpGear();

	}

	public void Die() {
		StopAllCoroutines();
		lr.SetPosition(0, Vector2.zero);
		lr.SetPosition(1, Vector2.zero);
		PickUpGear();
		rb2d.velocity = Vector2.zero;
		transform.position = deathPos;
		grounded = false;
	}

}