using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer : EnemyRobot {

	public bool hiding;
	// Use this for initialization
	public override void OnTrigger(Gear gear){
		if(hiding){return;}
		base.OnTrigger(gear);
	}

	public void Hide(){
		hiding=true;
	}

	public void UnHide(){
		hiding=false;
	}
}
