using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEditorInternal;

public class ComboManager : MonoSingleton<ComboManager>
{
    TextMeshProUGUI _judgeText;
    Sequence _judgeSeq;
    [SerializeField] Color _perfectColor;
    [SerializeField] Color _greatColor;
    [SerializeField] Color _goodColor;
    [SerializeField] Color _badColor;
    [SerializeField] Color _missColor;

    private void Awake()
    {
        _judgeText = transform.Find("JudgeText").GetComponent<TextMeshProUGUI>();
    }
    public void JudgeText(string text)
    {
        if(_judgeSeq != null && _judgeSeq.IsActive())
        {
            _judgeSeq.Kill();
            _judgeText.color = Color.white;
        }
        switch(text)
        {
            case "Perfect":
                _judgeText.color = _perfectColor;
                break;
            case "Great":
                _judgeText.color = _greatColor;
                break;
            case "Good":
                _judgeText.color = _goodColor;
                break;
            case "Bad":
                _judgeText.color = _badColor;
                break;
            case "Miss":
                _judgeText.color = _missColor;
                break;
        }
        _judgeSeq = DOTween.Sequence();
        var rect = _judgeText.GetComponent<RectTransform>();
        _judgeText.text = text;
        _judgeSeq.Append(rect.DOScale(1.3f, 0.05f));
        _judgeSeq.Append(rect.DOScale(0.8f, 0.3f));
    }
}
