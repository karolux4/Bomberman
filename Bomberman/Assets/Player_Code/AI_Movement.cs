﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour {
    public Rigidbody rb;
    private string moving_direction;
    private string second_prev_direction = "";
	// Use this for initialization
	void Start () {
        List<string> available_directions = AvailableDirections("");
        moving_direction = available_directions[Random.Range(0, available_directions.Count)];
        Debug.Log(moving_direction);
	}
	
	// Update is called once per frame
	void Update () {
        float x = 0,  z = 0;
		switch(moving_direction)
        {
            case "Front":
                z = Time.deltaTime * 2f; //speed
                break;
            case "Back":
                z = -Time.deltaTime * 2f;
                break;
            case "Left":
                x = -Time.deltaTime * 2f;
                break;
            case "Right":
                x = Time.deltaTime * 2f;
                break;
        }
        transform.Translate(x, 0, z);
	}
    private void OnCollisionEnter(Collision collision)
    {
        float x = 0, z = 0;
         switch (moving_direction)
         {
             case "Front":
                 z = -0.2f;
                 break;
             case "Back":
                 z = 0.2f;
                 break;
             case "Left":
                 x = 0.2f;
                 break;
             case "Right":
                 x = -0.2f;
                 break;
         }
         transform.Translate(x, 0, z);
        List<string> available_directions = AvailableDirections(second_prev_direction);
        Debug.Log(available_directions.Count);
        second_prev_direction = moving_direction;
        moving_direction = available_directions[Random.Range(0, available_directions.Count)];
        Debug.Log(moving_direction);
    }
    private List<string> AvailableDirections(string prevDirection)
    {
        List<string> available_directions = new List<string>();
        RaycastHit Front, Back, Left, Right;
        int layer_mask = LayerMask.GetMask("Player", "Map");
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out Front, Mathf.Infinity,layer_mask))
        {
            if(Front.distance>1)
            {
                available_directions.Add("Front");
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out Back, Mathf.Infinity, layer_mask))
        {
            if (Back.distance > 1)
            {
                available_directions.Add("Back");
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Left, Mathf.Infinity, layer_mask))
        {
            if (Left.distance > 1)
            {
                available_directions.Add("Left");
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Right, Mathf.Infinity, layer_mask))
        {
            if (Right.distance > 1)
            {
                available_directions.Add("Right");
            }
        }
        if(available_directions.Count>1)
        {
            int index = available_directions.IndexOf(prevDirection);
            if(index!=-1)
            {
                available_directions.RemoveAt(index);
            }
        }
        return available_directions;
    }
}