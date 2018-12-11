using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_height_bug_fix : MonoBehaviour {
    public GameObject creator { get; set; }
    private float prevY;
	// Use this for initialization
	void Start () {
        prevY = gameObject.GetComponent<Transform>().localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Transform>().localPosition.y>prevY)
        {
            Destroy(this.gameObject);
            if (creator.tag == "Player")
            {
                creator.GetComponent<Shooting_physics>().allowed_to_throw = true;
                if (creator.GetComponent<Shooting_physics>().count > 0)
                {
                    creator.GetComponent<Shooting_physics>().count--;
                }
            }
            else
            {
                creator.GetComponent<AI_Shooting>().allowed_to_throw = true;
                if (creator.GetComponent<AI_Shooting>().count > 0)
                {
                    creator.GetComponent<AI_Shooting>().count--;
                }
            }
        }
	}
}
