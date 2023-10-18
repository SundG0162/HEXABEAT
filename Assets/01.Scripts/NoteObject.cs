using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NoteObject : MonoBehaviour
{
    public bool life = false;

    public Note note = new Note();

    public float speed = 5f;

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
        float arriveTime = (Vector2.zero - (Vector2)transform.position).magnitude / speed;
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if(spriteRenderer.size.x <= 0) Destroy(gameObject);
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