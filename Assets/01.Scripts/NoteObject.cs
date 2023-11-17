using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public abstract class NoteObject : MonoBehaviour
{
    public bool start = false;

    public Note note = new Note();

    public float speed = NoteGenerate.Instance.speed;

    public float reachTime = 0;

    public SpriteRenderer spriteRenderer = null;
}

public interface INote
{
    void Init();
    void Move();
}

public class NoteShort : NoteObject, INote
{
    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);

        spriteRenderer.size = new Vector2(11.65f * 2, 1);


        transform.localPosition += new Vector3(0, distance + 18.01f);
    }

    public void Move()
    {
        if (!NoteGenerate.Instance.isGenerateEnd) return;
        if (Vector2.Distance(transform.position, Vector2.zero) <= 18.01f * 2)
            spriteRenderer.size -= new Vector2(Time.deltaTime * 11.65f / (18.01f / speed), 0);
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
        if (spriteRenderer.size.x <= 0)
        {
            NoteManager.Instance.DequeueNote(note.lineIndex);
            NoteManager.Instance.RemoveDictionary(note);
            ComboManager.Instance.JudgeText("Miss");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Move();
    }
}

public class NoteContinuous : NoteObject, INote
{
    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        spriteRenderer.size = new Vector2(11.65f * 2, 1);


        spriteRenderer.color = Color.cyan;

        transform.localPosition += new Vector3(0, distance + 18.01f);
    }

    public void Move()
    {
        if (!NoteGenerate.Instance.isGenerateEnd) return;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        if (Vector2.Distance(transform.position, Vector2.zero) <= 18.01f * 2)
            spriteRenderer.size -= new Vector2(Time.deltaTime * 11.65f / (18.01f / speed), 0);
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
        if (spriteRenderer.size.x <= 0)
        {
            NoteManager.Instance.DequeueNote(note.lineIndex);
            NoteManager.Instance.RemoveDictionary(note);
            ComboManager.Instance.JudgeText("Miss");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Move();
    }
}

public class NoteLongHead : NoteObject, INote
{
    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);

        spriteRenderer.size = new Vector2(11.65f * 2, 1);



        transform.localPosition += new Vector3(0, distance + 18.01f);
    }

    public void Move()
    {
        if (!NoteGenerate.Instance.isGenerateEnd) return;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        if (Vector2.Distance(transform.position, Vector2.zero) <= 18.01f * 2)
            spriteRenderer.size -= new Vector2(Time.deltaTime * 11.65f / (18.01f / speed), 0);
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
        if (spriteRenderer.size.x <= 0)
        {
            NoteManager.Instance.DequeueNote(note.lineIndex);
            NoteManager.Instance.RemoveDictionary(note);
            ComboManager.Instance.JudgeText("Miss");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Move();
    }
}

public class NoteLongTail : NoteObject, INote
{
    LineRenderer line;

    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float reachTime = 18.01f / speed;
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f); // 나의 reachTime을 봤을때 0,0과의 거리를 잼
        // reachTime이 0이면 값은 무조건 0임. 0,0에서 시작시킬 순 없으니 Default값으로 18.01을 더해줌. 0,0부터 시작점까지의 거리는 항상 18.01이기 때문임

        // 11.65는 spriteRenderer.size의 x값 내가 보기엔 여기서 이거 설정해주는 식이 잘못됨 ㅇㅇ
        // 그냥 11.65 * 2로 생성하고 그 위치에 왔을때부터 줄여주자. 하드코딩이여도 어쩔 수 없다.

        spriteRenderer.size = new Vector2(11.65f * 2, 1);



        transform.localPosition += new Vector3(0, distance + 18.01f);

        StartCoroutine(Line());
    }

    public void Move()
    {
        if (!NoteGenerate.Instance.isGenerateEnd) return;
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
        float distance = Mathf.Abs(note.reachTime * speed * 0.001f);
        if (Vector2.Distance(transform.position, Vector2.zero) <= 18.01f * 2)
            spriteRenderer.size -= new Vector2(Time.deltaTime * 11.65f / (18.01f / speed), 0);

        if (spriteRenderer.size.x <= 0)
        {
            NoteManager.Instance.DequeueNote(note.lineIndex);
            NoteManager.Instance.RemoveDictionary(note);
            ComboManager.Instance.JudgeText("Miss");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Move();
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