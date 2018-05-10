using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot : MonoBehaviour {

	// Use this for initialization

	[SerializeField] float shootTime;
	public Bullet bulletPrefab;
	public Vector3 shootOffset;


	void Start () {
		InvokeRepeating("Shoot",1,shootTime/2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Shoot(){
		if(Vector2.Distance(transform.position,Player.player.transform.position)<20){
			Instantiate<Bullet>(bulletPrefab,transform.position-shootOffset,transform.rotation);
		}
	}



}
