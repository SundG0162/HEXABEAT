using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoSingleton<NoteManager>
{
    public List<Queue<Note>> notes = new List<Queue<Note>>();
    [SerializeField]
    Queue<Note> note1 = new Queue<Note>();
    [SerializeField]
    Queue<Note> note2 = new Queue<Note>();
    [SerializeField]
    Queue<Note> note3 = new Queue<Note>();
    [SerializeField]
    Queue<Note> note4 = new Queue<Note>();
    [SerializeField]
    Queue<Note> note5 = new Queue<Note>();
    [SerializeField]
    Queue<Note> note6 = new Queue<Note>();
    public GameObject particle;

    public Dictionary<Note, GameObject> noteDictionary = new Dictionary<Note, GameObject>();

    private void Awake()
    {
        notes.Add(note1);
        notes.Add(note2);
        notes.Add(note3);
        notes.Add(note4);
        notes.Add(note5);
        notes.Add(note6);
    }

    public void DequeueNote(int trackNum)
    {
        if (notes[trackNum].Count <= 0) return;
        notes[trackNum].Dequeue();
    }

    public void EnqueueNote(int trackNum, Note note)
    {
        notes[trackNum].Enqueue(note);
    }

    public void AddDictionary(Note note, GameObject obj)
    {
        noteDictionary.Add(note, obj);
    }

    public void RemoveDictionary(Note note)
    {
        noteDictionary.Remove(note);
    }
}
