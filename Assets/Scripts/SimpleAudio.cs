using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleAudio : MonoBehaviour
{
    public SoundFile soundfile;
    private AudioSource audioSource;

    public void Reset()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundfile.audioClip;
        audioSource.outputAudioMixerGroup = soundfile.audioMixerGroup;
    }
}
