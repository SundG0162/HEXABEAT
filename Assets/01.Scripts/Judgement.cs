using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Judgement : MonoSingleton<Judgement>
{
    public int currentTime = 0;

    private void Start()
    {
        //StartCoroutine(Timer());
    }

    public void Judge(int trackNum)
    {
        if (NoteManager.Instance.notes[trackNum].Count <= 0) return;
        var note = NoteManager.Instance.notes[trackNum].Peek();
        int judgeTime;
        judgeTime = Mathf.Abs(note.arriveTime - currentTime);
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
        GameObject particle = Instantiate(NoteManager.Instance.particle, NoteManager.Instance.noteDictionary[note].transform.position, Quaternion.identity);
        Destroy(particle, 2f);
        NoteManager.Instance.DequeueNote(trackNum);
        Destroy(NoteManager.Instance.noteDictionary[note]);
        NoteManager.Instance.RemoveDictionary(note);
    }

    public void CheckContinousNote(int trackNum)
    {
        if (NoteManager.Instance.notes[trackNum].Count <= 0|| NoteManager.Instance.notes[trackNum].Peek().noteType != NoteType.Countinous) return;
        var note = NoteManager.Instance.notes[trackNum].Peek();
        int judgeTime;
        judgeTime = Mathf.Abs(note.arriveTime - currentTime);
        if (judgeTime > 1000) return;
        if (judgeTime > 500)
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
        GameObject particle = Instantiate(NoteManager.Instance.particle, NoteManager.Instance.noteDictionary[note].transform.position, Quaternion.identity);
        Destroy(particle, 2f);
        NoteManager.Instance.DequeueNote(trackNum);
        Destroy(NoteManager.Instance.noteDictionary[note]);
        NoteManager.Instance.RemoveDictionary(note);
    }
    private float prevDelta;

    private void Update()
    {
        currentTime += Mathf.FloorToInt(Time.deltaTime * 1000 + prevDelta);
        prevDelta = (Time.deltaTime * 1000 + prevDelta) % 1;
    }
}
