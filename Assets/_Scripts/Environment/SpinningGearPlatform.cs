using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningGearPlatform: MonoBehaviour, IContact<Player> {

	public bool clockwise;
	public float speed;

	private void Update() {
		if (clockwise) {
			transform.Rotate(Vector3.forward * Time.deltaTime * speed);
		}
		else{
			transform.Rotate(Vector3.back * Time.deltaTime * speed);
		}
	}

	public void OnCollision(Player p){
		p.movement.jumps=2;
	}

	public void OnTrigger(Player p){
		p.movement.jumps=2;
	}

}