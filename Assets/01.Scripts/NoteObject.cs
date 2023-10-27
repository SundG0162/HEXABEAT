using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

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
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), (18.01f / speed) + (note.reachTime * 0.001f)).SetEase(Ease.Linear);


        transform.localPosition += new Vector3(0, distance);

        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if (spriteRenderer.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                ComboManager.Instance.JudgeText("Miss");
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
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), (18.01f / speed) + (note.reachTime * 0.001f)).SetEase(Ease.Linear);

        transform.localPosition += new Vector3(0, distance);

        spriteRenderer.color = Color.green;
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if (spriteRenderer.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                ComboManager.Instance.JudgeText("Miss");
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}

public class NoteLongHead : NoteObject
{

    public override void Move()
    {
        StartCoroutine(IEMove());
    }

    public override IEnumerator IEMove()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), (18.01f / speed) + (note.reachTime * 0.001f)).SetEase(Ease.Linear);

        transform.localPosition += new Vector3(0, distance);

        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if (spriteRenderer.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                ComboManager.Instance.JudgeText("Miss");
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}

public class NoteLongTail : NoteObject
{
    public override void Move()
    {
        StartCoroutine(IEMove());
        StartCoroutine(Line());
    }

    public override IEnumerator IEMove()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);

        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), (18.01f / speed) + (note.reachTime * 0.001f)).SetEase(Ease.Linear);

        transform.localPosition += new Vector3(0, distance);
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
            if (spriteRenderer.size.x <= 0)
            {
                NoteManager.Instance.DequeueNote(note.lineIndex);
                NoteManager.Instance.RemoveDictionary(note);
                ComboManager.Instance.JudgeText("Miss");
                Destroy(gameObject);
            }
            yield return null;
        }
    }

    IEnumerator Line()
    {
        print(1);
        yield return new WaitForSeconds(0.1f);
        LineRenderer line = transform.parent.AddComponent<LineRenderer>();
        line.material = NoteGenerate.Instance.lineMat;
        while (true)
        {
            line.SetPosition(0, line.transform.GetChild(0).position);
            line.startWidth = line.transform.GetChild(0).GetComponent<SpriteRenderer>().size.x * 1.8f;
            if (line.transform.childCount != 1)
            {
                line.SetPosition(1, line.transform.GetChild(1).position);
                line.endWidth = line.transform.GetChild(1).GetComponent<SpriteRenderer>().size.x * 1.8f;
            }
            else
            {
                line.SetPosition(1, Vector2.zero);
                line.endWidth = 0;
            }
            yield return null;
        }
    }
}