using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    Vector2 prev_mouse_pos;
    Vector2 smooth;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        var mouse_delta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouse_delta = Vector2.Scale(mouse_delta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smooth.x = Mathf.Lerp(smooth.x, mouse_delta.x, 1f / smoothing);
        smooth.y = Mathf.Lerp(smooth.y, mouse_delta.y, 1f / smoothing);
        prev_mouse_pos += smooth;

        transform.localRotation = Quaternion.AngleAxis(-prev_mouse_pos.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(prev_mouse_pos.x, player.transform.up);
	}
}
