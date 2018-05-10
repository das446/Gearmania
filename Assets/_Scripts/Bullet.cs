using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] float speed;

	// Use this for initialization
	void Start() {
		LookAtPlayer();
		Destroy(gameObject,5);
	}

	// Update is called once per frame
	void Update() {
		transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
	}

	void LookAtPlayer(){
		transform.right = Player.player.transform.position - transform.position;
	}

	void LookAtPlayer2(){
		Vector3 diff = Player.player.transform.position - transform.position;
		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 180f, rot_z - 90);
	}
}