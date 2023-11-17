using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField]
    Transform[] _canvases;

    List<RectTransform> _rects = new List<RectTransform>();

    bool _isCoroutineRunning = false;
    bool _isSettingOn = true;

    int _currentSettingIndex = 1;

    private void Awake()
    {
        for (int i = 1; i < _canvases.Length; i++)
        {
            _rects.Add(_canvases[i].GetChild(0).GetComponent<RectTransform>());
        }
    }

    private void Start()
    {
        SettingClose();
    }

    private void Update()
    {
        if (_isCoroutineRunning) return;
        if (Input.GetKeyDown(KeyCode.K) && !_isSettingOn)
        {
            SettingOpen();
        }
        else if (Input.GetKeyDown(KeyCode.L) && _isSettingOn)
        {
            SettingClose();
        }
    }
    
    public void PanelChange(int index)
    {
        _canvases[_currentSettingIndex].GetComponent<Canvas>().sortingOrder = 0;
        _canvases[index].GetComponent<Canvas>().sortingOrder = 5;
        _currentSettingIndex = index;
    }

    private void SettingOpen()
    {
        StartCoroutine(IESettingOpen());
    }

    IEnumerator IESettingOpen()
    {
        _isSettingOn = true;
        _isCoroutineRunning = true;
        _canvases[0].gameObject.SetActive(true);
        RectTransform rect = _canvases[0].GetChild(0).GetComponent<RectTransform>();
        rect.localScale = Vector2.zero;
        rect.DOScale(new Vector2(1,0), 0.3f);
        yield return new WaitForSeconds(0.2f);
        rect.DOScale(Vector2.one, 0.3f);
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
            r.DOScale(Vector2.one, 0.1f);
        }
        _isCoroutineRunning = false;
    }

    private void SettingClose()
    {
        StartCoroutine(IESettingClose());
    }

    IEnumerator IESettingClose()
    {
        _isSettingOn = false;
        _isCoroutineRunning = true;
        for (int i = 1; i < _canvases.Length; i++)
        {
            _rects.Add(_canvases[i].GetChild(0).GetComponent<RectTransform>());
        }
        foreach (RectTransform r in _rects)
        {
            r.DOScale(Vector2.zero, 0.1f);
        }
        RectTransform rect = _canvases[0].GetChild(0).GetComponent<RectTransform>();
        rect.DOScale(Vector2.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        foreach (Transform c in _canvases)
        {
            c.gameObject.SetActive(false);
        }
        _isCoroutineRunning = false;
    }
}
