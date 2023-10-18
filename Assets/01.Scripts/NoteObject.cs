using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NoteObject : MonoBehaviour
{
    public bool life = false;

    public Note note = new Note();

    public float speed = 5f;

    public abstract void Move(); //노트가 움직이게 해주는 함수
    public abstract IEnumerator IEMove();
    //속도 변환에 따라 노트 나오는 거 느려지는것도 해야함...
    //인게임에서 실시간으로 해주는거 아닌이상 쉬울듯도 함
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