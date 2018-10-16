using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_up1 : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            GameObject Player = other.gameObject;
            if (gameObject.tag == "Strength_power_up")
            {
                Player.GetComponent<Shooting_physics>().bomb_power++;
            }
            else if(gameObject.tag == "Limit_power_up")
            {
                Player.GetComponent<Shooting_physics>().limit++;
            }
            Destroy(this.gameObject);
        }
    }
}
