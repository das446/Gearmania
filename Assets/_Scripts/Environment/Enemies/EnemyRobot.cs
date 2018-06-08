using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot : MonoBehaviour, IContact<Gear>, IContact<Player> {

	// Use this for initialization

	[SerializeField] float shootTime;
	public Bullet bulletPrefab;
	public Vector3 shootOffset;


	public virtual void Start () {
		InvokeRepeating("Shoot",1,shootTime/2);
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(transform.position.y < -10){
			Destroy(gameObject);
		}
	}

	void Shoot(){
		if(Vector2.Distance(transform.position,Player.player.transform.position)<20){
			Instantiate<Bullet>(bulletPrefab,transform.position-shootOffset,transform.rotation);
		}
	}

    public virtual void OnTrigger(Gear gear)
    {
        
			this.PlaySound("Robot_Die");
			if(Player.player.throwGear.holdingGear){
				gear.TakeDamage(10);
			}
			else{
				gear.TakeDamage(2);
			}
			Destroy(gameObject);
    }

    public virtual void OnCollision(Gear gear)
    {
        OnTrigger(gear);
    }

    public void OnCollision(Player p)
    {
        throw new System.NotImplementedException();
    }

    public void OnTrigger(Player p)
    {
        p.Die();
    }
}
