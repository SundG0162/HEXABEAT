using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public LevelSO[] levels;

    [SerializeField]
    private int _levelIndex = 0;
    [SerializeField]
    private int _rotateIndex = 0;
    private int _currentAngle = 0;

    Transform _selectWindow;

    RectTransform _rotatingLevel;

    Sequence _rotateSeq;

    public Image[] bga;

    TextMeshProUGUI _name;
    TextMeshProUGUI _artist;
    TextMeshProUGUI _score;
    TextMeshProUGUI _combo;
    Transform _judgeBar;
    [SerializeField]
    Image[] _bars;
    private void Awake()
    {
        _selectWindow = transform.Find("SelectWindow");
        _rotatingLevel = _selectWindow.Find("RotatingLevels").GetComponent<RectTransform>();


        _name = _selectWindow.Find("Name").GetComponent<TextMeshProUGUI>();
        _artist = _selectWindow.Find("Artist").GetComponent<TextMeshProUGUI>();
        _score = _selectWindow.Find("Score").GetComponent<TextMeshProUGUI>();
        _combo = _selectWindow.Find("Combo").GetComponent<TextMeshProUGUI>();
        _judgeBar = _selectWindow.Find("JudgeBar");
    }

    private void Start()
    {
        Fade();
        _levelIndex++;
        _rotateIndex++;
        LevelChange(60);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Fade();
            _levelIndex++;
            _rotateIndex++;
            LevelChange(60);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Fade();
            _levelIndex--;
            _rotateIndex--;
            LevelChange(-60);
        }
    }
    private void Fade()
    {
        bga[_rotateIndex].DOFade(0, 0.2f);
    }

    private void LevelChange(int angle)
    {
        _currentAngle += angle;
        if (_levelIndex < 0)
        {
            _levelIndex = levels.Length - 1;
        }
        else if (_levelIndex >= levels.Length)
        {
            _levelIndex = 0;
        }
        if(_rotateIndex < 0)
        {
            _rotateIndex = 5;
        }
        else if(_rotateIndex >= 6)
        {
            _rotateIndex = 0;
        }

        if (_rotateSeq != null && _rotateSeq.IsActive())
        {
            _rotateSeq.Kill();
        }
        _rotateSeq = DOTween.Sequence();
        _rotateSeq.AppendCallback(Rotating);
        _rotateSeq.Append(_rotatingLevel.DORotate(new Vector3(0, 0, _currentAngle), 0.5f));
        _rotateSeq.AppendCallback(SelectedLevelInit);
        _rotateSeq.Append(bga[_rotateIndex].DOFade(1, 0.2f));
    }

    private void Rotating()
    {
        for (int i = 0; i < 6; i++)
        {
            bga[i].color = Color.gray - new Color(0,0,0,1);
        }
        
        _name.gameObject.SetActive(false);
        _artist.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        _combo.gameObject.SetActive(false);
        _judgeBar.gameObject.SetActive(false);
    }

    Sequence _fillSeq;
    public void SelectedLevelInit()
    {
        
        _name.gameObject.SetActive(true);
        _artist.gameObject.SetActive(true);
        _score.gameObject.SetActive(true);
        _combo.gameObject.SetActive(true);
        _judgeBar.gameObject.SetActive(true);
        
        bga[_rotateIndex].sprite = levels[_levelIndex].bga;
        _name.text = levels[_levelIndex].name;
        _artist.text = levels[_levelIndex].artist;
        _score.text = string.Format("{0:D7}", levels[_levelIndex].score);
        if (levels[_levelIndex].totalNote != 0)
        {
            if (levels[_levelIndex].combo.Trim() == levels[_levelIndex].judges[0].ToString())
            {
                _combo.text = "All\nPerfect!";
            }
            else if (levels[_levelIndex].combo.Trim() == levels[_levelIndex].totalNote.ToString())
            {
                _combo.text = "Full\nCombo!";
            }
            else
            {
                _combo.text = string.Format("Combo\n{0:D4}", levels[_levelIndex].combo);
            }
        }
        else
        {
            _combo.text = string.Format("Combo\n{0:D4}", levels[_levelIndex].combo);
        }
        if (_fillSeq != null && _fillSeq.IsActive())
        {
            _fillSeq.Kill();
        }
        foreach (var item in _bars)
        {
            item.fillAmount = 0;
        }
        _fillSeq = DOTween.Sequence();
        float amount = 0;
        int i = 0;
        foreach(var bar in _bars) 
        {
            float fill = (float)levels[_levelIndex].judges[i] / levels[_levelIndex].totalNote;
            amount += fill;
            _fillSeq.Join(DOTween.To(() => bar.fillAmount, v => bar.fillAmount = v, amount, 1.2f));
            i++;
        }
    }
}
