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
                z = Mathf.Round(Time.deltaTime * 100) / 100 * gameObject.GetComponent<Additional_power_ups>().speed; //speed
                break;
            case "Back":
                z = -Mathf.Round(Time.deltaTime * 100) / 100 * gameObject.GetComponent<Additional_power_ups>().speed;
                break;
            case "Left":
                x = -Mathf.Round(Time.deltaTime * 100) / 100 * gameObject.GetComponent<Additional_power_ups>().speed;
                break;
            case "Right":
                x = Mathf.Round(Time.deltaTime*100)/100 * gameObject.GetComponent<Additional_power_ups>().speed;
                break;
        }
        covered_distance += x + z;
        transform.Translate(x, 0, z);
	}
    private void OnCollisionEnter(Collision collision)
    {
        float x = 0, z = 0;
        if (collision.gameObject.tag != "Player")
        {
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
        }
        List<string> available_directions = AvailableDirections(second_prev_direction);
        second_prev_direction = moving_direction;
        moving_direction = available_directions[Random.Range(0, available_directions.Count)];
        covered_distance = 0;
    }
    private bool Throw_Bomb(string moving_direction)
    {
        RaycastHit Front, Back, Left, Right;
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs", "AI");
        switch (moving_direction)
        {
            case "Front":
                if(Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), transform.TransformDirection(Vector3.forward),out Front, Mathf.Infinity,layer_mask))
                {
                    if(Front.distance >= 1 && Front.distance <= 2&&Front.collider.GetComponentInParent<BoxCollider>()==null)
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
                    else if(Front.distance>=1&&IsThereDestroyableObject(moving_direction, transform.position + new Vector3(0f, -0.5f, 1f)))
                    {
                        return true;
                    }
                }
                break;
            case "Back":
                if (Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), transform.TransformDirection(Vector3.back), out Back, Mathf.Infinity, layer_mask))
                {
                    if (Back.distance >= 1 && Back.distance <= 2 && Back.collider.GetComponentInParent<BoxCollider>() == null)
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
                    else if (Back.distance >= 1 && IsThereDestroyableObject(moving_direction, transform.position + new Vector3(0f, -0.5f, -1f)))
                    {
                        return true;
                    }
                }
                break;
            case "Left":
                if (Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), transform.TransformDirection(Vector3.left), out Left, Mathf.Infinity, layer_mask))
                {
                    if (Left.distance >= 1 && Left.distance <= 2 && Left.collider.GetComponentInParent<BoxCollider>() == null)
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
                    else if (Left.distance >= 1 && IsThereDestroyableObject(moving_direction, transform.position + new Vector3(-1f, -0.5f, 0f)))
                    {
                        return true;
                    }
                }
                break;
            case "Right":
                if (Physics.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), transform.TransformDirection(Vector3.right), out Right, Mathf.Infinity, layer_mask))
                {
                    if (Right.distance >= 1 && Right.distance <= 2 && Right.collider.GetComponentInParent<BoxCollider>() == null)
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
                    else if (Right.distance >= 1 && IsThereDestroyableObject(moving_direction, transform.position + new Vector3(1f, -0.5f, 0f)))
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
        RaycastHit Front, Back, Left, Right, Front1, Back1, Left1, Right1;
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs", "AI");
        Vector3 AI_back_position, AI_front_position;
        PositionCenter(out AI_back_position,out AI_front_position);//transform.position - new Vector3(0f, 0.5f, 0f);// PositionCenter();
        if ((Physics.Raycast(AI_back_position,transform.TransformDirection(Vector3.forward),out Front, Mathf.Infinity,layer_mask))
            && (Physics.Raycast(AI_front_position, transform.TransformDirection(Vector3.forward), out Front1, Mathf.Infinity, layer_mask)))
        {
            if((Front.distance>0.75)&&(Front1.distance>0.75)&&(Front.collider.gameObject.layer!=11))
            {
                available_directions.Add("Front");
            }
        }
        if ((Physics.Raycast(AI_back_position, transform.TransformDirection(Vector3.back), out Back, Mathf.Infinity, layer_mask))&&
            (Physics.Raycast(AI_front_position, transform.TransformDirection(Vector3.back), out Back1, Mathf.Infinity, layer_mask)))
        {
            if (Back.distance > 0.75 &&(Back1.distance > 0.75)&&(Back.collider.gameObject.layer != 11))
            {
                available_directions.Add("Back");
            }
        }
        if ((Physics.Raycast(AI_back_position, transform.TransformDirection(Vector3.left), out Left, Mathf.Infinity, layer_mask))&&
            (Physics.Raycast(AI_front_position, transform.TransformDirection(Vector3.left), out Left1, Mathf.Infinity, layer_mask)))
        {
            if (Left.distance > 0.75 && (Left1.distance > 0.75) && (Left.collider.gameObject.layer != 11))
            {
                available_directions.Add("Left");
            }
        }
        if ((Physics.Raycast(AI_back_position, transform.TransformDirection(Vector3.right), out Right, Mathf.Infinity, layer_mask))&&
            (Physics.Raycast(AI_front_position, transform.TransformDirection(Vector3.right), out Right1, Mathf.Infinity, layer_mask)))
        {
            if (Right.distance > 0.75 && (Right1.distance > 0.75) && (Right.collider.gameObject.layer != 11))
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
        RaycastHit Front, Back, Left, Right;
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs", "AI");
        if (moving_direction != "Back")
        {
            if (Physics.Raycast(bomb_position, transform.TransformDirection(Vector3.forward), out Front, Mathf.Infinity, layer_mask))
            {
                if (Front.collider.GetComponentInParent<BoxCollider>() == null)
                {
                    if (Front.distance <= 1 && Front.collider.tag == "Player" || Front.collider.tag == "AI")
                    {
                        return true;
                    }
                    if(Front.distance<=2 && Front.collider.tag=="Bombs")
                    {
                        DirectionChange(moving_direction);
                    }
                }
                else if (Front.distance <= 1 && Front.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                {
                    return true;
                }
            }
        }
        if (moving_direction != "Front")
        {
            if (Physics.Raycast(bomb_position, transform.TransformDirection(Vector3.back), out Back, Mathf.Infinity, layer_mask))
            {
                if (Back.collider.GetComponentInParent<BoxCollider>() == null)
                {
                    if (Back.distance <= 1 && Back.collider.tag == "Player" || Back.collider.tag == "AI")
                    {
                        return true;
                    }
                    if (Back.distance <= 2 && Back.collider.tag == "Bombs")
                    {
                        DirectionChange(moving_direction);
                    }
                }
                else if (Back.distance <= 1 && Back.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                {
                    return true;
                }
            }
        }
        if (moving_direction != "Right")
        {
            if (Physics.Raycast(bomb_position, transform.TransformDirection(Vector3.left), out Left, Mathf.Infinity, layer_mask))
            {
                if ( Left.collider.GetComponentInParent<BoxCollider>() == null)
                {
                    if (Left.distance <= 1 && Left.collider.tag == "Player" || Left.collider.tag == "AI")
                    {
                        return true;
                    }
                    if (Left.distance <= 2 && Left.collider.tag == "Bombs")
                    {
                       DirectionChange(moving_direction);
                    }
                }
                else if (Left.distance <= 1 && Left.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                {
                    return true;
                }
            }
        }
        if (moving_direction != "Left")
        {
            if (Physics.Raycast(bomb_position, transform.TransformDirection(Vector3.right), out Right, Mathf.Infinity, layer_mask))
            {
                if ( Right.collider.GetComponentInParent<BoxCollider>() == null)
                {
                    if (Right.distance <= 1 && Right.collider.tag == "Player" || Right.collider.tag == "AI")
                    {
                        return true;
                    }
                    if (Right.distance <= 2 && Right.collider.tag == "Bombs")
                    {
                       DirectionChange(moving_direction);
                    }
                }
                else if (Right.distance<=1 && Right.collider.GetComponentInParent<BoxCollider>().tag == "Boxes")
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void DirectionChange(string prev_direction)
    {
        List<string> available_directions = AvailableDirections(prev_direction);
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
        }
    }
    private void PositionCenter(out Vector3 back_position, out Vector3 front_position) // in progress
    {
        back_position = transform.position;
        front_position = transform.position;
        switch(moving_direction)
        {
            case "Front":
                back_position = transform.position + new Vector3(0f, -0.5f, -0.282f);
                front_position = transform.position + new Vector3(0f, -0.5f, 0.282f);
                break;
            case "Back":
                back_position = transform.position + new Vector3(0f, -0.5f, 0.282f);
                front_position = transform.position + new Vector3(0f, -0.5f, -0.282f);
                break;
            case "Left":
                back_position = transform.position + new Vector3(0.282f, -0.5f, 0f);
                front_position = transform.position + new Vector3(-0.282f, -0.5f, 0f);
                break;
            case "Right":
                back_position = transform.position + new Vector3(-0.282f, -0.5f, 0f);
                front_position = transform.position + new Vector3(0.282f, -0.5f, 0f);
                break;
        }
    }
}
