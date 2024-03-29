using System.Collections.Generic;
using UnityEngine;


public enum NoteType
{
    Short, // 짧은 노트
    Countinous, // 짧은 노트로 이루어져 있는 연속된 노트들 (롱노트와 비슷함)
    LongHead, // 긴 노트
    LongTail
}

public struct Note
{
    public int lineIndex; // 노트가 생성되는 인덱스
    public int reachTime; // 노트가 판정선에 도달하는 시간 (ms)
    public NoteType noteType; // 노트의 타입
    public int tail; // 롱노트의 지속시간
    public Note(int lineIndex, int reachTime, NoteType noteType, int tail)
    {
        this.lineIndex = lineIndex;
        this.reachTime = reachTime;
        this.noteType = noteType;
        this.tail = tail;
    }
}

public class Sheet
{
    public string title;
    public string artist;

    public int quaterNoteMs;
    public int firstNoteMs;

    public List<Note> notes = new List<Note>();

    public AudioClip audio;
    public Sprite image;
}
