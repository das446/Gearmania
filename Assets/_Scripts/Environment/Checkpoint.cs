using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IContact<Player>, IContact<Gear> {

	public Sprite activeSprite;

	bool active = false;

	public void OnTrigger(Player p) {
		if (active) { return; }
		active = true;
		this.PlaySound("Checkpoint");
		p.deathPos = transform.position;
		GetComponent<SpriteRenderer>().sprite = activeSprite;
	}

	public void OnCollision(Player p) {

		OnTrigger(p);

	}

	public void OnCollision(Gear g) {
		if (active) { return; }
		active = true;
		this.PlaySound("Checkpoint");
		Player.player.deathPos = transform.position;
		GetComponent<SpriteRenderer>().sprite = activeSprite;
	}

	public void OnTrigger(Gear g) {
		if (active) { return; }
		active = true;
		this.PlaySound("Checkpoint");
		Player.player.deathPos = transform.position;
		GetComponent<SpriteRenderer>().sprite = activeSprite;
	}
}