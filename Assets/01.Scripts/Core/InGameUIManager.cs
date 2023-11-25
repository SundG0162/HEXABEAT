using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoSingleton<InGameUIManager>
{
    TextMeshProUGUI _comboText;
    TextMeshProUGUI _judgeText;
    TextMeshProUGUI _countText;
    TextMeshProUGUI _scoreText;
    Sequence _judgeSeq;
    Sequence _comboSeq;
    [SerializeField] Color _perfectColor;
    [SerializeField] Color _greatColor;
    [SerializeField] Color _goodColor;
    [SerializeField] Color _badColor;
    [SerializeField] Color _missColor;
    Image _fadeOutPanel;

    Transform _gameStopPanel;

    private int[] _judges = new int[5];
    private int _currentCombo = 0;
    private int _bestCombo = 0;
    private int _score = 0;
    private int _totalNote = 0;
    private int _increaseScoreValue = 0;

    bool _isGameStopped = false;

    private void Awake()
    {
        _judgeText = transform.Find("JudgeText").GetComponent<TextMeshProUGUI>();
        _comboText = transform.Find("ComboText").GetComponent<TextMeshProUGUI>();
        _countText = transform.Find("CountText").GetComponent<TextMeshProUGUI>();
        _scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _fadeOutPanel = transform.Find("Panel").GetComponent<Image>();
        _gameStopPanel = transform.Find("GameStopPanel");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isGameStopped) GameStop();
            else GameStart();
        }
    }

    public void JudgeText(string text)
    {
        if (_judgeSeq != null && _judgeSeq.IsActive())
        {
            _judgeSeq.Kill();
            _judgeText.color = Color.white;
        }
        if (_comboSeq != null && _comboSeq.IsActive())
        {
            _comboSeq.Kill();
            _comboText.color = Color.white;
        }
        switch (text)
        {
            case "Perfect":
                _judgeText.color = _perfectColor;
                _judges[0]++;
                _currentCombo++;
                _score += _increaseScoreValue;
                break;
            case "Great":
                _judgeText.color = _greatColor;
                _judges[1]++;
                _currentCombo++;
                _score += _increaseScoreValue - (_increaseScoreValue / 10 * 9);
                break;
            case "Good":
                _judgeText.color = _goodColor;
                _judges[2]++;
                _currentCombo++;
                _score += _increaseScoreValue - (_increaseScoreValue / 10 * 7);
                break;
            case "Bad":
                _judgeText.color = _badColor;
                _judges[3]++;
                _currentCombo = 0;
                _score += _increaseScoreValue - (_increaseScoreValue / 10 * 3);
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
        _comboSeq = DOTween.Sequence();
        _scoreText.text = string.Format("{0:D7}", _score);
        var rect2 = _comboText.GetComponent<RectTransform>();
        var rect = _judgeText.GetComponent<RectTransform>();
        _judgeText.text = text;
        _judgeSeq.Append(rect.DOScale(1.3f, 0.02f));
        _judgeSeq.Append(rect.DOScale(0.8f, 0.3f));
        _judgeSeq.AppendInterval(4f);
        _judgeSeq.Append(_judgeText.DOFade(0, 3f));
        _comboText.text = _currentCombo.ToString();
        _comboSeq.Append(rect2.DOScale(1.3f, 0.05f));
        _comboSeq.Append(rect2.DOScale(0.8f, 0.3f));
        _comboSeq.AppendInterval(4f);
        _comboSeq.Append(_comboText.DOFade(0, 3f));
    }

    public void Init()
    {
        _totalNote = GameManager.Instance.sheet.notes.Count;
        _increaseScoreValue = 1000000 / _totalNote;
        _score += 1000000 % _increaseScoreValue;
    }

    public void SaveSO()
    {
        var so = LevelManager.Instance.levelSO;
        so.prevJudges = _judges;
        so.totalNote = _totalNote;
        so.prevScore = _score;
        so.prevCombo = _bestCombo;
        if (so.score < so.prevScore)
        {
            so.score = so.prevScore;
            so.judges = _judges;
            so.combo = so.prevCombo.ToString();
        }
    }

    public void GameEnd()
    {
        SaveSO();
        FadeOut();
    }

    public void FadeOut()
    {
        _fadeOutPanel.gameObject.SetActive(true);
        _fadeOutPanel.DOFade(1, 1.5f).OnComplete(() => SceneManager.LoadScene("ResultScene"));
    }


    public void GameStop()
    {
        if (AudioManager.Instance.audioSource.isPlaying)
            AudioManager.Instance.audioSource.Pause();
        _isGameStopped = true;
        Time.timeScale = 0;
        _gameStopPanel.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        if (_isRunning) return;
        StartCoroutine(IEGameStart());
    }

    bool _isRunning = false;
    IEnumerator IEGameStart()
    {
        _isRunning = true;
        _gameStopPanel.gameObject.SetActive(false);
        _countText.gameObject.SetActive(true);
        for (int i = 3; i >= 1; i--)
        {
            _countText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        AudioManager.Instance.audioSource.UnPause();
        _isGameStopped = false;
        _countText.gameObject.SetActive(false);
        Time.timeScale = 1;
        _isRunning = false;
    }
}
