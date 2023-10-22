using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource audioSource;
    bool active;

    private void Update()
    {
        if (Judgement.Instance.currentTime >= 300 && !active)
        {
            active = true;
            Play();
        }
    }
    public void Play()
    {
        print(1);
        audioSource.Play();
    }
}
