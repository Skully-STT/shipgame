using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider sldMastervolume;
    public Slider sldEffectVolume;
    public Slider sldMusicVolume;

    public float MasterVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("MasterVolume", 0f);
        }
        set
        {
            value = value > 20f ? 20f : value < -80f ? -80f : value;
            PlayerPrefs.SetFloat("MasterVolume", value);
        }
    }
    public float EffectVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("EffectVolume", 0f);
        }
        set
        {
            value = value > 20f ? 20f : value < -80f ? -80f : value;
            PlayerPrefs.SetFloat("EffectVolume", value);
        }
    }
    public float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("MusicVolume", 0f);
        }
        set
        {
            value = value > 20f ? 20f : value < -80f ? -80f : value;
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }

    public void OnEnable()
    {
        sldMastervolume.value = MasterVolume;
        sldEffectVolume.value = EffectVolume;
        sldMusicVolume.value = MusicVolume;
    }

}
