using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_up1 : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            GameObject Player = other.gameObject;
            Player.GetComponent<Shooting_physics>().bomb_power++;
            Destroy(this.gameObject);
        }
    }
}
