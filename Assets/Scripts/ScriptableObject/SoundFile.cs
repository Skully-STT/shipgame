using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundFile", menuName = "Lars/Sound/AudioFile   ", order = 1)]
public class SoundFile : ScriptableObject
{
    public string objectName = "New SoundFile";
    public AudioClip audioClip;
    public AudioMixerGroup audioMixerGroup;
}
