using System;
using System.Collections;using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteGenerate : MonoSingleton<NoteGenerate>
{

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
        for(int i = 0; i < sheet.notes.Count; i++) 
        {
            switch (sheet.notes[i].noteType)
            {
                case NoteType.Short:
                    D_SHORTNOTEGENERATE(sheet.notes[i].lineIndex, sheet.notes[i].reachTime);
                    break;
                case NoteType.Countinous:
                    D_CONTINUENOTEGENERATE(sheet.notes[i].lineIndex, sheet.notes[i].reachTime);
                    break;
                case NoteType.Long:
                    break;
            }
        }
    }

    private void D_SHORTNOTEGENERATE(int index, int reachTime)
    {
        NoteObject note = PoolManager.Get(notePrefab, noteSpawnPos[index]).AddComponent<NoteShort>();
        note.transform.localPosition = Vector2.zero;
        note.transform.localRotation = Quaternion.identity;
        note.note.lineIndex = index;
        note.note.reachTime = reachTime;
        note.note.noteType = NoteType.Short;
        note.Move();
        NoteManager.Instance.AddDictionary(note.note, note.gameObject);
        NoteManager.Instance.EnqueueNote(index, note.note);
    }

    private void D_CONTINUENOTEGENERATE(int index, int reachTime)
    {
        NoteObject note = PoolManager.Get(notePrefab, noteSpawnPos[index]).AddComponent<NoteContinuous>();
        note.transform.localPosition = Vector2.zero;
        note.transform.localRotation = Quaternion.identity;
        note.note.lineIndex = index;
        note.note.reachTime = reachTime;
        note.note.noteType = NoteType.Countinous;
        note.Move();
        NoteManager.Instance.AddDictionary(note.note, note.gameObject);
        NoteManager.Instance.EnqueueNote(index, note.note);
    }

    private void D_LONGNOTEGENERATE(int index)
    {
        GameObject note = new GameObject("NoteLong");
        note.transform.position = noteSpawnPos[index].position;

        GameObject head = PoolManager.Get(notePrefab, noteSpawnPos[index]);
        head.name = "head";
        head.transform.parent = note.transform;

        GameObject tail = PoolManager.Get(notePrefab, noteSpawnPos[index]);
        tail.transform.parent = note.transform;
        tail.name = "tail";

        GameObject line = new GameObject("line");
        line.transform.parent = note.transform;
        line.transform.position = noteSpawnPos[index].position;

        line.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.sortingOrder = 3;
        lineRenderer.widthMultiplier = 0.8f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false;
        note.AddComponent<NoteLong>().Move();
        note.GetComponent<NoteLong>().note.noteType = NoteType.Long;
        NoteManager.Instance.AddDictionary(note.GetComponent<NoteLong>().note, note);
        NoteManager.Instance.EnqueueNote(index, note.GetComponent<NoteLong>().note);
    }
}
