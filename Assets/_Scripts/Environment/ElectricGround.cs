using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGround : Ground, IPuzzleObject {

	bool deadly = true;

	public void TurnOff() {
		deadly = true;
		GetComponent<SpriteRenderer>().color = Color.red;
		if (GetComponent<Collider2D>().IsTouching(Player.player.col)) {
			ElectrocutePlayer(Player.player);
		}
	}

	public void TurnOn() {
		deadly = false;
		GetComponent<SpriteRenderer>().color = Color.white;

	}

	public void ElectrocutePlayer(Player p) {
		p.Die();
		this.PlaySound("Zaped");
	}

	public override void OnCollision(Player p) {
		if (deadly) {
			ElectrocutePlayer(p);
		} else {
			base.OnCollision(p);
		}
	}

}