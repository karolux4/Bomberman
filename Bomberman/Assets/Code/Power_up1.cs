﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_up1 : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"||other.tag=="AI")
        {
            GameObject Player = other.gameObject;
            if (gameObject.tag == "Strength_power_up")
            {
                Player.GetComponent<Additional_power_ups>().bomb_power++;
            }
            else if(gameObject.tag == "Limit_power_up")
            {
                Player.GetComponent<Additional_power_ups>().limit++;
            }
            else if(gameObject.tag == "Bounce_power_up")
            {
                Player.GetComponent<Additional_power_ups>().bounce_limit += 10;
            }
            else if(gameObject.tag == "Speed_power_up")
            {
                if (Player.GetComponent<Additional_power_ups>().speed != 3)
                {
                    Player.GetComponent<Additional_power_ups>().speed += 0.5f;
                }
            }
            else if(gameObject.tag=="Extra_life_power_up")
            {
                Player.GetComponent<Additional_power_ups>().lifes_count++;
            }
            else if(gameObject.tag=="Bomb_kick_power_up")
            {
                Player.GetComponent<Additional_power_ups>().bomb_kick = true;
            }
            Destroy(this.gameObject);
        }
    }
}