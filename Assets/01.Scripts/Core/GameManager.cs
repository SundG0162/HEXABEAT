using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public Sheet sheet;

    public bool isGameStart = false;

    private void Start()
    {
        Parser.Instance.ReadInfo();
        sheet = Parser.Instance.sheet;
        //NoteGenerate.Instance.Gen(sheet);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void GameStart()
    {
        isGameStart = true;
        NoteGenerate.Instance.Gen(sheet);
    }
}
