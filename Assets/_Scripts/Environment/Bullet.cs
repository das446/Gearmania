using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IContact<Gear>, IContact<Player> {

	[SerializeField] float speed;

	// Use this for initialization
	void Start() {
		LookAtPlayer();
		Destroy(gameObject, 5);
	}

	// Update is called once per frame
	void Update() {
		transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
	}

	void LookAtPlayer() {
		transform.right = Player.player.transform.position - transform.position;
	}

	void LookAtPlayer2() {
		Vector3 diff = Player.player.transform.position - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 180f, rot_z - 90);
	}

	public void OnTrigger(Gear gear) {
		this.PlaySound("Shield");
		if (Player.player.throwGear.holdingGear) {
			gear.TakeDamage(10);
		} else {
			gear.TakeDamage(2);
		}
		Destroy(gameObject);
	}

	public void OnCollision(Gear gear) {
		this.PlaySound("Shield");
		if (Player.player.throwGear.holdingGear) {
			gear.TakeDamage(10);
		} else {
			gear.TakeDamage(2);
		}
		Destroy(gameObject);
	}

    public void OnCollision(Player p)
    {
        throw new System.NotImplementedException();
    }

    public void OnTrigger(Player p)
    {
        p.Die();
    }
}