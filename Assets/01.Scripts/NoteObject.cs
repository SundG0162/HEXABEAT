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

    public SpriteRenderer spriteRenderer = null;

    public abstract void Start();
    public abstract void Update();
}

public class NoteShort : NoteObject
{

    public override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        if (note.lineIndex.Equals(1) || note.lineIndex.Equals(4))
        {
            spriteRenderer.size = new Vector2((distance * 1.05f + 18.01f) / 18.01f * 11.65f, 1);
        }
        else
            spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), (18.01f / speed) + (note.reachTime * 0.001f)).SetEase(Ease.Linear);


        transform.localPosition += new Vector3(0, distance);
    }

    public override void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
        if (spriteRenderer.size.x <= 0)
        {
            NoteManager.Instance.DequeueNote(note.lineIndex);
            NoteManager.Instance.RemoveDictionary(note);
            ComboManager.Instance.JudgeText("Miss");
            Destroy(gameObject);
        }
    }
}

public class NoteContinuous : NoteObject
{
    public override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        if (note.lineIndex.Equals(1) || note.lineIndex.Equals(4))
        {
            spriteRenderer.size = new Vector2((distance * 1.05f + 18.01f) / 18.01f * 11.65f, 1);
        }
        else
            spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), (18.01f / speed) + (note.reachTime * 0.001f)).SetEase(Ease.Linear);
        spriteRenderer.color = Color.cyan;

        transform.localPosition += new Vector3(0, distance);
    }

    public override void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
        if (spriteRenderer.size.x <= 0)
        {
            NoteManager.Instance.DequeueNote(note.lineIndex);
            NoteManager.Instance.RemoveDictionary(note);
            ComboManager.Instance.JudgeText("Miss");
            Destroy(gameObject);
        }
    }
}

public class NoteLongHead : NoteObject
{

    public override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        if (note.lineIndex.Equals(1) || note.lineIndex.Equals(4))
        {
            spriteRenderer.size = new Vector2((distance * 1.05f + 18.01f) / 18.01f * 11.65f, 1);
        }
        else
            spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), (18.01f / speed) + (note.reachTime * 0.001f)).SetEase(Ease.Linear);


        transform.localPosition += new Vector3(0, distance);
    }

    public override void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
        if (spriteRenderer.size.x <= 0)
        {
            NoteManager.Instance.DequeueNote(note.lineIndex);
            NoteManager.Instance.RemoveDictionary(note);
            ComboManager.Instance.JudgeText("Miss");
            Destroy(gameObject);
        }
    }
}

public class NoteLongTail : NoteObject
{
    LineRenderer line;
    public override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        if (note.lineIndex.Equals(1) || note.lineIndex.Equals(4))
        {
            spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);
        }
        else
            spriteRenderer.size = new Vector2((distance + 18.01f) / 18.01f * 11.65f, 1);
        DOTween.To(() => spriteRenderer.size, value => spriteRenderer.size = value, new Vector2(0, 1), (18.01f / speed) + (note.reachTime * 0.001f)).SetEase(Ease.Linear);


        transform.localPosition += new Vector3(0, distance);


        StartCoroutine(Line());
    }

    public override void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);

        if (spriteRenderer.size.x <= 0)
        {
            NoteManager.Instance.DequeueNote(note.lineIndex);
            NoteManager.Instance.RemoveDictionary(note);
            ComboManager.Instance.JudgeText("Miss");
            Destroy(gameObject);
        }

    }

    IEnumerator Line()
    {
        line = transform.parent.AddComponent<LineRenderer>();
        yield return new WaitForSeconds(0.1f);
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