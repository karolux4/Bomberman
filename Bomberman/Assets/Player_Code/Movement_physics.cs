using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_physics : MonoBehaviour {

    public Rigidbody rd;
    // Update is called once per frame
    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;
        transform.Translate(x, 0, z);
	}
}
