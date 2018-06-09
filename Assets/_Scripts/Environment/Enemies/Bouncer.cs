using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : EnemyRobot {

	Rigidbody2D rb2d;
	public float jumpHeight;

	public override void Start()
	{
		base.Start();
		rb2d=GetComponent<Rigidbody2D>();
	}
	public override void Update(){
		base.Update();
	}

}
