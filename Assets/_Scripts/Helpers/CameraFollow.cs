using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float maxDist;
	public float minDist;
	public float maxSize = 16;
	public float minSize = 8;

	public float defaultY;

	public float zoomSpeed;

	public GameObject background1;
	public GameObject background2;
	float d;
	public float BgScrollSpeed = 1;

	void Start() {
		defaultY = transform.position.y;
		//d = background2.transform.position.x - background1.transform.position.x;
	}

	void Update() {
		transform.position = new Vector3(Player.player.transform.position.x, transform.position.y, transform.position.z);
		SetZoom();
		if (Input.GetKey(KeyCode.P) && Camera.main.orthographicSize <= maxSize) {
			Zoom(2);
		} else if (Input.GetKey(KeyCode.O) && Camera.main.orthographicSize >= minSize) {
			Zoom(-2);
		}
		
		background1.transform.position= new Vector3(-transform.position.x/BgScrollSpeed,0,0);
		//background2.transform.position= new Vector3(-transform.position.x/BgScrollSpeed + d,0,0);

	}

	void SetZoom() {
		float dist = Mathf.Abs(Player.player.transform.position.x - Player.player.gear.transform.position.x);
		if (dist > minDist) { }
	}

	/// <summary>
	/// Zooms camera a certain amount
	/// </summary>
	/// <param name="amnt">Positive amount zooms out</param>
	void Zoom(float amnt) {
		Camera.main.orthographicSize += amnt * Time.deltaTime;
		transform.Translate(Vector2.up * Time.deltaTime * amnt);
	}
}