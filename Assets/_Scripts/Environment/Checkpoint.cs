using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IContact<Player> {

	public Sprite activeSprite;

	bool active=false;


	public void OnTrigger(Player p){
		this.PlaySound("Checkpoint");
		if(active){return;}
		p.deathPos=transform.position;
		GetComponent<SpriteRenderer>().sprite=activeSprite;
	}

	public void OnCollision(Player p){
		
		OnTrigger(p);
		
	}
}