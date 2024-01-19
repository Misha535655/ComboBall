using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainButtons : MonoBehaviour
{
    [SerializeField] GameObject optionUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] TextMeshProUGUI volumeText;
    [SerializeField] TextMeshProUGUI bestScore;
    bool isPlaying = true;

    private void Start()
    {
        bestScore.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void PlayGame()
    {
        
        SceneLoader.instance.LoadLevel("Level");
    }

    public void LoadOption()
    {
        //AudioPlayer.instance.PlayClip(AudioPlayer.instance.menuButtonsClip, AudioPlayer.instance.menuButtonsVolume);
        optionUI.SetActive(true);
        menuUI.SetActive(false);
    }

    public void LoadShop()
    {
        //AudioPlayer.instance.PlayClip(AudioPlayer.instance.menuButtonsClip, AudioPlayer.instance.menuButtonsVolume);
        shopUI.SetActive(true);
        menuUI.SetActive(false);
    }

    public void BackToMenu()
    {
        //AudioPlayer.instance.PlayClip(AudioPlayer.instance.menuButtonsClip, AudioPlayer.instance.menuButtonsVolume);
        SceneLoader.instance.LoadLevel("Menu");
        optionUI.SetActive(false);
        shopUI.SetActive(false);
        menuUI.SetActive(true);
    }

    public void SwichVolume()
    {
        isPlaying = !isPlaying;
        if(isPlaying == true)
        {
            volumeText.text = "On";
            volumeText.color = new Color32(0, 255, 21, 255);
            //AudioPlayer.instance.SwitchSoundState();
        }
        if(isPlaying == false)
        {
            volumeText.text = "Off";
            volumeText.color = new Color32(255, 21, 0, 255);
            //AudioPlayer.instance.SwitchSoundState();
        }
        
    }
}
