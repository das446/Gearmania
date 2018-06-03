using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, IContact<Gear>, IContact<Player>,IExitContact<Player>,IExitContact<Gear> {
    public void OnCollision(Gear gear)
    {
        gear.effector.enabled = true;
		gear.gameObject.layer = 0;
		this.PlaySound("Drop");
        //gear.transform.parent=transform;
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
    protected void Start() {

        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
        if(s.drawMode == SpriteDrawMode.Tiled){
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            col.size = new Vector2(s.size.x,0.25f);
            col.offset = new Vector2(0,0.375f);

        }

	}
}