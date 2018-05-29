using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleSwitch : MonoBehaviour, IContact<Gear> {

	public GameObject puzzle;
	[SerializeField] public IPuzzleObject Ipuzzle;
	bool valid = false;

	bool rotate = false;

	float rotSpeed = 50;

	bool enable = true;
	[SerializeField]LineRenderer lr;

	private void Start() {
		Ipuzzle = puzzle.GetComponent<IPuzzleObject>();
		lr.SetPosition(0,transform.position);
		lr.SetPosition(1,puzzle.transform.position);

	}

	void Update() {
		if (rotate) {
			transform.Rotate(Vector3.back * Time.deltaTime * rotSpeed);
		}
	}

	public void OnCollision(Gear gear) {
		
	}

	public void OnTrigger(Gear gear) {
		if(!enable){return;}
		gear.StopAllCoroutines();
		Player.player.throwGear.StopAllCoroutines();
		Player.player.throwGear.DropGear();
		gear.transform.position = transform.position;
		gear.rb2d.bodyType = RigidbodyType2D.Kinematic;
		Ipuzzle.TurnOn();
		gear.transform.parent = transform;
		rotate = true;
		gear.rb2d.velocity = Vector2.zero;
		Player.player.throwGear.SetLine(Vector2.zero, Vector2.zero);
		enable = false;
		this.PlaySound("Switch",true);

	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.GetComponent<Gear>()) {
			Ipuzzle.TurnOff();
			other.transform.parent = null;
			rotate = false;
			other.transform.rotation = Quaternion.identity;
			//see enableCol
			Invoke("enableCol",0.5f);
		}
	}

	/// <summary>
	/// This is used because the gear kept immediately reentering the trigger after exiting
	/// </summary>
	void enableCol(){
		enable = true;
	}
}

public interface IPuzzleObject {
	void TurnOn();
	void TurnOff();
}