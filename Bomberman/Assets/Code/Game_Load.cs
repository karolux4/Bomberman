using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Load : MonoBehaviour {

    public GameObject Player;
    public Sprite Heart;
    private int existing_hearts;
	// Use this for initialization
	void Start () {
        existing_hearts = Player.GetComponent<Additional_power_ups>().lifes_count;
        for (int i = 0; i <existing_hearts;i++)
        {
            GameObject obj = new GameObject("Heart"+(i+1));
            obj.AddComponent<Image>();
            obj.GetComponent<Image>().sprite = Heart;
            obj.transform.SetParent(this.gameObject.GetComponentInChildren<Canvas>().gameObject.transform);
            float x = (float)Screen.width / (float)1024 * -450 +i*100;
            float y = (float)Screen.height / (float)768 * -280;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
	}
    private void Update()
    {
        if(existing_hearts<Player.GetComponent<Additional_power_ups>().lifes_count)
        {
            for(int i=existing_hearts;i < Player.GetComponent<Additional_power_ups>().lifes_count;i++)
            {
                Debug.Log("New_heart");
                GameObject obj = new GameObject("Heart" + (i + 1));
                obj.AddComponent<Image>();
                obj.GetComponent<Image>().sprite = Heart;
                obj.transform.SetParent(this.gameObject.GetComponentInChildren<Canvas>().gameObject.transform);
                float x = (float)Screen.width / (float)1024 * -450 + i * 100;
                float y = (float)Screen.height / (float)768 * -280;
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            }
            existing_hearts = Player.GetComponent<Additional_power_ups>().lifes_count;
        }
        else if(existing_hearts> Player.GetComponent<Additional_power_ups>().lifes_count)
        {
            for(int i=existing_hearts;i> Player.GetComponent<Additional_power_ups>().lifes_count;i--)
            {
                if (i > 0)
                {
                    Destroy(GameObject.Find("Heart" + i));
                }
            }
            existing_hearts = Player.GetComponent<Additional_power_ups>().lifes_count;
        }
    }
}
