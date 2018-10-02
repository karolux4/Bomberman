using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_physics : MonoBehaviour {

    public GameObject bomb;
    public Transform bomb_transform;
    public Transform player_transform;
    private int count=0;
    private int limit = 1;
	// Update is called once per frame
	void Update ()
    {
        bool pressed = Input.GetKey(KeyCode.Space); //checking if player wants to shoot
        if(pressed&&count<limit)
        {
            count++; // counting amount of player bombs that has not exploded
            Shoot(); // dropping bomb
        }
	}
    void Shoot()
    {
        Vector3 pos = player_transform.localPosition; // getting player position
        switch(Movement_physics.direction) // finding the way player is watching
         {
             case 1:
                 pos = new Vector3(pos.x - 1, pos.y, pos.z);
                 break;
             case 2:
                 pos = new Vector3(pos.x + 1, pos.y, pos.z);
                 break;
             case 3:
                 pos = new Vector3(pos.x, pos.y, pos.z+1);
                 break;
             case 4:
                 pos = new Vector3(pos.x, pos.y, pos.z-1);
                 break;
         }
        bomb_transform.localPosition = pos; // changing bomb location
        GameObject player_bomb= Instantiate(bomb); // creating bomb in the scene
        player_bomb.name = player_transform.name + "_bomb_"+count; // renaming bomb
        SphereCollider sphereCollider = player_bomb.AddComponent<SphereCollider>() as SphereCollider; // adding colliders and rigidbody
        sphereCollider.radius = 0.2f;
        Rigidbody rb = player_bomb.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        StartCoroutine(Explosion(player_bomb)); // starting explosion
    }
    IEnumerator Explosion(GameObject player_bomb) //creating bomb explosion
    {
        Debug.Log("Bomb dropped");
        yield return new WaitForSeconds(5);
        Destroy(player_bomb); // destroying bomb
        count--;
        Debug.Log("BOOM");
    }
}
