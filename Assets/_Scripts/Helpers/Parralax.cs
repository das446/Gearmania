using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour {

	public GameObject[] objects;
	public GameObject layer;

	public float nextX;

	public float spawnD;
	Player player;
	// Use this for initialization
	void Start () {
		player = Player.player;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.x >= nextX){
			//SpawnObject();
			nextX+=spawnD;
		}
	}
}


