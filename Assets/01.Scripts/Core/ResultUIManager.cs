using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class ResultUIManager : MonoBehaviour
{
    Sequence _resultSeq;

    RectTransform _resultRect;
    Transform _infoTrm;
    Transform _specialComboTrm;
    TextMeshProUGUI _title;
    TextMeshProUGUI _artist;
    TextMeshProUGUI _score;
    TextMeshProUGUI _combo;
    TextMeshProUGUI _grade;

    [SerializeField]
    Image[] _bars;

    private void Awake()
    {
        _resultRect = transform.Find("Result").GetComponent<RectTransform>();
        _infoTrm = transform.Find("Info");
        _specialComboTrm = _infoTrm.Find("SpecialCombo");
        _title = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        _artist= transform.Find("Artist").GetComponent<TextMeshProUGUI>();
        _score = _infoTrm.Find("ResultText/Score").GetComponent<TextMeshProUGUI>();
        _combo = _infoTrm.Find("ResultText/Combo").GetComponent<TextMeshProUGUI>();
        _grade = transform.Find("Grade").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        _title.text = LevelManager.Instance.levelSO.name;
        _artist.text = LevelManager.Instance.levelSO.artist;
        _resultSeq = DOTween.Sequence();
        _resultSeq.Append(_resultRect.DOScale(1, 0.5f));
        _resultSeq.Join(_resultRect.DORotate(Vector3.zero, 0.5f));
        _resultSeq.AppendInterval(0.5f);
        _resultSeq.Append(_resultRect.DOAnchorPosX(-470, 0.5f));
        _resultSeq.AppendCallback(() =>
        {
            Fill();
            _infoTrm.gameObject.SetActive(true);
            StartCoroutine(IEScoreCount());
            StartCoroutine(IEComboCount());
            if (LevelManager.Instance.levelSO.prevJudges[0] == LevelManager.Instance.levelSO.totalNote) 
            {
                _specialComboTrm.GetComponent<TextMeshProUGUI>().text = "All Perfect!";
                _specialComboTrm.gameObject.SetActive(true);
            }
            else if (LevelManager.Instance.levelSO.prevCombo == LevelManager.Instance.levelSO.totalNote)
            {
                _specialComboTrm.GetComponent<TextMeshProUGUI>().text = "Full Combo!";
            }

            _specialComboTrm.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
            _grade.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
        });
        _resultSeq.Append(_specialComboTrm.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.InQuart));
        _resultSeq.AppendCallback(() => { 
            _grade.gameObject.SetActive(true);
            if(LevelManager.Instance.levelSO.prevScore <= 200000)
            {
                _grade.text = "E";
            }
            else if (LevelManager.Instance.levelSO.prevScore <= 400000)
            {
                _grade.text = "D";
            }
            else if (LevelManager.Instance.levelSO.prevScore <= 600000)
            {
                _grade.text = "C";
            }
            else if (LevelManager.Instance.levelSO.prevScore <= 800000)
            {
                _grade.text = "B";
            }
            else if (LevelManager.Instance.levelSO.prevScore <= 900000)
            {
                _grade.text = "A";
            }
            else
            {
                _grade.text = "S";
            }
        });
        _resultSeq.Append(_grade.GetComponent<RectTransform>().DOScale(1,1).SetEase(Ease.InQuart));
    }

    private void Update()
    {
        
    }

    private void Complete()
    {
        StopAllCoroutines();
        _currentScore = LevelManager.Instance.levelSO.prevScore;
        _currentCombo = LevelManager.Instance.levelSO.prevCombo;
        _fillSeq.Complete();
    }

    int _currentScore = 0;
    IEnumerator IEScoreCount()
    {
        int score = LevelManager.Instance.levelSO.prevScore;
        int divide = score / 100;
        int remain = score % divide;
        _currentScore = remain;
        while(_currentScore != score)
        {
            _currentScore += divide;
            _score.text = string.Format("{0:D7}", _currentScore);
            yield return new WaitForSeconds(0.02f);
        }
    }

    int _currentCombo = 0;
    IEnumerator IEComboCount()
    {
        int combo = LevelManager.Instance.levelSO.prevCombo;
        print(combo);
        int divide = combo / 100;
        int remain = 0;
        if (divide != 0)
            remain = combo % divide;
        _currentCombo = remain;
        while (_currentCombo != combo)
        {
            _currentCombo += divide;
            _combo.text = string.Format("{0:D4}", _currentCombo);
            yield return new WaitForSeconds(0.02f);
        }
    }

    Sequence _fillSeq;
    private void Fill()
    {
        _fillSeq = DOTween.Sequence();
        float amount = 0;
        int i = 0;
        float time = 1.2f;
        foreach (var bar in _bars)
        {
            float fill = (float)LevelManager.Instance.levelSO.prevJudges[i] / LevelManager.Instance.levelSO.totalNote;
            amount += fill;
            _fillSeq.Join(DOTween.To(() => bar.fillAmount, v => bar.fillAmount = v, amount, time));
            time += 0.2f;
            i++;
        }
    }
}
