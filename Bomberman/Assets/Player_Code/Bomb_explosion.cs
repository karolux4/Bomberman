using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_explosion : MonoBehaviour {
    public GameObject creator { get; set; }
    public float explosion_distance { get; set; }
    // Use this for initialization
    void Start () {
        StartCoroutine(Explosion());
	}
    private IEnumerator Explosion()
    {
        this.gameObject.layer = 11;
        yield return new WaitForSeconds(1);
        creator.GetComponent<Shooting_physics>().allowed_to_throw = true;
        yield return new WaitForSeconds(2);
        Explode();
        creator.GetComponent<Shooting_physics>().count--;
        Destroy(this.gameObject);
    }
    void Explode()
    {
        explosion_distance = 1;
        RaycastHit Front, Back, Left, Right;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out Front,Mathf.Infinity))
        {
            float distance1 = Front.distance;
            if (Front.collider.gameObject.layer != 11)
            {
                if ((distance1 < explosion_distance) && (Front.collider.GetComponentInParent<BoxCollider>().tag == "Boxes"))
                {
                    Debug.Log("Hit front");
                    Destroy(Front.collider.gameObject.GetComponentInParent<BoxCollider>().gameObject);
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out Back))
        {
            float distance1 = Back.distance;
            if (Back.collider.gameObject.layer != 11)
            {
                if ((distance1 < explosion_distance) && (Back.collider.GetComponentInParent<BoxCollider>().tag == "Boxes"))
                {
                    Debug.Log("Hit back");
                    Destroy(Back.collider.gameObject.GetComponentInParent<BoxCollider>().gameObject);
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Left))
        {
            float distance1 = Left.distance;
            if (Left.collider.gameObject.layer != 11)
            {
                if ((distance1 < explosion_distance) && (Left.collider.GetComponentInParent<BoxCollider>().tag == "Boxes"))
                {
                    Debug.Log("Hit left");
                    Destroy(Left.collider.gameObject.GetComponentInParent<BoxCollider>().gameObject);
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Right))
        {
            float distance1 = Right.distance;
            if (Right.collider.gameObject.layer != 11)
            {
                if ((distance1 < explosion_distance) && (Right.collider.GetComponentInParent<BoxCollider>().tag == "Boxes"))
                {
                    Debug.Log("Hit right");
                    Destroy(Right.collider.gameObject.GetComponentInParent<BoxCollider>().gameObject);
                }
            }
        }

    }
}
