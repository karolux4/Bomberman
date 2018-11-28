using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour {
    public Rigidbody rb;
    public string moving_direction;
    private string second_prev_direction = "";
    private float covered_distance = 0;
	// Use this for initialization
	void Start () {
        List<string> available_directions = AvailableDirections("");
        moving_direction = available_directions[Random.Range(0, available_directions.Count)];
	}

    // Update is called once per frame
    void Update() {
        if (Throw_Bomb(moving_direction) && gameObject.GetComponent<AI_Shooting>().allowed_to_throw && gameObject.GetComponent<AI_Shooting>().count < gameObject.GetComponent<Additional_power_ups>().limit)
        {
            gameObject.GetComponent<AI_Shooting>().Shoot();
            DirectionChange(moving_direction);
        }
        else if (covered_distance > 1)
        {
            DirectionChange("");
        }
        CheckForBombs();
        float x = 0,  z = 0;
		switch(moving_direction)
        {
            case "Front":
                z = Time.deltaTime* gameObject.GetComponent<Additional_power_ups>().speed; //speed
                break;
            case "Back":
                z = -Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
                break;
            case "Left":
                x = -Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
                break;
            case "Right":
                x = Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
                break;
        }
        covered_distance += x + z;
        transform.Translate(x, 0, z);
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            float posX, posZ;
            CenterPosition(gameObject, out posX, out posZ);

            transform.localPosition = new Vector3(posX, 1.72f, posZ);
        }
        List<string> available_directions = AvailableDirections(second_prev_direction);
        second_prev_direction = moving_direction;
        if (available_directions.Count == 0)
        {
            moving_direction = "";
        }
        else
        {
            moving_direction = available_directions[Random.Range(0, available_directions.Count)];
        }
        covered_distance = 0;
    }
    private void CenterPosition(GameObject obj, out float posX, out float posZ)
    {
        Vector3 pos = new Vector3();
        pos = obj.GetComponent<Transform>().localPosition;
        if (pos.x >= 0)
        {
            posX = (int)pos.x + 0.5f;
        }
        else
        {
            posX = Mathf.Sign(pos.x) * (Mathf.Abs((int)pos.x) + 0.5f);
        }
        if (pos.z >= 0)
        {
            posZ = (int)pos.z + 0.5f;
        }
        else
        {
            posZ = Mathf.Sign(pos.z) * ((int)Mathf.Abs(pos.z) + 0.5f);
        }
    }
    private bool Throw_Bomb(string moving_direction)
    {
        RaycastHit Hit;
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs", "AI");
        List<string> Directions = new List<string>();
        Directions.Add("Front");
        Directions.Add("Right");
        Directions.Add("Back");
        Directions.Add("Left");
        for (int i = 0; i < 4; i++)
        {
            if (moving_direction == Directions[i])
            {
                if (Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0,90*i,0)*Vector3.forward, out Hit, Mathf.Infinity, layer_mask))
                {
                    if (Hit.distance >= 1 && Hit.distance <= 2 && Hit.collider.GetComponentInParent<BoxCollider>() == null)
                    {
                        if (Hit.collider.tag == "Player" || Hit.collider.tag == "AI")
                        {
                            return true;
                        }
                    }
                    else if (Hit.distance >= 1 && Hit.distance <= 2 && Hit.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                    {
                        return true;
                    }
                    else if (Hit.distance >= 1)
                    {
                        Vector3 bomb_position = new Vector3();
                        switch(i)
                        {
                            case 0:
                                bomb_position = transform.position + new Vector3(0f, -0.5f, 1f);
                                break;
                            case 1:
                                bomb_position = transform.position + new Vector3(1f, -0.5f, 0f);
                                break;
                            case 2:
                                bomb_position = transform.position + new Vector3(0f, -0.5f, -1f);
                                break;
                            case 3:
                                bomb_position = transform.position + new Vector3(-1f, -0.5f, 0f);
                                break;
                        }
                        if (IsThereDestroyableObject(moving_direction, bomb_position))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    private List<string> AvailableDirections(string prevDirection)
    {
        List<string> available_directions = new List<string>();
        RaycastHit Front, Back, Left, Right;
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs", "AI");
        /* Vector3 AI_back_position, AI_front_position;
         PositionCenter(out AI_back_position,out AI_front_position);//transform.position - new Vector3(0f, 0.5f, 0f);// PositionCenter();*/
        Vector3 AI_position = transform.position - new Vector3(0f, 0.5f, 0f);
        if (Physics.SphereCast(AI_position,0.3f,transform.TransformDirection(Vector3.forward),out Front, Mathf.Infinity,layer_mask))
        {
            if((Front.distance>0.4)&&(Front.collider.gameObject.layer!=11))
            {
                available_directions.Add("Front");
            }
        }
        if (Physics.SphereCast(AI_position,0.3f, transform.TransformDirection(Vector3.back), out Back, Mathf.Infinity, layer_mask))
        {
            if (Back.distance > 0.4&&(Back.collider.gameObject.layer != 11))
            {
                available_directions.Add("Back");
            }
        }
        if (Physics.SphereCast(AI_position,0.3f, transform.TransformDirection(Vector3.left), out Left, Mathf.Infinity, layer_mask))
        {
            if (Left.distance > 0.4 && (Left.collider.gameObject.layer != 11))
            {
                available_directions.Add("Left");
            }
        }
        if (Physics.SphereCast(AI_position,0.3f, transform.TransformDirection(Vector3.right), out Right, Mathf.Infinity, layer_mask))
        {
            if (Right.distance > 0.4 && (Right.collider.gameObject.layer != 11))
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
    private bool IsThereDestroyableObject(string moving_direction, Vector3 bomb_position)
    {
        RaycastHit Hit;
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs", "AI");
        List<string> Directions = new List<string>();
        Directions.Add("Back");
        Directions.Add("Left");
        Directions.Add("Front");
        Directions.Add("Right");
        for (int i = 0; i < 4; i++)
        {
            if (moving_direction != Directions[i])
            {
                if (Physics.Raycast(bomb_position, Quaternion.Euler(0,90*i,0)*Vector3.forward, out Hit, Mathf.Infinity, layer_mask))
                {
                    if (Hit.collider.GetComponentInParent<BoxCollider>() == null)
                    {
                        if (Hit.distance <= 1 && Hit.collider.tag == "Player" || Hit.collider.tag == "AI")
                        {
                            return true;
                        }
                        if (Hit.distance <= 2 && Hit.collider.tag == "Bombs")
                        {
                            DirectionChange(moving_direction);
                        }
                    }
                    else if (Hit.distance <= 1 && Hit.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void DirectionChange(string prev_direction)
    {
        List<string> available_directions = AvailableDirections(prev_direction);
        if (available_directions.Count == 0)
        {
            moving_direction = "";
        }
        else
        {
            string temp_moving_direction = available_directions[Random.Range(0, available_directions.Count)];
            if (prev_direction == "")
            {
                switch (temp_moving_direction)
                {
                    case "Front":
                        if (moving_direction != "Back")
                        {
                            moving_direction = temp_moving_direction;
                            // PositionNeutralization();
                            covered_distance = 0;
                        }
                        break;
                    case "Back":
                        if (moving_direction != "Front")
                        {
                            moving_direction = temp_moving_direction;
                            // PositionNeutralization();
                            covered_distance = 0;
                        }
                        break;
                    case "Left":
                        if (moving_direction != "Right")
                        {
                            moving_direction = temp_moving_direction;
                            // PositionNeutralization();
                            covered_distance = 0;
                        }
                        break;
                    case "Right":
                        if (moving_direction != "Left")
                        {
                            moving_direction = temp_moving_direction;
                            //PositionNeutralization();
                            covered_distance = 0;
                        }
                        break;
                }
            }
            else
            {
                moving_direction = temp_moving_direction;
            }
        }
    }
    private void CheckForBombs()
    {
        RaycastHit Front, Back, Left, Right;
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs", "AI");
        switch (moving_direction)
        {
            case "Front":
                if (Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), transform.TransformDirection(Vector3.forward), out Front, Mathf.Infinity, layer_mask))
                {
                    if(Front.collider.tag=="Bombs")
                    {
                        DirectionChange(moving_direction);
                    }
                }
                    break;
            case "Back":
                if (Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), transform.TransformDirection(Vector3.back), out Back, Mathf.Infinity, layer_mask))
                {
                    if(Back.collider.tag=="Bombs")
                    {
                        DirectionChange(moving_direction);
                    }
                }
                break;
            case "Left":
                if (Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), transform.TransformDirection(Vector3.left), out Left, Mathf.Infinity, layer_mask))
                {
                    if (Left.collider.tag == "Bombs")
                    {
                        DirectionChange(moving_direction);
                    }
                }
                break;
            case "Right":
                if (Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), transform.TransformDirection(Vector3.right), out Right, Mathf.Infinity, layer_mask))
                {
                    if (Right.collider.tag == "Bombs")
                    {
                        DirectionChange(moving_direction);
                    }
                }
                break;
            case "":
                {
                    DirectionChange("");
                    break;
                }
        }
    }
}
