using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] Rigidbody2D rb2d;
	[SerializeField] bool grounded;
	[SerializeField] float jumpForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		MoveH();
		if(Input.GetKeyDown(KeyCode.UpArrow) && grounded){
			Jump();
		}

		

	}

	void MoveH(){
		float x = Input.GetAxis("Horizontal");
		rb2d.velocity=new Vector2(x*speed,rb2d.velocity.y);
		if(x<0){
			transform.eulerAngles=new Vector3(0,180,0);
		}
		else if(x>0){
			transform.eulerAngles = Vector3.zero;
		}
	}

	void Jump(){
		rb2d.AddForce(Vector3.up*jumpForce);
		grounded = false;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		grounded = true;
	}

	// IEnumerator ThrowGear(){

	// }


}
