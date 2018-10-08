using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_explosion : MonoBehaviour {
    public GameObject creator { get; set; }
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
        creator.GetComponent<Shooting_physics>().count--;
        Destroy(this.gameObject);
    }
}
