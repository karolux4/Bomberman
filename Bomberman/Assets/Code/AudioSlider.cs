﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSlider : MonoBehaviour {
    public Slider slider;
    public GameObject text;
    public void Start()
    {
        slider.value = Mathf.Pow(10, (float)AudioListener.volume / (float)20);
    }
    public void Update()
    {
        text.GetComponent<TextMeshProUGUI>().text = ((int)slider.value).ToString();
    }
}