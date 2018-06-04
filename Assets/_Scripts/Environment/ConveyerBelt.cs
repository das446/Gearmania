using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : Ground {

	public float speed;
	private void Update()
	{
		int childCount = transform.childCount;
		for(int i =0;i<childCount;i++){
			transform.GetChild(i).transform.Translate(Vector2.right*Time.deltaTime*speed,Space.World);
		}	
	}
}
