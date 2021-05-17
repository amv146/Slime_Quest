using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string MusicName;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(MusicName, 1f);
    }
    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat(MusicName, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(MusicName, sliderValue);
        PlayerPrefs.Save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
