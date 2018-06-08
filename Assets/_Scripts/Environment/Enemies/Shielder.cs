using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : EnemyRobot {
	public Shield shield;
	Vector3 offset;

	public override void Start(){
		base.Start();
		offset=shield.transform.position-transform.position;
	}

	public override void Update(){
		base.Update();
		shield.transform.position=transform.position+offset;
	}

	public override void OnCollision(Gear gear){
		Destroy(shield.gameObject);
		base.OnCollision(gear);
		
	}

	public override void OnTrigger(Gear gear){
		Destroy(shield.gameObject);
		base.OnTrigger(gear);
		
	}


}