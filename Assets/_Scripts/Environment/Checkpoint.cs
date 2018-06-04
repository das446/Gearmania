using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IContact<Player> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTrigger(Player p){
		p.deathPos=transform.position;
	}

	public void OnCollision(Player p){
		OnTrigger(p);
	}
}
