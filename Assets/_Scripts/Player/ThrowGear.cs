using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGear : MonoBehaviour {

	public float throwDist;
	public float gearThrowSpeed;

	public bool holdingGear = true;
	public LineRenderer lr;

	public GameObject hand;

	private Gear gear;

	public GameObject targetIcon;

	// Use this for initialization
	void Start() {
		gear = GetComponent<Player>().gear;
	}

	// Update is called once per frame
	void Update() {

		if (gear.broken) { return; }

		updateTargetIcon();

		

		bool i = Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(1) || (Input.GetButtonDown("Fire2") && !holdingGear);

		if (i && holdingGear) {
			DropGear();
		} else if (i && Vector2.Distance(transform.position, gear.transform.position) < 1.5f) {
			PickUpGear();
		} else if (i && Vector2.Distance(transform.position, gear.transform.position) >= 1.5f) {
			StartCoroutine(RetrieveGear());
		}

		else if ((Input.GetButtonDown("Fire2") || Input.GetMouseButtonDown(0)) && (holdingGear || !gear.gameObject.activeInHierarchy)) {
			Vector2 target = getTarget();
			StartCoroutine(throwGear(target));
			//ThrowGearPhysics(target);
		}

		

	}

	private void updateTargetIcon() {
		float x = Input.GetAxis("aimH");
		float y = Input.GetAxis("aimV");
		targetIcon.transform.position = (Vector2)gear.transform.position + new Vector2(x, y) * 3;
	}

	public void DropGear(bool effector = true) {
		gear.transform.parent = null;
		gear.col.isTrigger = false;
		gear.effector.enabled = false;
		holdingGear = false;
		gear.rb2d.bodyType = RigidbodyType2D.Dynamic;
		gear.rb2d.gravityScale = 1;
		gear.rb2d.velocity = Vector2.zero;
		gear.gameObject.layer = 9;
		targetIcon.gameObject.SetActive(false);
	}

	public IEnumerator throwGear(Vector2 target) {
		Vector2 dir = target - (Vector2)gear.transform.position;

		holdingGear = false;
		DropGear(effector: false);
		bool right = transform.eulerAngles.y == 0;
		//Vector2 target = right ? transform.position + Vector3.right * throwDist : transform.position - Vector3.right * throwDist;
		Vector2 cur = gear.transform.position;

		while (Vector2.Distance(cur, target) > 0.25f) {
			cur = Vector3.MoveTowards(cur, target, gearThrowSpeed * Time.deltaTime);
			gear.transform.position = cur;
			yield return null;
		}

		DropGear();
		gear.rb2d.AddForce(400*dir);

		//yield return StartCoroutine(RetrieveGear(drawGrapple: false));

	}

	public void ThrowGearPhysics(Vector2 dir){
		DropGear();
		gear.rb2d.AddForce(dir*500);
	}

	public void PickUpGear() {
		gear.transform.parent = hand.transform;
		//gear.col.isTrigger = true;
		gear.transform.localPosition = Vector3.zero;
		gear.effector.enabled = false;
		holdingGear = true;
		gear.rb2d.bodyType = RigidbodyType2D.Kinematic;
		gear.rb2d.gravityScale = 0;
		gear.rb2d.velocity = Vector2.zero;
		targetIcon.gameObject.SetActive(true);

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

	public void PutAwayGear() {
		holdingGear = false;
		gear.gameObject.SetActive(false);
	}

	public void TakeOutGear() {
		gear.gameObject.SetActive(true);
		PickUpGear();
	}

	Vector2 getTarget() {

		bool mouse = Input.GetMouseButtonDown(0);
		Vector2 gearPos = gear.transform.position;
		Vector2 target = Vector2.zero;

		if (mouse) {
			target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
		} else {

			target = targetIcon.transform.position;
			if (Input.GetAxis("aimH")==0 && Input.GetAxis("aimV")==0) {
				target = transform.position + transform.right*3;
			}
		}

		float distance = Vector2.Distance(target, gearPos);
		Vector2 tempTarget = target - gearPos;
		tempTarget = tempTarget * throwDist / distance;
		target = tempTarget + gearPos;
		return target;
	}

}