using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//9, 15.6
//18.009997
public enum NoteType
{
    s, // 짧은 노트
    l, // 긴 노트
    c // 짧은 노트로 이루어져 있는 연속된 노트들 (롱노트와 비슷함)
}

//arriveTime 구하는법
//거리 = 시간 x 속도, 즉 시간 = 거리 / 속도
//노트가 생성되고 판정선까지 가는 거리는 모두 동일
//속도도 게임 뒤집을거 아니면 모두 동일
//arriveTime = distance / speed;
//근데 이거 생성해주는 타이밍은 어케 구함? (진짜모름)
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
