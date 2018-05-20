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

	// Use this for initialization
	void Start() {
		gear = GetComponent<Player>().gear;
	}

	// Update is called once per frame
	void Update() {

		if (gear.broken) { return; }

		if (Input.GetButtonDown("Fire1") && holdingGear) {
			DropGear();
		} else if (Input.GetButtonDown("Fire1") && Vector2.Distance(transform.position, gear.transform.position) < 1.5f) {
			PickUpGear();
		} else if (Input.GetButtonDown("Fire1") && Vector2.Distance(transform.position, gear.transform.position) >= 1.5f) {
			StartCoroutine(RetrieveGear());
		}

		if (Input.GetButtonDown("Fire2") && (holdingGear || !gear.gameObject.activeInHierarchy)) {
			StartCoroutine(throwGear());
		}

		if (Input.GetButton("TriggerR") && !holdingGear && !gear.gameObject.activeInHierarchy) {
			TakeOutGear();

		} else if (!Input.GetButton("TriggerR") && holdingGear) {
			PutAwayGear();
		}

	}

	void DropGear(bool effector = true) {
		gear.transform.parent = null;
		gear.col.isTrigger = false;
		gear.effector.enabled = true;
		holdingGear = false;
	}

	public IEnumerator throwGear() {
		holdingGear = false;
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

	public void PickUpGear() {
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

	public void PutAwayGear() {
		holdingGear = false;
		gear.gameObject.SetActive(false);
	}

	public void TakeOutGear() {
		gear.gameObject.SetActive(true);
		PickUpGear();
	}

}