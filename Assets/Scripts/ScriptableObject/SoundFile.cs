using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SoundFile", menuName = "Lars/Sound/AudioFile   ", order = 1)]
public class SoundFile : ScriptableObject
{
    public string objectName = "New SoundFile";
    public AudioClip audioClip;
}
