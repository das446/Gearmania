using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimSpeed : MonoBehaviour {

	// Use this for initialization
	public float min,max=1;
	void Start () {
		GetComponent<Animator>().speed=Random.Range(min,max); 
	}
	
}
