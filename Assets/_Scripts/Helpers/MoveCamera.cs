using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public class MoveCamera : MonoBehaviour {

   
    public GameObject Target;
    public float maxX,minX, maxY, minY;
    public float WorldMinX=Mathf.NegativeInfinity, WorldMaxX=Mathf.Infinity, WorldMinY=Mathf.NegativeInfinity, WorldMaxY=Mathf.Infinity;
    //public DrawJump dj;
    public Vector3 Offset;
	// Use this for initialization
	void Start () {
        GameObject levelCanvas = GameObject.Find("Level Canvas");
        if (levelCanvas == null) { return; }
        if (levelCanvas.activeInHierarchy==false) { levelCanvas.SetActive(true); }
    }
	
	// Update is called once per frame
	void Update () {
        if (Target == null) { Target = GameObject.Find("Player"); }
        if (Target == null) { return; }
        Vector3 target=Target.transform.position+Offset;
        if (target.x-transform.position.x > maxX)
        {
            transform.position = new Vector3(target.x-maxX, transform.position.y,transform.position.z);
        }
        if (target.y - transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, target.y - maxY, transform.position.z);
        }
        if (transform.position.y - target.y > minY)
        {
            transform.position = new Vector3(transform.position.x, target.y + minY, transform.position.z);
        }
        if (transform.position.x - target.x > minX)
        {
            transform.position = new Vector3(target.x + minX, transform.position.y, transform.position.z);
        }
        //Adjust To World Bounds
        
        if (transform.position.x < WorldMinX) { transform.position = transform.position.setX(WorldMinX); }
        if (transform.position.x > WorldMaxX) { transform.position = transform.position.setX(WorldMaxX); }
        if (transform.position.y < WorldMinY) { transform.position = transform.position.setY(WorldMinY); }
        if (transform.position.y > WorldMaxY) { transform.position = transform.position.setY(WorldMaxY); }
        


    }

}
