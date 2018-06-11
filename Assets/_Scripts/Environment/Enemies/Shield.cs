using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour,IContact<Gear> {

	 public void OnCollision(Gear g)
    {
        if(Player.player.throwGear.holdingGear){return;}
		Debug.Log("Shield");
        g.StopAllCoroutines();
		Player.player.throwGear.StopAllCoroutines();
		g.rb2d.velocity=Vector2.zero;
		Player.player.throwGear.DropGear();
    }

    public void OnTrigger(Gear g)
    {
        OnCollision(g);
    }
}
