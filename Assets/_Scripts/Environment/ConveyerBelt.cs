using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : Ground {

	public float speed;

	public override void Start()
	{

		base.Start();

		if(speed>0){
			GetComponent<Animator>().SetFloat("Speed",-speed/2);
		}
	}
	private void Update()
	{
		int childCount = transform.childCount;
		for(int i =0;i<childCount;i++){
			transform.GetChild(i).transform.Translate(Vector2.right*Time.deltaTime*speed,Space.World);
		}	
	}
}
