using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleSeq : MonoBehaviour
{
    Sequence _titleSeq;
    Sequence _seq;
    Transform _hexagon;
    Transform _title;

    AudioSource _titleBGM;

    private void Awake()
    {
        _hexagon = transform.Find("Hexagon");
        _title = _hexagon.Find("Title");
        _titleBGM = transform.Find("BGM").GetComponent<AudioSource>();
        Screen.SetResolution(1920, 1080, true);
        
    }
    private void Start()
    {
        _titleSeq = DOTween.Sequence();
        _titleSeq.Append(_hexagon.DOScale(1, 1f)).SetEase(Ease.OutQuad);
        _titleSeq.Join(_hexagon.DORotate(new Vector3(0, 0, 180), 1f).SetEase(Ease.OutQuad));
        _titleSeq.AppendInterval(1f);
        _titleSeq.Append(_hexagon.DOMoveX(-3f, 0.7f).SetEase(Ease.OutQuad));
        _titleSeq.AppendInterval(1f);
        _titleSeq.Join(_hexagon.DORotate(Vector3.zero, 0f));
        _titleSeq.Append(_title.DOMoveX(3f, 0.7f).SetEase(Ease.OutQuad));
        _titleSeq.AppendCallback(WhiteFlash);
    }

    private void Update()
    {
        if (Input.anyKeyDown && _titleSeq != null && _titleSeq.IsActive())
        {
            _titleSeq.Complete();
            WhiteFlash();
        }
        else if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
        if (_titleBGM.isPlaying)
        {
            float db = GetAveragedVolume();
            float value = db * 60 / 3f;
            if (!(value > 1)) return;
            if (value > 1.25f) value = 1.25f;
            _seq.Kill();
            _seq = DOTween.Sequence();
            _seq.Append(_hexagon.DOScale(value, 0.05f));
            _seq.Join(_title.DOScale(value, 0.05f));
            _seq.Append(_hexagon.DOScale(1, 0.2f));
            _seq.Join(_title.DOScale(1, 0.2f));
        }
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _titleBGM.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }



    private void WhiteFlash()
    {
        _seq.Complete();
        _titleBGM.Play();
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
    }
}
