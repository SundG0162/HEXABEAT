using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using JetBrains.Annotations;

public class ComboManager : MonoSingleton<ComboManager>
{
    TextMeshProUGUI _judgeText;
    Sequence _judgeSeq;
    [SerializeField] Color _perfectColor;
    [SerializeField] Color _greatColor;
    [SerializeField] Color _goodColor;
    [SerializeField] Color _badColor;
    [SerializeField] Color _missColor;

    private int[] _judges = new int[5];
    private int _currentCombo = 0;
    private int _bestCombo = 0;
    private int _score = 0;
    private int _totalNote = 0;

    private void Awake()
    {
        _judgeText = transform.Find("JudgeText").GetComponent<TextMeshProUGUI>();
    }
    public void JudgeText(string text)
    {
        _totalNote++;
        if (_judgeSeq != null && _judgeSeq.IsActive())
        {
            _judgeSeq.Kill();
            _judgeText.color = Color.white;
        }
        switch (text)
        {
            case "Perfect":
                _judgeText.color = _perfectColor;
                _judges[0]++;
                _currentCombo++;
                break;
            case "Great":
                _judgeText.color = _greatColor;
                _judges[1]++;
                _currentCombo++;
                break;
            case "Good":
                _judgeText.color = _goodColor;
                _judges[2]++;
                _currentCombo++;
                break;
            case "Bad":
                _judgeText.color = _badColor;
                _judges[3]++;
                _currentCombo = 0;
                break;
            case "Miss":
                _judgeText.color = _missColor;
                _judges[4]++;
                _currentCombo = 0;
                break;
        }
        if (_bestCombo < _currentCombo)
        {
            _bestCombo = _currentCombo;
        }
        _judgeSeq = DOTween.Sequence();
        var rect = _judgeText.GetComponent<RectTransform>();
        _judgeText.text = text;
        _judgeSeq.Append(rect.DOScale(1.3f, 0.05f));
        _judgeSeq.Append(rect.DOScale(0.8f, 0.3f));
        _judgeSeq.AppendInterval(4f);
        _judgeSeq.Append(_judgeText.DOFade(0, 3f));
    }

    public void SaveSO()
    {
        var so = LevelManager.Instance.levelSO;
        so.judges = _judges;
        so.totalNote = _totalNote;
        if (so.score > _score) so.score = _score;
        if (int.Parse(so.combo) > _bestCombo) so.combo= _bestCombo.ToString();
    }
}
