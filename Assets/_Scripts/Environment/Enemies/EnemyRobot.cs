using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot : MonoBehaviour, IContact<Gear>, IContact<Player> {

	// Use this for initialization

	[SerializeField] float shootTime;
	public Bullet bulletPrefab;
	public Vector3 shootOffset;
	public Animator anim;
	public Collider2D col;
	public GameObject Explosion;


	public virtual void Start () {
		InvokeRepeating("Shoot",1,shootTime/2);
		col = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(transform.position.y < -10){
			Destroy(gameObject);
		}
	}

	void Shoot(){
		if(Vector2.Distance(transform.position,Player.player.transform.position)<20){
			this.PlaySound("Fire");
			Instantiate(bulletPrefab,transform.position-shootOffset,transform.rotation);
		}
	}

    public virtual void OnTrigger(Gear gear)
    {
			col.enabled=false;
			this.PlaySound("Robot_Die");

			if(Player.player.throwGear.holdingGear){
				gear.TakeDamage(10);
			}
			else{
				gear.TakeDamage(2);
			}
			GameObject e = Instantiate(Explosion,transform.position,transform.rotation);
			Destroy(e,0.5f);
			Destroy(gameObject);
    }

    public virtual void OnCollision(Gear gear)
    {
        OnTrigger(gear);
    }

    public void OnCollision(Player p)
    {
        p.Die();
    }

    public void OnTrigger(Player p)
    {
        p.Die();
    }
}
