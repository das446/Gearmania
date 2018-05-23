using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : MonoBehaviour {

	public Rigidbody2D rb2d;
	public Collider2D col;
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
			if(other.gameObject.tag == "Enemy"){this.PlaySound("Robot_Die");}
			else{this.PlaySound("Shield");}
			Destroy(other.gameObject);
			if(Player.player.throwGear.holdingGear){
				TakeDamage(10);
			}
			else{
				TakeDamage(2);
			}
		}

	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag!="Player" && other.gameObject.tag!="Projectile" && other.gameObject.tag!="Enemy"){
			effector.enabled = true;
			gameObject.layer = 0;
			this.PlaySound("Drop");
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

	public void Heal(int amnt) {
		Debug.Log("Heal");
		hp += amnt;
		if (hp > hpMax) {
			hp = hpMax;
		}
		UpdateHealthBar();

	}

	public IEnumerator RepeatedHeal() {
		while (true) {
			yield return new WaitForSeconds(1);
			if(!broken){
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

}