using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour,IPuzzleObject,IContact<Player> {

	public Electricity other;
	public bool on = false;
	public bool main;

	public void OnCollision(Player p){
		if(on){p.Die();}
	}

	public void OnTrigger(Player p){
		if(on){p.Die();}
	}



	public void TurnOn(){
		gameObject.SetActive(true);
		other.gameObject.SetActive(false);
	}

	public void TurnOff(){
		gameObject.SetActive(false);
		other.gameObject.SetActive(true);
	}

	private void Start()
	{
		if(main){
			other.gameObject.SetActive(true);
			gameObject.SetActive(false);
		}

	}

	
}
