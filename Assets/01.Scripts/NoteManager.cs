using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoSingleton<NoteManager>
{
    public Queue<Note>[] notes = new Queue<Note>[6];




    public void DequeueNote(int trackNum)
    {
        notes[trackNum].Dequeue();
    }

    public void EnqueueNote(int trackNum, Note note)
    {
        notes[trackNum].Enqueue(note);
    }
}
