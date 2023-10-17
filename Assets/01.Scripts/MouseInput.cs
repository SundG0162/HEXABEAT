using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private GameObject _oppositeTarget = null;
    private GameObject _currentTarget = null;
    public GameObject[] mouseInputs;
    [SerializeField]
    private int _currentIndex = 0;
    [SerializeField]
    private int _oppositeIndex = 0;

    [SerializeField]
    LayerMask ga;

    private void Update()
    {
        ChangeTarget();
        if (_currentTarget == null) return;
        LeftClick();
        RightClick();
    }

    private void ChangeTarget() // for문을 사용하고 싶었지만 그건 나의 희망사항일뿐 무수한 NullReference의 요청으로 되돌림
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2 = -Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, ga);
        RaycastHit2D hit2 = Physics2D.Raycast(mousePos2, Vector2.zero, 0f, ga);
        if (hit.collider == null)
        {
            if (_currentTarget != null)
            {
                _currentTarget.transform.Find("Glow").GetComponent<SpriteRenderer>().color = Color.white;
                _currentTarget.transform.Find("Glow").gameObject.SetActive(false);
                _oppositeTarget.transform.Find("Glow").GetComponent<SpriteRenderer>().color = Color.white;
                _oppositeTarget.transform.Find("Glow").gameObject.SetActive(false);
                _currentTarget = null;
                _oppositeTarget = null;
            }
            return;
        }
        GameObject temp = _currentTarget;
        GameObject temp2 = _oppositeTarget;
        _currentTarget = hit.transform.gameObject;
        _oppositeTarget = hit2.transform.gameObject;
        _currentTarget.transform.Find("Glow").gameObject.SetActive(true);
        _oppositeTarget.transform.Find("Glow").gameObject.SetActive(true);
        _oppositeTarget.transform.Find("Glow").GetComponent<SpriteRenderer>().color = Color.gray;
        if (_currentTarget != temp && temp != null)
        {
            temp.transform.Find("Glow").GetComponent<SpriteRenderer>().color = Color.white;
            temp.transform.Find("Glow").gameObject.SetActive(false);
            temp2.transform.Find("Glow").GetComponent<SpriteRenderer>().color = Color.white;
            temp2.transform.Find("Glow").gameObject.SetActive(false);
        }
        _currentIndex = int.Parse(_currentTarget.name);
        _oppositeIndex = int.Parse(_oppositeTarget.name);
    }

    private void LeftClick()
    {
        var spriteRenderer = _currentTarget.transform.Find("Glow").GetComponent<SpriteRenderer>();
        if (Input.GetMouseButton(0))
        {
            //Judgement.Instance.Judge(_currentIndex);
            spriteRenderer.color = Color.blue;
        }
        if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void RightClick()
    {
        var spriteRenderer = _oppositeTarget.transform.Find("Glow").GetComponent<SpriteRenderer>();
        if (Input.GetMouseButton(1))
        {
            //Judgement.Instance.Judge(_oppositeIndex);
            spriteRenderer.color = Color.blue;
        }
        if (Input.GetMouseButtonUp(1))
        {
            spriteRenderer.color = Color.white;
        }
    }
}
