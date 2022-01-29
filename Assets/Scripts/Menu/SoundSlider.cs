using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerTarget = "None";
    private void Start()
    {
        if (PlayerPrefs.HasKey(mixerTarget))
        {
            var value = PlayerPrefs.GetFloat(mixerTarget);
            GetComponent<Slider>().value = value;
            ModifyValue(value);
        }
        else
        {
            ModifyValue(GetComponent<Slider>().value);
        }
    }
    public void ModifyValue(float value)
    {
        mixer.SetFloat(mixerTarget, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(mixerTarget, value);
        PlayerPrefs.Save();
    }
}
