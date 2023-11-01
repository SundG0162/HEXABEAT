using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerate : MonoSingleton<NoteGenerate>
{
    public Material lineMat;
    public GameObject notePrefab;

    public Transform[] noteSpawnPos;

    public float speed = 13;
    public float reachTime = 0;


    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Alpha1)) D_SHORTNOTEGENERATE(0);
        if(Input.GetKeyDown(KeyCode.Alpha2)) D_SHORTNOTEGENERATE(1);
        if(Input.GetKeyDown(KeyCode.Alpha3)) D_SHORTNOTEGENERATE(2);
        if(Input.GetKeyDown(KeyCode.Alpha4)) D_SHORTNOTEGENERATE(3);
        if(Input.GetKeyDown(KeyCode.Alpha5)) D_SHORTNOTEGENERATE(4);
        if(Input.GetKeyDown(KeyCode.Alpha6)) D_SHORTNOTEGENERATE(5);
        if(Input.GetKeyDown(KeyCode.Q)) D_CONTINUENOTEGENERATE(0);
        if(Input.GetKeyDown(KeyCode.W)) D_CONTINUENOTEGENERATE(1);
        if(Input.GetKeyDown(KeyCode.E)) D_CONTINUENOTEGENERATE(2);
        if(Input.GetKeyDown(KeyCode.R)) D_CONTINUENOTEGENERATE(3);
        if(Input.GetKeyDown(KeyCode.T)) D_CONTINUENOTEGENERATE(4);
        if(Input.GetKeyDown(KeyCode.Y)) D_CONTINUENOTEGENERATE(5);
        if(Input.GetKeyDown(KeyCode.A)) D_LONGNOTEGENERATE(0);
        if(Input.GetKeyDown(KeyCode.S)) D_LONGNOTEGENERATE(1);
        if(Input.GetKeyDown(KeyCode.D)) D_LONGNOTEGENERATE(2);
        if(Input.GetKeyDown(KeyCode.F)) D_LONGNOTEGENERATE(3);
        if(Input.GetKeyDown(KeyCode.G)) D_LONGNOTEGENERATE(4);
        if(Input.GetKeyDown(KeyCode.H)) D_LONGNOTEGENERATE(5);*/
    }

    public void Gen(Sheet sheet)
    {
        StartCoroutine(IEGen(sheet));
    }

    IEnumerator IEGen(Sheet sheet)
    {
        if(AudioManager.Instance.offset >= 0)
            yield return new WaitForSeconds(AudioManager.Instance.offset * 0.001f);
        for (int i = 0; i < sheet.notes.Count; i++)
        {
            switch (sheet.notes[i].noteType)
            {
                case NoteType.Short:
                    ShortNoteGenerate(sheet.notes[i].lineIndex, sheet.notes[i].reachTime);
                    break;
                case NoteType.Countinous:
                    ContinuousNoteGenerate(sheet.notes[i].lineIndex, sheet.notes[i].reachTime);
                    break;
                case NoteType.LongHead:
                    LongNoteGenerate(sheet.notes[i].lineIndex, sheet.notes[i].reachTime, sheet.notes[i].tail);
                    break;
            }
        }
    }

    private void ShortNoteGenerate(int index, int reachTime)
    {
        NoteObject note = Instantiate(notePrefab, noteSpawnPos[index]).AddComponent<NoteShort>();
        note.transform.localPosition = Vector2.zero;
        note.transform.localRotation = Quaternion.identity;
        note.note.lineIndex = index;
        note.note.reachTime = reachTime;
        note.note.noteType = NoteType.Short;
        NoteManager.Instance.AddDictionary(note.note, note.gameObject);
        NoteManager.Instance.EnqueueNote(index, note.note);
    }

    private void ContinuousNoteGenerate(int index, int reachTime)
    {
        NoteObject note = Instantiate(notePrefab, noteSpawnPos[index]).AddComponent<NoteContinuous>();
        note.transform.localPosition = Vector2.zero;
        note.transform.localRotation = Quaternion.identity;
        note.note.lineIndex = index;
        note.note.reachTime = reachTime;
        note.note.noteType = NoteType.Countinous;
        NoteManager.Instance.AddDictionary(note.note, note.gameObject);
        NoteManager.Instance.EnqueueNote(index, note.note);
    }

    private void LongNoteGenerate(int index, int reachTime, int tail)
    {
        GameObject longNote = new GameObject("Note");
        longNote.transform.SetParent(noteSpawnPos[index]);
        longNote.transform.transform.localRotation = Quaternion.identity;
        for (int i = 0; i < 2; i++)
        {
            NoteObject note = null;
            if (i == 0)
            {
                note = Instantiate(notePrefab, noteSpawnPos[index]).AddComponent<NoteLongHead>();
                note.note.reachTime = reachTime;
                note.note.noteType = NoteType.LongHead;
            }
            else
            {
                note = Instantiate(notePrefab, noteSpawnPos[index]).AddComponent<NoteLongTail>();
                note.note.reachTime = tail;
                note.note.noteType = NoteType.LongTail;
            }
            note.transform.localPosition = Vector2.zero;
            note.transform.localRotation = Quaternion.identity;
            note.note.lineIndex = index;
            note.transform.SetParent(longNote.transform);
            NoteManager.Instance.AddDictionary(note.note, note.gameObject);
            NoteManager.Instance.EnqueueNote(index, note.note);
        }
    }
}
