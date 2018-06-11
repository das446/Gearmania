using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

	public Vector2 pos1, pos2, target;
	public float speed = 1;

	public bool flip;

	public SpriteRenderer sr;

	void Start() {
		pos1 = transform.position;
		target = pos2;
		sr = GetComponent<SpriteRenderer>();
	}

	void Update() {
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
		if (transform.position == (Vector3) target) {
			if (target == pos1) {
				target = pos2;
				if (flip) {

				}
			} else {
				target = pos1;
			}
			sr.flipX = !sr.flipX;
		}
	}

	void OnDrawGizmosSelected() {
		if (target != null) {
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, pos2);
		}
	}

	// Update is called once per frame

}