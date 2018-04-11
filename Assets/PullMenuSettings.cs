using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PullMenuSettings : MonoBehaviour {

    bool settingsOpen = false;
    Image bulletSettings;
    Image circleSettings;
    Image background;
    Image sound;
    Image scroll;
    Image handle;
    Image help;

    public void Start()
    {
        ChangeVolume();
    }
    public void SettingsButtonOpen()
    {
       help = GameObject.Find("HelpClosed").GetComponent<Image>();
       help.enabled = false;

        circleSettings = GameObject.Find("SettingsOpen").GetComponent<Image>();
        bulletSettings = GameObject.Find("SettingsClosed").GetComponent<Image>();
        background = GameObject.Find("SettingsBackground").GetComponent<Image>();
        sound = GameObject.Find("SoundChoice").GetComponent<Image>();
        scroll = GameObject.Find("Volume").GetComponent<Image>();
        handle = GameObject.Find("VolumeHandle").GetComponent<Image>();
        circleSettings.enabled = true;
        bulletSettings.enabled = false;
        background.enabled = true;
        sound.enabled = true;
        scroll.enabled = true;
        handle.enabled = true;

        Debug.Log("Settings menu open");
        settingsOpen = true;
    }

    public void SettingsButtonClose()
    {
        background.enabled = false;
        help.enabled = true;
        circleSettings.enabled = false;
        bulletSettings.enabled = true;
        Debug.Log("Settings menu closed");
        settingsOpen = false;
        sound.enabled = false;
        scroll.enabled = false;
        handle.enabled = false;
    }

    public void BackgroundClick()
    {

    }

    public void ChangeVolume()
    {

        Debug.Log(GameObject.Find("Volume").GetComponent<Scrollbar>().value.ToString());
        float newVol = AudioListener.volume;
        newVol = GameObject.Find("Volume").GetComponent<Scrollbar>().value;
        AudioListener.volume = newVol;
    }


}
