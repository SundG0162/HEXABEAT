using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource audioSource;
    public int offset = -520;

    public void Play()
    {
        audioSource.clip = LevelManager.Instance.levelSO.bgm;
        audioSource.Play();
    }
}
