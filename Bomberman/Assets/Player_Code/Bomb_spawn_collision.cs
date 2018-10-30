using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_spawn_collision : MonoBehaviour {
    public GameObject creator { get; set; }
    public GameObject explosion_vertical { get; set; }
    public GameObject explosion_horizontal { get; set; }
    public bool collided { get; set; }
    private int bounce_count;
    private bool kicked = false;
    private void Start()
    {
        bounce_count = 0;
        collided = false;
        StartCoroutine(CheckForCollision());
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Map Objects")&&(bounce_count<creator.GetComponent<Additional_power_ups>().bounce_limit))
        {
            bounce_count++;
        }
        else if (other.tag == "Map Objects")
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else if((other.tag=="Boxes")||(other.tag=="Walls"))
        {
            collided = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.tag == "Map Objects") && (bounce_count < creator.GetComponent<Additional_power_ups>().bounce_limit))
        {
            bounce_count++;
        }
        else if ((other.tag == "Map Objects")&&(!kicked))
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
          if((collision.gameObject.tag=="Player")&&(creator.GetComponent<Additional_power_ups>().bomb_kick))
          {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            Vector3 direction = creator.GetComponent<Transform>().forward; // finding the way player is watching
            gameObject.GetComponent<Rigidbody>().AddForce(direction*10f, ForceMode.Impulse);
            gameObject.GetComponent<SphereCollider>().material = null;
            kicked = true;
          }
    }
    private IEnumerator CheckForCollision()
    {
        yield return null;
        this.gameObject.GetComponent<Transform>().position = this.gameObject.GetComponent<Transform>().position + Vector3.zero;
        yield return null;
        Debug.Log(collided);
        if (collided)
        {
            Debug.Log(this.gameObject.GetComponent<Transform>().localPosition);
            Debug.Log(creator.GetComponent<Transform>().localPosition);
            Destroy(this.gameObject);
            creator.GetComponent<Shooting_physics>().allowed_to_throw = true;
            if (creator.GetComponent<Shooting_physics>().count > 0)
            {
                creator.GetComponent<Shooting_physics>().count--;
            }
        }
        else
        {
            Debug.Log(this.gameObject.GetComponent<Transform>().localPosition);
            Debug.Log(creator.GetComponent<Transform>().localPosition);
            this.gameObject.AddComponent<Bomb_explosion>();
            this.gameObject.GetComponent<Bomb_explosion>().creator = creator;
            this.gameObject.GetComponent<Bomb_explosion>().explosion_vertical = explosion_vertical;
            this.gameObject.GetComponent<Bomb_explosion>().explosion_horizontal = explosion_horizontal;
        }
    }
}
