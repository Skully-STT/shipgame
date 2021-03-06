﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider sldMastervolume;
    public Slider sldEffectVolume;
    public Slider sldMusicVolume;

    public AudioMixer audiomixer;

    public float MasterVolume
    {
        get
        {
            audiomixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 0f));
            return PlayerPrefs.GetFloat("MasterVolume", 0f);
        }
        set
        {
            value = value > 20f ? 20f : value < -80f ? -80f : value;
            audiomixer.SetFloat("MasterVolume", value);
            PlayerPrefs.SetFloat("MasterVolume", value);
        }
    }
    public float EffectVolume
    {
        get
        {
            audiomixer.SetFloat("EffectVolume", PlayerPrefs.GetFloat("EffectVolume", 0f));
            return PlayerPrefs.GetFloat("EffectVolume", 0f);
        }
        set
        {
            value = value > 20f ? 20f : value < -80f ? -80f : value;
            audiomixer.SetFloat("EffectVolume", value);
            PlayerPrefs.SetFloat("EffectVolume", value);
        }
    }
    public float MusicVolume
    {
        get
        {
            audiomixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0f));
            return PlayerPrefs.GetFloat("MusicVolume", 0f);
        }
        set
        {
            value = value > 20f ? 20f : value < -80f ? -80f : value;
            audiomixer.SetFloat("MusicVolume", value);
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }

    public void OnEnable()
    {
        sldMastervolume.value = MasterVolume;
        sldEffectVolume.value = EffectVolume;
        sldMusicVolume.value = MusicVolume;
    }

    public void Start()
    {
        gameObject.SetActive(false);
        sldMastervolume.value = MasterVolume;
        sldEffectVolume.value = EffectVolume;
        sldMusicVolume.value = MusicVolume;
    }
}
