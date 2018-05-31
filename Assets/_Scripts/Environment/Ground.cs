using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, IContact<Gear>, IContact<Player> {
    public void OnCollision(Gear gear)
    {
        gear.effector.enabled = true;
		gear.gameObject.layer = 0;
		this.PlaySound("Drop");
    }

    public virtual void OnCollision(Player p)
    {
        //p.movement.Grounded = true;
    }


    public void OnTrigger(Gear gear)
    {
        gear.effector.enabled = true;
		gear.gameObject.layer = 0;
		this.PlaySound("Drop");
    }

    public void OnTrigger(Player t)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start() {

        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
        if(s.drawMode == SpriteDrawMode.Tiled){
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            col.size = new Vector2(s.size.x,0.25f);
            col.offset = new Vector2(0,0.375f);

        }

	}

	// Update is called once per frame
	void Update() {

	}
}