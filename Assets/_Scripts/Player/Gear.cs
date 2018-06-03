using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : MonoBehaviour, IContact<Player> {

	public Rigidbody2D rb2d;
	public Collider2D col;
	public PlatformEffector2D effector;
	public int hp,
	hpMax;
	public float respawnTime;
	public bool broken = false;
	public Image HealthBar;

	// Use this for initialization
	void Start() {
		hp = hpMax;
		StartCoroutine(RepeatedHeal());

	}

	// Update is called once per frame
	void Update() {

	}

	void OnTriggerEnter2D(Collider2D other) {

		other.GetComponent<IContact<Gear>>()?.OnTrigger(this);

	}

	void OnTriggerExit2D(Collider2D other) {

		other.GetComponent<IExitContact<Gear>>()?.OnExitTrigger(this);

	}

	private void OnCollisionEnter2D(Collision2D other) {

		other.gameObject.GetComponent<IContact<Gear>>()?.OnCollision(this);
	}

	private void OnCollisionExit2D(Collision2D other) {

		other.gameObject.GetComponent<IExitContact<Gear>>()?.OnExitCollision(this);
	}

	public void TakeDamage(int amnt) {
		hp -= amnt;
		if (hp <= 0) {
			hp = 0;
			Break();
		}
		UpdateHealthBar();
	}

	void Break() {
		broken = true;
		Invoke("Respawn", respawnTime);
		gameObject.SetActive(false);
	}

	public void Heal(int amnt) {
		hp += amnt;
		if (hp > hpMax) {
			hp = hpMax;
		}
		UpdateHealthBar();

	}

	public IEnumerator RepeatedHeal() {
		while (true) {
			yield return new WaitForSeconds(1);
			if (!broken) {
				Heal(2);
			}

		}
	}

	void Respawn() {
		broken = false;
		gameObject.SetActive(true);
		hp = hpMax;
		UpdateHealthBar();
		StartCoroutine(RepeatedHeal());
	}

	void UpdateHealthBar() {
		HealthBar.fillAmount = (float) ((float) hp / (float) hpMax);
	}

	public void OnCollision(Player p) {
		if (!p.throwGear.holdingGear) {
			//p.movement.Grounded = true;
		}
	}

	public void OnTrigger(Player p) { }
}