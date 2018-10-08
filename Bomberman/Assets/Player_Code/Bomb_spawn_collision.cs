using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_spawn_collision : MonoBehaviour {
    public GameObject creator { get; set; }
    public bool collided { get; set; }
    private void Start()
    {
        collided = false;
        StartCoroutine(CheckForCollision());
    }
    private void OnTriggerEnter(Collider other)
    {
        collided = true;
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
            creator.GetComponent<Shooting_physics>().count--;
        }
        else
        {
            Debug.Log(this.gameObject.GetComponent<Transform>().localPosition);
            Debug.Log(creator.GetComponent<Transform>().localPosition);
            this.gameObject.AddComponent<Bomb_explosion>();
            this.gameObject.GetComponent<Bomb_explosion>().creator = creator;
        }
    }
}
