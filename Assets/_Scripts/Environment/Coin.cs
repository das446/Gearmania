using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IContact<Player> {

	// Use this for initialization
	public void OnCollision (Player p) {
		Destroy(gameObject);
	}

	public void OnTrigger (Player p) {
		Destroy(gameObject);
	}
	
	
}
