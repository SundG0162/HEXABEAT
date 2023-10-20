using System.Collections;using System.Collections.Generic;
using UnityEngine;

public class NoteGenerate : MonoSingleton<NoteGenerate>
{

    public GameObject notePrefab;

    public Transform[] noteSpawnPos;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) D_SHORTNOTEGENERATE(0);
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
    }

    private void D_SHORTNOTEGENERATE(int index)
    {
        NoteObject note = Instantiate(notePrefab, noteSpawnPos[index]).AddComponent<NoteShort>();
        note.transform.localPosition = Vector2.zero;
        note.transform.localRotation = Quaternion.identity;
        note.Move();
        note.note.lineIndex = index;
        note.note.noteType = NoteType.Short;
        NoteManager.Instance.AddDictionary(note.note, note.gameObject);
        NoteManager.Instance.EnqueueNote(index, note.note);
    }

    private void D_CONTINUENOTEGENERATE(int index)
    {
        NoteObject note = Instantiate(notePrefab, noteSpawnPos[index]).AddComponent<NoteContinuous>();
        note.transform.localPosition = Vector2.zero;
        note.transform.localRotation = Quaternion.identity;
        note.Move();
        note.note.lineIndex = index;
        note.note.noteType = NoteType.Countinous;
        NoteManager.Instance.AddDictionary(note.note, note.gameObject);
        NoteManager.Instance.EnqueueNote(index, note.note);
    }
}
