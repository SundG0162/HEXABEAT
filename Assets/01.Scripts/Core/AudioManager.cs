using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource audioSource;
    public int offset = -520;

    public void Play()
    {
        audioSource.Play();
    }
}
