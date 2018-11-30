using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_physics : MonoBehaviour {
    public GameObject UI;
    public GameObject Death_Menu;
	// Update is called once per frame
	void Update () {
		if((gameObject.tag=="Player"&&gameObject.GetComponent<Additional_power_ups>().lifes_count==0)&&(!Death_Menu.activeInHierarchy))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UI.SetActive(false);
            Death_Menu.SetActive(true);
            Destroy(this.gameObject);
        }
        else if(gameObject.tag=="AI"&&gameObject.GetComponent<Additional_power_ups>().lifes_count==0)
        {
            Destroy(this.gameObject);
        }
	}
}
