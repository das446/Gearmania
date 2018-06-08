using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour {

	public GameObject[] objects;
	public GameObject layer;

	public float nextX;

	public float spawnD = 20;

	// Update is called once per frame
	void Update() {
		if (Player.player.transform.position.x >= nextX) {
			SpawnObject();
			nextX += spawnD + Random.Range(-1f, 1f);
		}
		transform.position = new Vector3(-Player.player.transform.position.x * 1.5f, 0, 0);

	}

	void SpawnObject() {
		GameObject g = objects.RandomItem();
		float y = Random.Range(-5, 5);
		Vector3 pos = new Vector3(-transform.position.x, y, 0);
		g = Instantiate(g, pos, Quaternion.identity);
		g.transform.parent=transform;
	}
}