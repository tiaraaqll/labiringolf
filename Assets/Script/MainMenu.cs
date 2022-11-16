using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle toggle;

    public void SliderVolume()
    {
        SoundsManager.Instance.audioSource.volume = volumeSlider.value;
    }

    public void SetMute()
    {
        if (toggle.isOn == true)
        {
            SoundsManager.Instance.audioSource.mute = false;
        }
        else
        {
            SoundsManager.Instance.audioSource.mute = true;
        }
    }

    private void Start()
    {
        if (SoundsManager.Instance.audioSource.mute == true)
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }
        
        volumeSlider.value = SoundsManager.Instance.audioSource.volume;
        SoundsManager.Instance.LoadVolume();
    }

    //tombol exit
    public void ExitGame () {
        Application.Quit();
        Debug.Log("Quit Game Success");
    }
    
    //tombol play
    public void PlayGame (string Level1) {
        SceneManager.LoadScene(Level1);
        Debug.Log("Ini Scene Level 1 Aktif" + Level1);
    }

    public void PlayLevel2 (string Level2) {
        SceneManager.LoadScene(Level2);
        Debug.Log("Ini Scene Level 2 Aktif" + Level2);
    }

    public void PlayLevel3 (string Level3) {
        SceneManager.LoadScene(Level3);
        Debug.Log("Ini Scene Level 3 Aktif" + Level3);
    }
}
