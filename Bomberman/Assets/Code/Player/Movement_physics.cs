using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_physics : MonoBehaviour {
    public GameObject PauseMenu, UI, Stats;
    private Animator animator;
    public string Horizontal_Axis;
    public string Vertical_Axis;
    private void Start()
    {
        animator = new Animator();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void FixedUpdate()
     {
       //  float prevX = transform.position.x;
       //  float prevZ = transform.position.z;
        var x = Input.GetAxis(Horizontal_Axis) * Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
        var z = Input.GetAxis(Vertical_Axis) * Time.deltaTime * gameObject.GetComponent<Additional_power_ups>().speed;
        transform.Translate(x, 0, z);

        animator = this.gameObject.GetComponent<Animator>();
        if (x != 0 || z != 0)
        {
            animator.SetTrigger("Walking");
            animator.ResetTrigger("Standing");
        }
        else
        {
            animator.SetTrigger("Standing");
            animator.ResetTrigger("Walking");
        }

        if (Input.GetKey(KeyCode.Escape)&&(!PauseMenu.activeInHierarchy))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UI.SetActive(false);
            PauseMenu.SetActive(true);
        }
        if(Input.GetKey(KeyCode.Tab)&&(!Stats.activeInHierarchy))
        {
            Stats.SetActive(true);
        }
        else if(!Input.GetKey(KeyCode.Tab))
        {
            Stats.SetActive(false);
        }
     }
}
