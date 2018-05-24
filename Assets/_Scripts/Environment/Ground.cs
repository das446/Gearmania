using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, IGearContact, IPlayerContact {
	public void OnGearCollision(Gear gear) {
		gear.effector.enabled = true;
		gear.gameObject.layer = 0;
		this.PlaySound("Drop");
	}

	public void OnGearTrigger(Gear gear) {
		gear.effector.enabled = true;
		gear.gameObject.layer = 0;
		this.PlaySound("Drop");
	}

    public virtual void OnPlayerCollision(Player p)
    {
        p.movement.grounded = true;
    }

    public void OnPlayerTrigger(Player p)
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