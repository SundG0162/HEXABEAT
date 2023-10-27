using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class T_UIManager : MonoBehaviour
{
    TMP_InputField _inputFieldTrm;

    private void Awake()
    {
        _inputFieldTrm = transform.Find("Input Field").GetComponent<TMP_InputField>();
    }

    public void EndEdit(string speed)
    {
        string[] str = _inputFieldTrm.text.Split();
        _inputFieldTrm.gameObject.SetActive(false);
        NoteGenerate.Instance.speed = float.Parse(str[0]);
        AudioManager.Instance.offset = int.Parse(str[1]);
        GameManager.Instance.GameStart();
    }
}
