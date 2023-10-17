using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoSingleton<Judgement>
{
    float currentTime;

    private void Update()
    {
        currentTime += Time.deltaTime * 1000;
    }

    public void Judge(int trackNum)
    {
        float judgeTime;
        if (NoteManager.Instance.notes[trackNum].Count <= 0) return;
        judgeTime = currentTime - NoteManager.Instance.notes[trackNum].Peek().arriveTime;
    }
}
