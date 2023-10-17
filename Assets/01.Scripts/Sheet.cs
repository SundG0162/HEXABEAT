using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//9, 15.6
//18.009997
public enum NoteType
{
    s, // ª�� ��Ʈ
    l, // �� ��Ʈ
    c // ª�� ��Ʈ�� �̷���� �ִ� ���ӵ� ��Ʈ�� (�ճ�Ʈ�� �����)
}

//arriveTime ���ϴ¹�
//�Ÿ� = �ð� x �ӵ�, �� �ð� = �Ÿ� / �ӵ�
//��Ʈ�� �����ǰ� ���������� ���� �Ÿ��� ��� ����
//�ӵ��� ���� �������� �ƴϸ� ��� ����
//arriveTime = distance / speed;
//�ٵ� �̰� �������ִ� Ÿ�̹��� ���� ����? (��¥��)
public struct Note
{
    public int lineIndex; // ��Ʈ�� �����Ǵ� �ε���
    public int arriveTime; // ��Ʈ�� �������� �����ϴ� �ð� (ms)
    public NoteType noteType; // ��Ʈ�� Ÿ��
    public int tail; // �ճ�Ʈ�� ���ӽð�

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
    public int[] signature; // ���� ex) 3/4, 4/4, 6/8

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