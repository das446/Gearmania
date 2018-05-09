using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {

	float speed;
	Rigidbody2D rb2d;
	public bool grounded;
	float jumpForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		MoveH();
		if(Input.GetKeyDown(KeyCode.Space) && grounded){
			Jump();
		}

	}

	void MoveH(){
		float x = Input.GetAxis("Horizontal");
		rb2d.velocity=new Vector2(x*speed,rb2d.velocity.y);
	}

	void Jump(){
		rb2d.AddForce(Vector3.up*jumpForce);
	}
}
