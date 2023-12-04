using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource audioSource;
    public int offset = -520;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "SampleScene" || audioSource.clip == null) return;
        if(Judgement.Instance.currentTime / 1000f >= audioSource.clip.length)
        {
            GameManager.Instance.GameEnd();
        }
    }

    public void Play()
    {
        audioSource.clip = LevelManager.Instance.levelSO.bgm;
        audioSource.Play();
    }

    public void FadeOutMusic()
    {
        audioSource.DOFade(0, 0.4f);
        audioSource.clip = LevelManager.Instance.levelSO.bgm;   
    }

    public void FadeInMusic()
    {
        audioSource.Play();
        audioSource.DOFade(1, 0.8f);
    }

    

}
