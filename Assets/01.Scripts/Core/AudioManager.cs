using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource audioSource;
    public int offset = -520;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        print(1);
        audioSource.clip = LevelManager.Instance.levelSO.bgm;
        audioSource.Play();
    }
}
