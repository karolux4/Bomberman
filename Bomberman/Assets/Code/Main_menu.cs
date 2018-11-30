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
        Exit();
    }
    public void Button_click_sound()
    {
        click.Play();
    }
    public void AudioVolume(float slider_value)
    {
        AudioListener.volume = Mathf.Log10(slider_value) * 20;
    }
}
