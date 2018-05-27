using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGround : Ground,IPuzzleObject {

	bool deadly=true;

    public void TurnOff()
    {
        deadly = true;
		GetComponent<SpriteRenderer>().color = Color.red;
		if(GetComponent<Collider2D>().IsTouching(Player.player.col)){
			Player.player.Die();
		}
    }

    public void TurnOn()
    {
        deadly = false;
		GetComponent<SpriteRenderer>().color = Color.white;

    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnCollision(Player p){
		if (deadly){
			p.Die();
		}
		else{
			base.OnCollision(p);
		}
	}

}
