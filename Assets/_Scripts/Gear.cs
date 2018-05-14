using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] Rigidbody2D rb2d;
	public Collider2D col;
	[SerializeField] bool grounded;
	[SerializeField] float jumpForce;
	public PlatformEffector2D effector;
	public int hp, hpMax;
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
		if (other.gameObject.tag == "Projectile" || other.gameObject.tag == "Enemy") {
			Destroy(other.gameObject);
			if(Player.player.throwGear.holdingGear){
				TakeDamage(10);
			}
			else{
				TakeDamage(2);
			}
		}

	}

	void TakeDamage(int amnt) {
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

	void Heal(int amnt) {
		hp += amnt;
		if (hp > hpMax) {
			hp = hpMax;
		}
		UpdateHealthBar();

	}

	IEnumerator RepeatedHeal() {
		while (true) {
			yield return new WaitForSeconds(1);
			Heal(1);

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

}