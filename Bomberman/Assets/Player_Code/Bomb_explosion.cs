using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_explosion : MonoBehaviour {
    public GameObject creator { get; set; }
    public GameObject explosion_vertical { get; set; }
    public GameObject explosion_horizontal { get; set; }
    public bool exploding { get; set; }
    public float power { get; set; }
    public Coroutine Explosive;
    // Use this for initialization
    void Start () {
        exploding = false;
        Explosive = StartCoroutine(Explosion());
	}
    IEnumerator Explosion()
    {
        this.gameObject.layer = 11;
        yield return new WaitForSeconds(1);
        creator.GetComponent<Shooting_physics>().allowed_to_throw = true;
        yield return new WaitForSeconds(2);
        Explode("No");
        creator.GetComponent<Shooting_physics>().count--;
        Destroy(this.gameObject);
    }
    public void Explode(string message)
    {
        exploding = true;
        Vector3 pos = gameObject.GetComponent<Transform>().localPosition;
        float posX, posZ;
        if(pos.x>=0)
        {
            posX = (int)pos.x + 0.5f;
        }
        else
        {
            posX= Mathf.Sign(pos.x) * (Mathf.Abs((int)pos.x) + 0.5f);
        }
        if (pos.z >= 0)
        {
            posZ = (int)pos.z + 0.5f;
        }
        else
        {
            posZ = Mathf.Sign(pos.z) * (Mathf.Abs((int)pos.z) + 0.5f);
        }
        explosion_vertical.GetComponent<Transform>().localPosition = new Vector3(posX, 1.5f, posZ);
        explosion_horizontal.GetComponent<Transform>().localPosition = new Vector3(posX, 1.5f, posZ);
        explosion_vertical.GetComponent<ParticleSystem>().startSpeed = power*5f;
        explosion_horizontal.GetComponent<ParticleSystem>().startSpeed = power * 5f;
        // vertical explosion
        Instantiate(explosion_vertical);
        // horizontal explosion
        Instantiate(explosion_horizontal);
        ExplosionRays(this.gameObject,power,true,true,true,true);
        if (message == "Yes")
        {
            creator.GetComponent<Shooting_physics>().count--;
            creator.GetComponent<Shooting_physics>().allowed_to_throw = true;
            Destroy(gameObject);
        }

    }
    public void ExplosionRays(GameObject obj,float exploding_power, bool front, bool back, bool left, bool right)
    {
        int layer_mask = LayerMask.GetMask("Player", "Map", "Bombs");
        RaycastHit Front, Back, Left, Right;
        if (front)
        {
            if (Physics.Raycast(obj.GetComponent<Transform>().position, obj.GetComponent<Transform>().TransformDirection(Vector3.forward), out Front, Mathf.Infinity, layer_mask))
            {
                float distance1 = Front.distance;
                if (Front.collider.gameObject.layer != 11)
                {
                    if (Front.collider.GetComponentInParent<BoxCollider>() != null)
                    {
                        if ((distance1 < exploding_power) && (Front.collider.GetComponentInParent<BoxCollider>().tag == "Boxes"))
                        {
                            Debug.Log("Hit front");
                            Destroy(Front.collider.gameObject.GetComponentInParent<BoxCollider>().gameObject);
                        }
                    }
                    if ((distance1 < exploding_power) && (Front.collider.tag == "Player"))
                    {
                        Debug.Log("Hit player");
                        Front.collider.gameObject.GetComponent<Health>().lifes_count--;
                        ExplosionRays(Front.collider.gameObject, exploding_power - distance1, true, false, false, false);
                    }
                }
                else
                {
                    if ((distance1 < exploding_power) && (!Front.collider.gameObject.GetComponent<Bomb_explosion>().exploding))
                    {
                        Front.collider.gameObject.GetComponent<Bomb_explosion>().StopCoroutine(Front.collider.gameObject.GetComponent<Bomb_explosion>().Explosive);
                        Front.collider.gameObject.GetComponent<Bomb_explosion>().Explode("Yes");
                    }
                }
            }
        }
        if (back)
        {
            if (Physics.Raycast(obj.GetComponent<Transform>().position, obj.GetComponent<Transform>().TransformDirection(Vector3.back), out Back, Mathf.Infinity, layer_mask))
            {
                float distance1 = Back.distance;
                if (Back.collider.gameObject.layer != 11)
                {
                    if (Back.collider.GetComponentInParent<BoxCollider>() != null)
                    {
                        if ((distance1 < exploding_power) && (Back.collider.GetComponentInParent<BoxCollider>().tag == "Boxes"))
                        {
                            Debug.Log("Hit back");
                            Destroy(Back.collider.gameObject.GetComponentInParent<BoxCollider>().gameObject);
                        }
                    }
                    if ((distance1 < exploding_power) && (Back.collider.tag == "Player"))
                    {
                        Debug.Log("Hit player");
                        Back.collider.gameObject.GetComponent<Health>().lifes_count--;
                        ExplosionRays(Back.collider.gameObject, exploding_power - distance1, false, true, false, false);
                    }
                }
                else
                {
                    if ((distance1 < exploding_power) && (!Back.collider.gameObject.GetComponent<Bomb_explosion>().exploding))
                    {
                        Back.collider.gameObject.GetComponent<Bomb_explosion>().StopCoroutine(Back.collider.gameObject.GetComponent<Bomb_explosion>().Explosive);
                        Back.collider.gameObject.GetComponent<Bomb_explosion>().Explode("Yes");
                    }
                }
            }
        }
        if (left)
        {
            if (Physics.Raycast(obj.GetComponent<Transform>().position, obj.GetComponent<Transform>().TransformDirection(Vector3.left), out Left, Mathf.Infinity, layer_mask))
            {
                float distance1 = Left.distance;
                if (Left.collider.gameObject.layer != 11)
                {
                    if (Left.collider.GetComponentInParent<BoxCollider>() != null)
                    {
                        if ((distance1 < exploding_power) && (Left.collider.GetComponentInParent<BoxCollider>().tag == "Boxes"))
                        {
                            Debug.Log("Hit left");
                            Destroy(Left.collider.gameObject.GetComponentInParent<BoxCollider>().gameObject);
                        }
                    }

                    if ((distance1 < exploding_power) && (Left.collider.tag == "Player"))
                    {
                        Debug.Log("Hit player");
                        Left.collider.gameObject.GetComponent<Health>().lifes_count--;
                        ExplosionRays(Left.collider.gameObject, exploding_power - distance1, false, false, true, false);
                    }
                }
                else
                {
                    if ((distance1 < exploding_power) && (!Left.collider.gameObject.GetComponent<Bomb_explosion>().exploding))
                    {
                        Left.collider.gameObject.GetComponent<Bomb_explosion>().StopCoroutine(Left.collider.gameObject.GetComponent<Bomb_explosion>().Explosive);
                        Left.collider.gameObject.GetComponent<Bomb_explosion>().Explode("Yes");
                    }
                }
            }
        }
        if (right)
        {
            if (Physics.Raycast(obj.GetComponent<Transform>().position, obj.GetComponent<Transform>().TransformDirection(Vector3.right), out Right, Mathf.Infinity, layer_mask))
            {
                float distance1 = Right.distance;
                if (Right.collider.gameObject.layer != 11)
                {
                    if (Right.collider.GetComponentInParent<BoxCollider>() != null)
                    {
                        if ((distance1 < exploding_power) && (Right.collider.GetComponentInParent<BoxCollider>().tag == "Boxes"))
                        {
                            Debug.Log("Hit right");
                            Destroy(Right.collider.gameObject.GetComponentInParent<BoxCollider>().gameObject);
                        }
                    }

                    if ((distance1 < exploding_power) && (Right.collider.tag == "Player"))
                    {
                        Debug.Log("Hit player");
                        Right.collider.gameObject.GetComponent<Health>().lifes_count--;
                        ExplosionRays(Right.collider.gameObject, exploding_power - distance1, false, false, false, true);
                    }
                }
                else
                {
                    if ((distance1 < exploding_power) && (!Right.collider.gameObject.GetComponent<Bomb_explosion>().exploding))
                    {
                        Right.collider.gameObject.GetComponent<Bomb_explosion>().StopCoroutine(Right.collider.gameObject.GetComponent<Bomb_explosion>().Explosive);
                        Right.collider.gameObject.GetComponent<Bomb_explosion>().Explode("Yes");
                    }
                }
            }
        }
    }
}
