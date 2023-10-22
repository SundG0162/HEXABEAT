using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEditor.Rendering;
using UnityEditor.Experimental.GraphView;
using System;

public abstract class NoteObject : MonoBehaviour
{
    public bool life = false;

    public Note note = new Note();

    public float speed = NoteGenerate.Instance.speed;

    public float reachTime = 0;

    public abstract void Move(); //��Ʈ�� �����̰� ���ִ� �Լ�
    public abstract IEnumerator IEMove();
    //�ӵ� ��ȯ�� ���� ��Ʈ ������ �� �������°͵� �ؾ���...
    //�ΰ��ӿ��� �ǽð����� ���ִ°� �ƴ��̻� ����� ��
    // ++ ��Ʈ�� �����Ǵ°� speed�� ����ؼ� ���ִµ��� �ȵ�. ������ ��
    // ù���� ��Ʈ�� 0�̶� �׷���.
    // 
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
        float reachTime = 18.01f / speed;
        float errorTime = 1.75f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);

        transform.localPosition += new Vector3(0, distance);

        bool active = false;
        float time = 0;
        while (true)
        {
            if (((Vector2)transform.position).magnitude / speed < reachTime && !active)
            {
                DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), reachTime).SetEase(Ease.Linear);
                active = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if (spriteRenderer.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                PoolManager.Release(gameObject);
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
        float reachTime = 18.01f / speed;
        float errorTime = 1.75f / speed;
        float differentTime = note.reachTime - reachTime * 1000;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        spriteRenderer.color = Color.green;

        transform.localPosition += new Vector3(0, distance);
        bool active = false;
        while (true)
        {
            if (((Vector2)transform.position).magnitude / speed < reachTime && !active)
            {
                DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), reachTime).SetEase(Ease.Linear);
                active = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if (spriteRenderer.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                PoolManager.Release(gameObject);
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

    // 100 : 57
    public override IEnumerator IEMove()
    {
        var headSprite = head.GetComponent<SpriteRenderer>();
        var tailSprite = tail.GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float errorTime = 1.75f / speed;
        note.reachTime = Judgement.Instance.currentTime + (int)(reachTime * 1000) - (int)(errorTime * 1000);
        note.tail = note.reachTime + 500;
        int differenceTime = note.tail - note.reachTime;
        float distance = differenceTime * speed * 0.001f;
        tail.transform.localPosition += new Vector3(distance * -0.57f, distance * 1);
        bool headActive = false;
        bool tailActive = false;
        print(tail.transform.position);
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
            if (((Vector2)head.transform.position).magnitude / speed < reachTime && !headActive && head != null)
            {
                DOTween.To(() => headSprite.size, value => headSprite.size = value, new Vector2(0, 1), reachTime).SetEase(Ease.Linear);
                headActive = true;
            }
            if (((Vector2)tail.transform.position).magnitude / speed < reachTime && !tailActive)
            {
                DOTween.To(() => tailSprite.size, value => tailSprite.size = value, new Vector2(0, 1), reachTime).SetEase(Ease.Linear);
                tailActive = true;
            }
            if (tailSprite.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                PoolManager.Release(gameObject);
            }
            yield return null;
        }
    }
}