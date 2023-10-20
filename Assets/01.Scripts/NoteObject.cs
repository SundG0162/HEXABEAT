using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public abstract class NoteObject : MonoBehaviour
{
    public bool life = false;

    public Note note = new Note();

    public float speed = 8f;

    public abstract void Move(); //��Ʈ�� �����̰� ���ִ� �Լ�
    public abstract IEnumerator IEMove();
    //�ӵ� ��ȯ�� ���� ��Ʈ ������ �� �������°͵� �ؾ���...
    //�ΰ��ӿ��� �ǽð����� ���ִ°� �ƴ��̻� ����� ��
}

public class NoteShort : NoteObject
{
    public override void Move()
    {
        StartCoroutine(IEMove());
    }

    public override IEnumerator IEMove()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        float arriveTime = ((Vector2)transform.position).magnitude / speed;
        float errorTime = 1.75f / speed;
        note.arriveTime = Judgement.Instance.currentTime + (int)(arriveTime * 1000) - (int)(errorTime * 1000);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), arriveTime).SetEase(Ease.Linear);
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if (spriteRenderer.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}

public class NoteContinuous : NoteObject
{
    public override void Move()
    {
        StartCoroutine(IEMove());
    }

    public override IEnumerator IEMove()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        float arriveTime = ((Vector2)transform.position).magnitude / speed;
        float errorTime = 1.75f / speed;
        note.arriveTime = Judgement.Instance.currentTime + (int)(arriveTime * 1000) - (int)(errorTime * 1000);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), arriveTime).SetEase(Ease.Linear);
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if (spriteRenderer.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}

public class NoteLong : NoteObject
{
    LineRenderer lineRenderer;
    public GameObject head;
    public GameObject tail;
    GameObject line;

    void Awake()
    {
        head = transform.GetChild(0).gameObject;
        tail = transform.GetChild(1).gameObject;
        line = transform.GetChild(2).gameObject;
        lineRenderer = line.GetComponent<LineRenderer>();
    }

    public override void Move()
    {
        StartCoroutine(IEMove());
    }

    public override IEnumerator IEMove()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed);
            yield return null;
        }
    }
}