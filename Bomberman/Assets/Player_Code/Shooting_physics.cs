using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_physics : MonoBehaviour {

    public GameObject bomb;
    public GameObject player;
    public GameObject explosion_vertical;
    public GameObject explosion_horizontal;
    public float bomb_power;
    public int count { get; set; }
    public bool allowed_to_throw { get; set; }
    public int limit;
    public float strength;
    public float bomb_collision_radius;
    private void Start()
    {
        count = 0;
        allowed_to_throw = true;
    }
    // Update is called once per frame
    void Update ()
    {
        bool pressed = Input.GetKey(KeyCode.Space); //checking if player wants to shoot
        if (pressed && count < limit && allowed_to_throw)
        {
            allowed_to_throw = false;
            count++; // counting amount of player bombs that has not exploded
            Shoot(); // dropping bomb
        }
	}
    void Shoot()
    {
        float strengthX = 0, strengthZ = 0;
        Vector3 pos = player.GetComponent<Transform>().localPosition; // getting player position
        switch(Movement_physics.direction) // finding the way player is watching
         {
             case 1:
                 pos = new Vector3(pos.x - 0.7f, pos.y, pos.z);
                 strengthX = -strength;
                 break;
             case 2:
                 pos = new Vector3(pos.x + 0.7f, pos.y, pos.z);
                 strengthX = strength;
                 break;
             case 3:
                 pos = new Vector3(pos.x, pos.y, pos.z + 0.7f);
                 strengthZ = strength;
                 break;
             case 4:
                 pos = new Vector3(pos.x, pos.y, pos.z - 0.7f);
                 strengthZ = -strength;
                 break;
         }
        bomb.GetComponent<Transform>().localPosition = pos; // changing bomb location
        GameObject player_bomb= Instantiate(bomb); // creating bomb in the scene
        player_bomb.name = player.name + "_bomb_"+count; // renaming bomb
        player_bomb.layer = 12;
        SphereCollider sphereCollider = player_bomb.AddComponent<SphereCollider>() as SphereCollider; // adding colliders and rigidbody
        sphereCollider.radius = bomb_collision_radius;
        Rigidbody rb = player_bomb.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.AddForce(strengthX,0,strengthZ,ForceMode.Impulse);
        player_bomb.AddComponent<Bomb_height_bug_fix>();
        player_bomb.GetComponent<Bomb_height_bug_fix>().creator = player;
        player_bomb.AddComponent<Bomb_spawn_collision>();
        player_bomb.GetComponent<Bomb_spawn_collision>().creator = player;
        player_bomb.GetComponent<Bomb_spawn_collision>().explosion_vertical = explosion_vertical;
        player_bomb.GetComponent<Bomb_spawn_collision>().explosion_horizontal = explosion_horizontal;
        player_bomb.GetComponent<Bomb_spawn_collision>().power = bomb_power;
    }
}
