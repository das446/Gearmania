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
        p.movement.grounded = true;
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

	}

	// Update is called once per frame
	void Update() {

	}
}