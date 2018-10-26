using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_physics : MonoBehaviour {

     public Rigidbody rd;
     public int direction = 3;
     // Update is called once per frame
     void FixedUpdate()
     {
         float prevX = transform.position.x;
         float prevZ = transform.position.z;
         var x = Input.GetAxis("Horizontal") * Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
         var z = Input.GetAxis("Vertical") * Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
         transform.Translate(x, 0, z);
         if(prevX-transform.position.x>0)
         {
             direction = 1;
         }
         else if(prevX - transform.position.x < 0)
         {
             direction = 2;
         }

         if (prevZ - transform.position.z < 0)
         {
             direction = 3;
         }
         else if (prevZ- transform.position.z > 0)
         {
             direction = 4;
         }
     }
}
