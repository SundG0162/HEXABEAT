using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Judgement : MonoSingleton<Judgement>
{
    public int currentTime = 0;
    public bool isSongStart = false;


    private void Start()
    {
        //StartCoroutine(Timer());
    }

    
    public void Judge(int trackNum)
    {
        if (NoteManager.Instance.notes[trackNum].Count <= 0) return;
        var note = NoteManager.Instance.notes[trackNum].Peek();
        if (note.noteType == NoteType.LongTail) return;
        int judgeTime;
        judgeTime = Mathf.Abs(note.reachTime - currentTime + AudioManager.Instance.offset);
        if(judgeTime > 300)
        {
            return;
        }
        else if (judgeTime > 200)
        {
            InGameUIManager.Instance.JudgeText("Miss");
        }
        else if (judgeTime > 150)
        {
            InGameUIManager.Instance.JudgeText("Bad");
        }
        else if (judgeTime > 120)
        {
            InGameUIManager.Instance.JudgeText("Good");
        }
        else if (judgeTime > 70)
        {
            InGameUIManager.Instance.JudgeText("Great");

        }
        else if (judgeTime >= 0)
        {
            InGameUIManager.Instance.JudgeText("Perfect");
        }

        GameObject particle = Instantiate(NoteManager.Instance.particle, NoteManager.Instance.valueList[NoteManager.Instance.keyList.IndexOf(note)].transform.position, Quaternion.identity);
        if (NoteManager.Instance.notes[trackNum].Peek().noteType != NoteType.LongHead || NoteManager.Instance.notes[trackNum].Peek().noteType != NoteType.LongTail)
        {
            NoteManager.Instance.DequeueNote(trackNum);
            Destroy(NoteManager.Instance.valueList[NoteManager.Instance.keyList.IndexOf(note)]);
            NoteManager.Instance.RemoveDictionary(note);
        }
        else
        {
            NoteManager.Instance.DequeueNote(trackNum);
        }
    }

    public void CheckContinousNote(int trackNum)
    {
        if (NoteManager.Instance.notes[trackNum].Count <= 0 || NoteManager.Instance.notes[trackNum].Peek().noteType != NoteType.Countinous) return;
        Note note = NoteManager.Instance.notes[trackNum].Peek();
        int judgeTime;
        judgeTime = Mathf.Abs(note.reachTime - currentTime + AudioManager.Instance.offset);
        if (judgeTime > 150) return;
        if (judgeTime > 120)
        {
            InGameUIManager.Instance.JudgeText("Good");
        }
        else if (judgeTime > 80)
        {
            InGameUIManager.Instance.JudgeText("Great");

        }
        else if (judgeTime >= 0)
        {
            InGameUIManager.Instance.JudgeText("Perfect");
        }
        GameObject particle = Instantiate(NoteManager.Instance.particle, NoteManager.Instance.valueList[NoteManager.Instance.keyList.IndexOf(note)].transform.position, Quaternion.identity);
        NoteManager.Instance.DequeueNote(trackNum);
        Destroy(NoteManager.Instance.valueList[NoteManager.Instance.keyList.IndexOf(note)]);
        NoteManager.Instance.RemoveDictionary(note);
    }

    public void CheckLongNote(int trackNum)
    {
        if (NoteManager.Instance.notes[trackNum].Count <= 0 || NoteManager.Instance.notes[trackNum].Peek().noteType != NoteType.LongTail) return;
        var note = NoteManager.Instance.notes[trackNum].Peek();
        int judgeTime;
        judgeTime = Mathf.Abs(note.reachTime - currentTime + AudioManager.Instance.offset);
        if(judgeTime > 300)
        {
            return;
        }
        else if (judgeTime > 200)
        {
            InGameUIManager.Instance.JudgeText("Miss");
        }
        else if (judgeTime > 150)
        {
            InGameUIManager.Instance.JudgeText("Bad");
        }
        else if (judgeTime > 120)
        {
            InGameUIManager.Instance.JudgeText("Good");
        }
        else if (judgeTime > 70)
        {
            InGameUIManager.Instance.JudgeText("Great");

        }
        else if (judgeTime >= 0)
        {
            InGameUIManager.Instance.JudgeText("Perfect");
        }
        GameObject particle = Instantiate(NoteManager.Instance.particle, NoteManager.Instance.valueList[NoteManager.Instance.keyList.IndexOf(note)].transform.position, Quaternion.identity);
        NoteManager.Instance.DequeueNote(trackNum);
        Destroy(NoteManager.Instance.valueList[NoteManager.Instance.keyList.IndexOf(note)].transform.parent.gameObject);
        NoteManager.Instance.RemoveDictionary(note);
    }
    private float prevDelta;
    [SerializeField]
    private float countTime;
    private void Update()
    {
        if (!GameManager.Instance.isGameStart) return;
        if (!isSongStart)
        {
            countTime += Time.deltaTime;
            if (countTime >= (18.01f * 2 - 1.75f) / NoteGenerate.Instance.speed)
            {
                isSongStart = true;
                AudioManager.Instance.Play();
            }
            return;
        }
        currentTime += Mathf.FloorToInt(Time.deltaTime * 1000 + prevDelta);
        prevDelta = (Time.deltaTime * 1000 + prevDelta) % 1;
    }
}
