using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    [SerializeField]
    Button[] _colorButtons;

    [SerializeField]
    List<GameObject> _colorPickers = new List<GameObject>();

    int _currentIndex = 0;

    bool _enabled = false;

    private void Awake()
    {
        for(int i = 0;  i < _colorButtons.Length; i++)
        {
            _colorPickers.Add(_colorButtons[i].transform.Find("ColorEditor").gameObject);
        }
    }

    private void Update()
    {
        if(_enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            ColorPickerOff();
        }
    }

    public void ColorPickerOn(int index)
    {
        if (_enabled) return;
        _enabled = true;
        _colorPickers[index].gameObject.SetActive(true);
        _currentIndex = index;
    }

    public void ColorPickerOff()
    {
        foreach(GameObject g in _colorPickers)
        {
            g.SetActive(false);
        }

        ColorPick();
        _enabled = false;
    }

    public void ColorPick()
    {
        _colorButtons[_currentIndex].GetComponent<Image>().color = _colorPickers[_currentIndex].transform.Find("FlexibleColorPicker").GetComponent<FlexibleColorPicker>().color;
    }
}
