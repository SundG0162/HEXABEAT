using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Sheet sheet;

    private void Start()
    {
        Parser.Instance.ReadInfo();
        sheet = Parser.Instance.sheet;
        NoteGenerate.Instance.Gen(sheet);
    }
}
