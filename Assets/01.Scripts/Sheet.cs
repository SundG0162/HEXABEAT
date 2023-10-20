using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//9, 15.6
//18.009997
public enum NoteType
{
    Short, // 짧은 노트
    Long, // 긴 노트
    Countinous // 짧은 노트로 이루어져 있는 연속된 노트들 (롱노트와 비슷함)
}

public struct Note
{
    public int lineIndex; // 노트가 생성되는 인덱스
    public int arriveTime; // 노트가 판정선에 도달하는 시간 (ms)
    public NoteType noteType; // 노트의 타입
    public int tail; // 롱노트의 지속시간

    public Note(int lineIndex, int arrivedTime, NoteType noteType, int tail)
    {
        this.lineIndex = lineIndex;
        this.arriveTime = arrivedTime;
        this.noteType = noteType;
        this.tail = tail;
    }
}

public class Sheet
{
    public string title;
    public string artist;

    public int bpm;
    public int offset;
    public int[] signature; // 박자 ex) 3/4, 4/4, 6/8

    public List<Note> notes = new List<Note>();

    public AudioClip audio;
    public Sprite image;

    public float BarPerSec { get; set; }
    public float BeatPerSec { get; set; }

    public int BarPerMilliSec { get; set; }
    public int BeatPerMilliSec { get; set; }

    public void Init()
    {
        BarPerMilliSec = (int)(signature[0] / (bpm/60) * 1000);
        BeatPerMilliSec = BarPerMilliSec / 64;

        BarPerSec = BarPerMilliSec * 0.001f;
        BeatPerSec = BarPerSec / 64f;
    }
}
