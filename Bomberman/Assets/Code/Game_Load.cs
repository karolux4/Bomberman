using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Load : MonoBehaviour {

    public GameObject Player;
    public Sprite Heart;
    private int existing_hearts;
    public GameObject Damage;
	// Use this for initialization
	void Start () {
        existing_hearts = Player.GetComponent<Additional_power_ups>().lifes_count;
        for (int i = 0; i <existing_hearts;i++)
        {
            float aspect_ratio = (float)Screen.width / (float)1920;
            GameObject obj = new GameObject("Heart"+(i+1));
            obj.AddComponent<Image>();
            obj.GetComponent<Image>().sprite = Heart;
            obj.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(200*aspect_ratio, 200*aspect_ratio);
            obj.transform.SetParent(this.gameObject.GetComponentInChildren<Canvas>().gameObject.transform);
            float x = -860 + i * 200;
            float y = -(((float)Screen.width / (float)Screen.height) * 1080)/2+ 100;
            Debug.Log(y);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
	}
    private void Update()
    {
        if(existing_hearts<Player.GetComponent<Additional_power_ups>().lifes_count)
        {
            for(int i=existing_hearts;i < Player.GetComponent<Additional_power_ups>().lifes_count;i++)
            {
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
            RectTransform rec = Damage.GetComponent<RectTransform>();
            rec.sizeDelta = new Vector2(Screen.width, Screen.height);
            Damage.SetActive(true);
            for(int i=existing_hearts;i> Player.GetComponent<Additional_power_ups>().lifes_count;i--)
            {
                if (i > 0)
                {
                    Destroy(GameObject.Find("Heart" + i));
                }
            }
            existing_hearts = Player.GetComponent<Additional_power_ups>().lifes_count;
            StartCoroutine(DamageWait());
        }
    }
    private IEnumerator DamageWait()
    {
        yield return new WaitForSeconds(1);
        Damage.SetActive(false);
    }
}
