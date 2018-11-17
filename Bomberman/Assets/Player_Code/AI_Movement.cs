using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour {
    public Rigidbody rb;
    public string moving_direction;
    private string second_prev_direction = "";
	// Use this for initialization
	void Start () {
        List<string> available_directions = AvailableDirections("");
        moving_direction = available_directions[Random.Range(0, available_directions.Count)];
	}
	
	// Update is called once per frame
	void Update () {
        if(Throw_Bomb(moving_direction)&&gameObject.GetComponent<AI_Shooting>().allowed_to_throw&&gameObject.GetComponent<AI_Shooting>().count<gameObject.GetComponent<Additional_power_ups>().limit    )
        {
            gameObject.GetComponent<AI_Shooting>().Shoot();
        }
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
                 z = -0.15f;
                 break;
             case "Back":
                 z = 0.15f;
                 break;
             case "Left":
                 x = 0.15f;
                 break;
             case "Right":
                 x = -0.15f;
                 break;
         }
         transform.Translate(x, 0, z);
        List<string> available_directions = AvailableDirections(second_prev_direction);
        second_prev_direction = moving_direction;
        moving_direction = available_directions[Random.Range(0, available_directions.Count)];
    }
    private bool Throw_Bomb(string moving_direction)
    {
        RaycastHit Front, Back, Left, Right;
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs");
        switch (moving_direction)
        {
            case "Front":
                if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out Front, Mathf.Infinity,layer_mask))
                {
                    if(Front.collider.GetComponentInParent<BoxCollider>()==null)
                    {
                        if(Front.collider.tag=="Player"||Front.collider.tag=="AI")
                        {
                            return true;
                        }
                    }
                    else if(Front.distance>=1&&Front.distance<=2&&Front.collider.GetComponentInParent<BoxCollider>().tag=="Boxes")
                    {
                        return true;
                    }
                }
                break;
            case "Back":
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out Back, Mathf.Infinity, layer_mask))
                {
                    if (Back.collider.GetComponentInParent<BoxCollider>() == null)
                    {
                        if (Back.collider.tag == "Player" || Back.collider.tag == "AI")
                        {
                            return true;
                        }
                    }
                    else if (Back.distance >= 1 && Back.distance <= 2 && Back.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                    {
                        return true;
                    }
                }
                break;
            case "Left":
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Left, Mathf.Infinity, layer_mask))
                {
                    if (Left.collider.GetComponentInParent<BoxCollider>() == null)
                    {
                        if (Left.collider.tag == "Player" || Left.collider.tag == "AI")
                        {
                            return true;
                        }
                    }
                    else if (Left.distance >= 1 && Left.distance <= 2 && Left.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                    {
                        return true;
                    }
                }
                break;
            case "Right":
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Right, Mathf.Infinity, layer_mask))
                {
                    if (Right.collider.GetComponentInParent<BoxCollider>() == null)
                    {
                        if (Right.collider.tag == "Player" || Right.collider.tag == "AI")
                        {
                            return true;
                        }
                    }
                    else if (Right.distance >= 1 && Right.distance <= 2 && Right.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
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
