using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//9, 15.6
//18.009997
public enum NoteType
{
    Short, // ª�� ��Ʈ
    Countinous, // ª�� ��Ʈ�� �̷���� �ִ� ���ӵ� ��Ʈ�� (�ճ�Ʈ�� �����)
    Long // �� ��Ʈ
}

public struct Note
{
    public int lineIndex; // ��Ʈ�� �����Ǵ� �ε���
    public int reachTime; // ��Ʈ�� �������� �����ϴ� �ð� (ms)
    public NoteType noteType; // ��Ʈ�� Ÿ��
    public int tail; // �ճ�Ʈ�� ���ӽð�
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
    public int[] signature; // ���� ex) 3/4, 4/4, 6/8

    public List<Note> notes = new List<Note>();

    public AudioClip audio;
    public Sprite image;
}
