using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_physics : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(gameObject.tag=="Player"&&gameObject.GetComponent<Additional_power_ups>().lifes_count==0)
        {
            // user interface
        }
        else if(gameObject.tag=="AI"&&gameObject.GetComponent<Additional_power_ups>().lifes_count==0)
        {
            Destroy(this.gameObject);
        }
	}
}
