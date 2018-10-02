using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_physics : MonoBehaviour {

     public Rigidbody rd;
     public static int direction = 3;
     // Update is called once per frame
     void FixedUpdate()
     {
         float prevX = transform.position.x;
         float prevZ = transform.position.z;
         var x = Input.GetAxis("Horizontal") * Time.deltaTime * 2.0f;
         var z = Input.GetAxis("Vertical") * Time.deltaTime * 2.0f;
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
