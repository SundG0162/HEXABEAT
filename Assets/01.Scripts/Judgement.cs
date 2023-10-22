using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
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
        int judgeTime;
        judgeTime = Mathf.Abs(note.reachTime - currentTime);
        if (judgeTime > 1000)
        {
            Debug.Log("miss");
        }
        else if(judgeTime > 500)
        {
            Debug.Log("bad");
        }
        else if(judgeTime > 200)
        {
            Debug.Log("good");
        }
        else if(judgeTime > 100)
        {
            Debug.Log("Great");
        }
        else if(judgeTime >= 0)
        {
            Debug.Log("Perfect");
        }
        GameObject particle = PoolManager.Get(NoteManager.Instance.particle, NoteManager.Instance.noteDictionary[note].transform.position, Quaternion.identity);
        if (NoteManager.Instance.notes[trackNum].Peek().noteType != NoteType.Long)
        {
            NoteManager.Instance.DequeueNote(trackNum);
            PoolManager.Release(NoteManager.Instance.noteDictionary[note]);
            NoteManager.Instance.RemoveDictionary(note);
        }
        else
        {
            PoolManager.Release(NoteManager.Instance.noteDictionary[note].transform.GetChild(0).gameObject);
        }

    }

    public void CheckContinousNote(int trackNum)
    {
        if (NoteManager.Instance.notes[trackNum].Count <= 0|| NoteManager.Instance.notes[trackNum].Peek().noteType != NoteType.Countinous) return;
        Note note = NoteManager.Instance.notes[trackNum].Peek();
        int judgeTime;
        judgeTime = Mathf.Abs(note.reachTime - currentTime);
        if (judgeTime > 101) return;
        if (judgeTime > 50)
        {
            Debug.Log("Great");
        }
        else if (judgeTime >= 0)
        {
            Debug.Log("Perfect");
        }
        GameObject particle = PoolManager.Get(NoteManager.Instance.particle, NoteManager.Instance.noteDictionary[note].transform.position, Quaternion.identity);
        NoteManager.Instance.DequeueNote(trackNum);
        PoolManager.Release(NoteManager.Instance.noteDictionary[note]);
        NoteManager.Instance.RemoveDictionary(note);
    }

    public void CheckLongNote(int trackNum)
    {
        if (NoteManager.Instance.notes[trackNum].Count <= 0 || NoteManager.Instance.notes[trackNum].Peek().noteType != NoteType.Long) return;
        var note = NoteManager.Instance.notes[trackNum].Peek();
        int judgeTime;
        judgeTime = Mathf.Abs(note.reachTime - currentTime);
        if (judgeTime > 1000)
        {
            Debug.Log("miss");
        }
        else if (judgeTime > 500)
        {
            Debug.Log("bad");
        }
        else if (judgeTime > 200)
        {
            Debug.Log("good");
        }
        else if (judgeTime > 100)
        {
            Debug.Log("Great");
        }
        else if (judgeTime >= 0)
        {
            Debug.Log("Perfect");
        }
        GameObject particle = PoolManager.Get(NoteManager.Instance.particle, NoteManager.Instance.noteDictionary[note].transform.position, Quaternion.identity);
        NoteManager.Instance.DequeueNote(trackNum);
        PoolManager.Release(NoteManager.Instance.noteDictionary[note]);
        NoteManager.Instance.RemoveDictionary(note);
    }
    private float prevDelta;
    private float countTime;
    private void Update()
    {
        countTime += Time.deltaTime;
        if (countTime >= (18.01f - 1.75f) / NoteGenerate.Instance.speed && !isSongStart)
        {
            isSongStart = true;
            AudioManager.Instance.Play();
        }
        if (!isSongStart) return;
        currentTime += Mathf.FloorToInt(Time.deltaTime * 1000 + prevDelta);
        prevDelta = (Time.deltaTime * 1000 + prevDelta) % 1;
    }
}
