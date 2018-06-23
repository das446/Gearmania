using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, IContact<Gear>, IContact<Player>,IExitContact<Player>,IExitContact<Gear> {

    public bool fixCol=true;
    public void OnCollision(Gear gear)
    {
        if(Player.player.throwGear.holdingGear || gear.rb2d.bodyType!=RigidbodyType2D.Dynamic){return;}
        gear.effector.enabled = true;
		gear.gameObject.layer = 0;
		this.PlaySound("Drop");
        gear.transform.parent=transform;
    }

    public virtual void OnCollision(Player p)
    {
        p.transform.parent=transform;
        p.movement.jumps=2;
    }

    public void OnExitCollision(Player t)
    {
        t.transform.parent=null;
    }

    public void OnExitCollision(Gear t)
    {
        //t.transform.parent=null;
    }

    public void OnExitTrigger(Player t)
    {
       t.transform.parent=null;
    }

    public void OnExitTrigger(Gear t)
    {
        //t.transform.parent=null;
    }

    public void OnTrigger(Gear gear)
    {
        if(Player.player.throwGear.holdingGear){return;}
        gear.effector.enabled = true;
		gear.gameObject.layer = 0;
		this.PlaySound("Drop");
        gear.transform.parent=transform;
    }

    public void OnTrigger(Player t)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    public virtual void Start() {

        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
        if(s.drawMode == SpriteDrawMode.Tiled && fixCol){
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            col.size = new Vector2(s.size.x,1);
            col.offset = new Vector2(s.size.x/2,-s.size.y/2);

        }

	}
}