﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_physics : MonoBehaviour {

    public Rigidbody rd;
    public int direction = 3;
    public GameObject PauseMenu, UI;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void FixedUpdate()
     {
       //  float prevX = transform.position.x;
       //  float prevZ = transform.position.z;
         var x = Input.GetAxis("Horizontal") * Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
         var z = Input.GetAxis("Vertical") * Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
         transform.Translate(x, 0, z);

        if(Input.GetKeyDown(KeyCode.Escape)&&(!PauseMenu.activeInHierarchy))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            UI.SetActive(false);
            PauseMenu.SetActive(true);
        }
     }
}
