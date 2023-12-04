using DG.Tweening;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    GameObject _up;
    GameObject _down;
    GameObject _press;
    Transform _judgeBar;
    [SerializeField]
    Image[] _bars;
    Image _difficultyImage;

    [SerializeField]
    Color[] _difficultyColor;

    private void Awake()
    {
        _selectWindow = transform.Find("SelectWindow");
        _rotatingLevel = _selectWindow.Find("RotatingLevels").GetComponent<RectTransform>();


        _name = _selectWindow.Find("Name").GetComponent<TextMeshProUGUI>();
        _artist = _selectWindow.Find("Artist").GetComponent<TextMeshProUGUI>();
        _score = _selectWindow.Find("Score").GetComponent<TextMeshProUGUI>();
        _combo = _selectWindow.Find("Combo").GetComponent<TextMeshProUGUI>();
        _difficultyImage = transform.Find("Difficulty").GetComponent<Image>();
        _up = _selectWindow.Find("Up").gameObject;
        _down = _selectWindow.Find("Down").gameObject;
        _press = _selectWindow.Find("Press").gameObject;
        _judgeBar = _selectWindow.Find("JudgeBar");

        for(int i = 0; i < levels.Length; i++) 
        {
            string name = levels[i].name;
            TextAsset sheet = Resources.Load<TextAsset>(name);
            if(sheet != null)
            {
                File.WriteAllText(Path.Combine(Application.persistentDataPath, name + ".txt"), sheet.text);
            }
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _levelIndex = PlayerPrefs.GetInt("LevelIndex");
        _rotateIndex++;
        LevelChange(60);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            _levelIndex++;
            _rotateIndex++;
            LevelChange(60);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            _levelIndex--;
            _rotateIndex--;
            LevelChange(-60);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ESCPanel.Instance._isPanelOn) return;
            LevelManager.Instance.levelSO = levels[_levelIndex];
            AudioManager.Instance.audioSource.Stop();
            AudioManager.Instance.audioSource.clip = LevelManager.Instance.levelSO.bgm;
            SceneManager.LoadScene("SampleScene");
        }

    }
    private void Fade()
    {
        bga[_rotateIndex].DOFade(0, 0.2f);
        AudioManager.Instance.FadeOutMusic();
    }

    private void LevelChange(int angle)
    {
        PlayerPrefs.SetInt("LevelIndex", _levelIndex);
        _currentAngle += angle;
        if (_levelIndex < 0)
        {
            _levelIndex = levels.Length - 1;
        }
        else if (_levelIndex >= levels.Length)
        {
            _levelIndex = 0;
        }
        if (_rotateIndex < 0)
        {
            _rotateIndex = 5;
        }
        else if (_rotateIndex >= 6)
        {
            _rotateIndex = 0;
        }
        Fade();
        if (_rotateSeq != null && _rotateSeq.IsActive())
        {
            _rotateSeq.Kill();
        }
        _rotateSeq = DOTween.Sequence();
        _rotateSeq.AppendCallback(Rotating);
        _rotateSeq.Append(_rotatingLevel.DORotate(new Vector3(0, 0, _currentAngle), 0.5f));
        _rotateSeq.AppendCallback(() =>
        {
            SelectedLevelInit();
            AudioManager.Instance.FadeInMusic();
        });
        _rotateSeq.Append(bga[_rotateIndex].DOFade(1, 0.2f));
    }

    private void Rotating()
    {
        for (int i = 0; i < 6; i++)
        {
            bga[i].color = Color.gray - new Color(0, 0, 0, 1);
        }

        _name.gameObject.SetActive(false);
        _artist.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        _combo.gameObject.SetActive(false);
        _judgeBar.gameObject.SetActive(false);
        _difficultyImage.gameObject.SetActive(false);
        _up.SetActive(false);
        _down.SetActive(false);
        _press.SetActive(false);
    }

    Sequence _fillSeq;
    public void SelectedLevelInit()
    {
        AudioManager.Instance.audioSource.clip = levels[_levelIndex].bgm;
        AudioManager.Instance.audioSource.Play();
        _name.gameObject.SetActive(true);
        _artist.gameObject.SetActive(true);
        _score.gameObject.SetActive(true);
        _combo.gameObject.SetActive(true);
        _judgeBar.gameObject.SetActive(true);
        _difficultyImage.gameObject.SetActive(true);
        _up.SetActive(true);
        _down.SetActive(true);
        _press.SetActive(true);
        
        print(1);
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
        BarCharge();
        switch (levels[_levelIndex].difficulty)
        {
            case Difficulty.Easy:
                _difficultyImage.color = _difficultyColor[0];
                _difficultyImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Easy";
                break;
            case Difficulty.Normal:
                _difficultyImage.color = _difficultyColor[1];
                _difficultyImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Normal";
                break;
            case Difficulty.Hard:
                _difficultyImage.color = _difficultyColor[2];
                _difficultyImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hard";
                break;
            case Difficulty.Insane:
                _difficultyImage.color = _difficultyColor[3];
                _difficultyImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Insane";
                break;
        }
        
        
    }


    private void BarCharge()
    {
        if (levels[_levelIndex].totalNote == 0) return;
        _fillSeq = DOTween.Sequence();
        float amount = 0;
        int i = 0;
        float time = 1.2f;
        foreach (var bar in _bars)
        {
            float fill = (float)levels[_levelIndex].judges[i] / levels[_levelIndex].totalNote;
            amount += fill;
            _fillSeq.Join(DOTween.To(() => bar.fillAmount, v => bar.fillAmount = v, amount, time));
            time += 0.2f;
            i++;
        }
        _rotateSeq.Complete();
    }
}
