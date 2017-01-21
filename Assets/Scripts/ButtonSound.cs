using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public SoundFile highlightSound;
    public SoundFile clickSound;
    private AudioSource audioSource;

    public void Reset()
    {
        SoundFile[] soundfiles = Resources.FindObjectsOfTypeAll<SoundFile>();
        if (!highlightSound)
        {
            foreach (SoundFile soundfile in soundfiles)
            {
                if (soundfile.name == "HighlightSound")
                {
                    highlightSound = soundfile;
                }
            }
        }
        if (!clickSound)
        {
            foreach (SoundFile soundfile in soundfiles)
            {
                if (soundfile.name == "ClickSound")
                {
                    clickSound = soundfile;
                }
            }
        }
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        audioSource.clip = highlightSound.audioClip;
        audioSource.Play();
        Debug.Log("Pointer enter");
    }

    public void OnPointerDown(PointerEventData ped)
    {
        audioSource.clip = clickSound.audioClip;
        audioSource.Play();
    }
}