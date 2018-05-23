﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleSwitch : MonoBehaviour, IGearContact
{

	public GameObject puzzle;
	[SerializeField] public IPuzzleObject Ipuzzle;
	bool valid = false;

	bool rotate = false;

	float rotSpeed=50;

    private void Start()
	{
		Ipuzzle = puzzle.GetComponent<IPuzzleObject>();
	}

	void Update()
	{
		if(rotate){
			transform.Rotate(Vector3.back*Time.deltaTime*rotSpeed);
		}
	}
    
    public void OnGearCollision(Gear gear)
    {
        throw new System.NotImplementedException();
    }

    public void OnGearTrigger(Gear gear)
    {
        gear.StopAllCoroutines();
		Player.player.throwGear.DropGear();
		gear.transform.position=transform.position;
		gear.rb2d.bodyType = RigidbodyType2D.Kinematic;
		Ipuzzle.TurnOn();
		gear.transform.parent=transform;
		rotate = true;
    }

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<Gear>()){
			Ipuzzle.TurnOff();
			other.transform.parent = null;
			rotate = false;
		}
	}
}

public interface IPuzzleObject{
	void TurnOn();
	void TurnOff();
}
