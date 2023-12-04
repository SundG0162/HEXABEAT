using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Sheet sheet;

    public bool isGameStart = false;

    private void Start()
    {
        Parser.Instance.ReadInfo();
        SettingManager.Instance.isCoroutineRunning = true;
        sheet = Parser.Instance.sheet;
        NoteGenerate.Instance.speed = PlayerPrefs.GetFloat("Speed");
        AudioManager.Instance.offset = PlayerPrefs.GetInt("Offset");
        InGameUIManager.Instance.Init();
        StartCoroutine(IEGameStart());
    }


    IEnumerator IEGameStart()
    {
        yield return new WaitForSeconds(2f);
        GameStart();
    }

    public void GameStart()
    {
        isGameStart = true;
        NoteGenerate.Instance.Gen(sheet);
    }

    public void GameEnd()
    {
        InGameUIManager.Instance.GameEnd();
    }
}
