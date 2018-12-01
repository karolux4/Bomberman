using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Main_menu : MonoBehaviour {
    public AudioSource click;
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Button_click_sound()
    {
        click.Play();
    }
    public void AudioVolume(float slider_value)
    {
        AudioListener.volume = (float)slider_value/(float)100;
    }
}
