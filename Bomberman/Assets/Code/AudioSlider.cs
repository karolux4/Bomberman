﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour {
    public Slider slider;
    public GameObject text;
    public AudioMixer mixer;
    public void Start()
    {
       float vol=0;
       if(slider.name=="Master")
        {
            mixer.GetFloat("masterVol", out vol);
        }
       else if(slider.name=="SFX")
        {
            mixer.GetFloat("sfxVol", out vol);
        }
       else if(slider.name=="Music")
        {
            mixer.GetFloat("musicVol", out vol);
        }
       slider.value = Mathf.Pow(10,(float)vol/(float)20);
    }
    public void Update()
    {
        text.GetComponent<TextMeshProUGUI>().text =((int)((slider.value*100))).ToString();
    }
}
