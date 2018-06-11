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
		//InvokeRepeating("Jump",0,2);
	}
	public override void Update(){
		base.Update();
		
	}

	private void OnCollisionEnter2D(Collision2D other) {

		if(other.gameObject.GetComponent<Ground>()){Jump();}
	}

	void Jump(){
		Vector2 dir = getDir();
		rb2d.AddForce(dir);
	}

	Vector2 getDir(){
		float x = -(transform.position.x-Player.player.transform.position.x)*3;
		if(Mathf.Abs(x)<10){x=10*Mathf.Sign(x);}
		if(Random.Range(0,3)==0|| Mathf.Abs(x)>40){
			x=0;
		}
		float y = jumpHeight*Random.Range(0.7f,1.3f);
		return new Vector2(x,y);
	}


}
