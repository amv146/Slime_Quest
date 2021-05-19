using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC245-01/CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the Volume Sliders
*   This code is used in the Menu which is the final project for my(Luke Driscoll) unity programming class(CPSC245) that I am currently taking
*   I(Luke Driscoll) am the only person who has worked on this section
*   I needed to include this menu inside this project to show the functionality of the menu
*/
public class SetVolume : MonoBehaviour
{
    public AudioMixer Mixer;
    public string MusicName;
    public Slider Slider;

    // Start is called before the first frame update
    void Start()
    {
        Slider.value = PlayerPrefs.GetFloat(MusicName, 1f);
    }
    public void SetLevel (float sliderValue)
    {
        Mixer.SetFloat(MusicName, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(MusicName, sliderValue);
        PlayerPrefs.Save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
