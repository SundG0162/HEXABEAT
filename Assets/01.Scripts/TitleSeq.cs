using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleSeq : MonoBehaviour
{
    Sequence _titleSeq;
    Transform _hexagon;
    Transform _title;

    AudioSource _titleBGM;

    private void Awake()
    {
        _hexagon = transform.Find("Hexagon");
        _title = _hexagon.Find("Title");
        _titleBGM = transform.Find("BGM").GetComponent<AudioSource>();
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
        else if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void WhiteFlash()
    {
        _titleBGM.Play();
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        StartCoroutine(TitleBeat());
    }

    IEnumerator TitleBeat()
    {
        _titleSeq.Complete();
        _titleSeq = DOTween.Sequence();
        while (true)
        {
            _titleSeq.Append(_hexagon.DOScale(1.3f, 0.05f));
            _titleSeq.Join(_title.DOScale(1.3f, 0.05f));
            _titleSeq.Append(_hexagon.DOScale(1, 0.3f));
            _titleSeq.Join(_title.DOScale(1, 0.3f));
            yield return new WaitForSeconds(0.5505f);
        }
    }

}
