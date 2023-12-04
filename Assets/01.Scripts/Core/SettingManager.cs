using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingManager : MonoSingleton<SettingManager>
{
    [SerializeField]
    Transform[] _canvases;

    [SerializeField]
    Transform _keySettingPanelTrm;

    List<RectTransform> _rects = new List<RectTransform>();

    [SerializeField]
    TMP_InputField _offsetInput;

    [SerializeField]
    TMP_InputField _speedInput;

    public AudioMixer audioMixer;

    Slider _masterVolumeSlider;
    Slider _bgmVolumeSlider;
    Slider _sfxVolumeSlider;

    [SerializeField]
    GameObject[] _cantEdit;


    float _speed = 8;
    float _Speed
    {
        get => _speed; set { _speed = Mathf.Clamp((float)Math.Round(value, 1), 1, 100); }
    }
    int _offset = 0;
    public bool isCoroutineRunning = false;
    public bool isSettingOn = true;

    int _currentSettingIndex = 1;

    public KeyCode currentKey;
    public KeyCode oppositeKey;

    private void Awake()
    {
        for (int i = 1; i < _canvases.Length; i++)
        {
            _rects.Add(_canvases[i].GetChild(0).GetComponent<RectTransform>());
        }
        Transform volumeSettingPanel = _rects[0].transform.Find("SettingPanel");
        _masterVolumeSlider = volumeSettingPanel.Find("Master").GetComponent<Slider>();
        _bgmVolumeSlider = volumeSettingPanel.Find("BGM").GetComponent<Slider>();
        _sfxVolumeSlider = volumeSettingPanel.Find("SFX").GetComponent<Slider>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("IsFirst") == 0)
        {
            SettingReset();
        }
        SettingClose();
        InitSetting();
    }

    private void Update()
    {
        if (isCoroutineRunning) return;
        if(isSettingOn) 
            if(Input.GetKeyDown(KeyCode.Escape))
                SettingClose();
    }

    private void InitSetting()
    {
        _Speed = PlayerPrefs.GetFloat("Speed");
        _offset = PlayerPrefs.GetInt("Offset");
        currentKey = (KeyCode)PlayerPrefs.GetInt("CurrentKey");
        KeyCode k = (KeyCode)currentKey;
        KeyCode k2 = (KeyCode)oppositeKey;
        _keySettingPanelTrm.transform.Find("Current/Text (TMP)").GetComponent<TextMeshProUGUI>().text = k.ToString().ToUpper();
        _keySettingPanelTrm.transform.Find("Opposite/Text (TMP)").GetComponent<TextMeshProUGUI>().text = k2.ToString().ToUpper();
        oppositeKey = (KeyCode)PlayerPrefs.GetInt("OppositeKey");
        _masterVolumeSlider.value = PlayerPrefs.GetFloat("Master");
        audioMixer.SetFloat("Master", _masterVolumeSlider.value);
        _masterVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        audioMixer.SetFloat("BGM", _bgmVolumeSlider.value);
        _masterVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        audioMixer.SetFloat("SFX", _sfxVolumeSlider.value);
        SpeedChange();
        OffsetChange();
    }

    public void SettingReset()
    {
        currentKey = KeyCode.Mouse0;
        oppositeKey = KeyCode.Mouse1;
        _offset = 0;
        _Speed = 8f;
        SpeedChange();
        OffsetChange();
        _keySettingPanelTrm.Find("Current").GetChild(0).GetComponent<TextMeshProUGUI>().text = "MOUSE0";
        _keySettingPanelTrm.Find("Opposite").GetChild(0).GetComponent<TextMeshProUGUI>().text = "MOUSE1";
        _masterVolumeSlider.value = 1;
        audioMixer.SetFloat("Master", _masterVolumeSlider.value);
        _masterVolumeSlider.value = 1;
        audioMixer.SetFloat("BGM", _bgmVolumeSlider.value);
        _masterVolumeSlider.value = 1;
        audioMixer.SetFloat("SFX", _sfxVolumeSlider.value);
        PlayerPrefs.SetFloat("Speed", _Speed);
        PlayerPrefs.SetInt("Offset", _offset);
        PlayerPrefs.SetInt("currentKey", (int)currentKey);
        PlayerPrefs.SetInt("oppositeKey", (int)oppositeKey);
        PlayerPrefs.SetFloat("MasterVolume", 1);
        PlayerPrefs.SetFloat("BGMVolume", 1);
        PlayerPrefs.SetFloat("SFXVolume", 1);
        PlayerPrefs.SetInt("IsFirst", 1);
    }

    public void MasterControl()
    {
        float sound = _masterVolumeSlider.value;
        if (sound <= -50) sound = -80;
        audioMixer.SetFloat("Master", sound);
        PlayerPrefs.SetFloat("MasterVolume", sound);
    }

    public void BGMControl()
    {
        float sound = _bgmVolumeSlider.value;
        if (sound <= -50) sound = -80;
        audioMixer.SetFloat("BGM", sound);
        PlayerPrefs.SetFloat("BGMVolume", sound);
    }

    public void SFXControl()
    {
        float sound = _sfxVolumeSlider.value;
        if (sound <= -50) sound = -80;
        audioMixer.SetFloat("SFX", sound);
        PlayerPrefs.SetFloat("SFXVolume", sound);
    }

    #region Change Setting
    public void SpeedChange(float value = 0)
    {
        _Speed += value;
        _speedInput.text = _Speed.ToString();
        PlayerPrefs.SetFloat("Speed", _Speed);
    }

    public void OffsetChange(int value = 0)
    {
        _offset += value;
        _offsetInput.text = _offset.ToString();
        PlayerPrefs.SetInt("Offset", _offset);
    }

    public void SpeedInput()
    {
        float prevSpeed = _Speed;
        try
        {
            _Speed = float.Parse(_speedInput.text);
            PlayerPrefs.SetFloat("Speed", _Speed);
        }
        catch (Exception e) 
        {
            _Speed = prevSpeed;
            SpeedChange();
        }
    }

    public void OffsetInput()
    {
        int prevOffset = _offset;
        try
        {
            _offset = int.Parse(_offsetInput.text);
            PlayerPrefs.SetInt("Offset", _offset);
        }
        catch (Exception e)
        {
            _offset = prevOffset;
            SpeedChange();
        }
        PlayerPrefs.SetInt("Offset", _offset);
    }
    #endregion

    #region Key Setting
    public void KeyChange(Transform trm)
    {
        trm.GetChild(0).GetComponent<TextMeshProUGUI>().text = "listening...";
        StartCoroutine(ListenKey(trm));
    }

    IEnumerator ListenKey(Transform trm, bool reset = false)
    {
        yield return new WaitUntil(() => Input.anyKey);
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(k))
                {
                    trm.GetChild(0).GetComponent<TextMeshProUGUI>().text = k.ToString().ToUpper();
                    if (trm.name == "Current")
                    {
                        currentKey = k;
                        PlayerPrefs.SetInt("CurrentKey", (int)k);
                    }
                    else
                    {
                        oppositeKey = k;
                        PlayerPrefs.SetInt("OppositeKey", (int)k);
                    }
                    yield break;
                }
            }
        }
        trm.GetChild(0).GetComponent<TextMeshProUGUI>().text = Input.inputString.ToUpper();
        KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), Input.inputString.ToUpper());
        if (trm.name == "Current")
        {
            currentKey = key;
            PlayerPrefs.SetInt("CurrentKey", (int)key);
        }
        else
        {
            oppositeKey = key;
            PlayerPrefs.SetInt("OppositeKey", (int)key);
        }
    }
    #endregion

    #region Panel
    public void PanelChange(int index)
    {
        _canvases[_currentSettingIndex].GetComponent<Canvas>().sortingOrder = 2;
        _canvases[index].GetComponent<Canvas>().sortingOrder = 5;
        _currentSettingIndex = index;
    }

    public void SettingOpen()
    {
        StartCoroutine(IESettingOpen());
    }

    IEnumerator IESettingOpen()
    {
        isSettingOn = true;
        isCoroutineRunning = true;
        _canvases[0].gameObject.SetActive(true);
        RectTransform rect = _canvases[0].GetChild(0).GetComponent<RectTransform>();
        rect.localScale = Vector2.zero;
        rect.DOScale(new Vector2(1,0), 0.3f).SetUpdate(true);
        foreach (GameObject g in _cantEdit)
        {
            g.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            foreach (GameObject g in _cantEdit)
            {
                g.SetActive(true);
            }
        }
        yield return new WaitForSeconds(0.2f);
        rect.DOScale(Vector2.one, 0.3f).SetUpdate(true);
        yield return new WaitForSeconds(0.2f);
        float delay = 0.05f;
        for(int i = 0; i < 5; i++)
        {
            rect.gameObject.SetActive(false);
            yield return new WaitForSeconds(delay);
            rect.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            delay -= 0.009f;
        }
        yield return new WaitForSeconds(0.08f * 5);
        foreach(Transform c in _canvases)
        {
            c.gameObject.SetActive(true);
        }
        for(int i = 1; i < _canvases.Length; i++)
        {
            _rects.Add(_canvases[i].GetChild(0).GetComponent<RectTransform>());
        }
        foreach (RectTransform r in _rects)
        {
            r.localScale = Vector2.zero;
            r.DOScale(Vector2.one, 0.1f).SetUpdate(true);
        }
        isCoroutineRunning = false;
    }

    public void SettingClose()
    {
        StartCoroutine(IESettingClose());
    }

    IEnumerator IESettingClose()
    {
        isSettingOn = false;
        isCoroutineRunning = true;
        
        for (int i = 1; i < _canvases.Length; i++)
        {
            _rects.Add(_canvases[i].GetChild(0).GetComponent<RectTransform>());
        }
        foreach (RectTransform r in _rects)
        {
            r.DOScale(Vector2.zero, 0.1f).SetUpdate(true);
        }
        RectTransform rect = _canvases[0].GetChild(0).GetComponent<RectTransform>();
        rect.DOScale(Vector2.zero, 0.3f).SetUpdate(true);
        yield return new WaitForSeconds(0.3f);
        foreach (Transform c in _canvases)
        {
            c.gameObject.SetActive(false);
        }
        isCoroutineRunning = false;
    }
    #endregion
}
