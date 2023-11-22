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
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, ga);
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
        _currentIndex = int.Parse(_currentTarget.name);
        if (_currentIndex >= 3)
        {
            _oppositeIndex = _currentIndex % 3;
        }
        else
        {
            _oppositeIndex = _currentIndex + 3;
        }
        _oppositeTarget = mouseInputs[_oppositeIndex];
        _currentTarget.transform.Find("Glow").gameObject.SetActive(true);
        _oppositeTarget.transform.Find("Glow").gameObject.SetActive(true);
        if (_currentTarget != temp && temp != null)
        {
            temp.transform.Find("Glow").GetComponent<SpriteRenderer>().color = Color.white;
            temp.transform.Find("Glow").gameObject.SetActive(false);
            temp2.transform.Find("Glow").GetComponent<SpriteRenderer>().color = Color.white;
            temp2.transform.Find("Glow").gameObject.SetActive(false);
            if(Input.GetKey(SettingManager.Instance.currentKey))
            {
                Judgement.Instance.CheckLongNote(_currentIndex);
            }
            if (Input.GetKey(SettingManager.Instance.oppositeKey))
            {
                Judgement.Instance.CheckLongNote(_oppositeIndex);
            }
        }
    }

    private void LeftClick()
    {
        var spriteRenderer = _currentTarget.transform.Find("Glow").GetComponent<SpriteRenderer>();

        if(Input.GetKeyDown(SettingManager.Instance.currentKey)) 
        {
            Judgement.Instance.Judge(_currentIndex);
        }
        if (Input.GetKey(SettingManager.Instance.currentKey))
        {
            spriteRenderer.color = Color.blue;
            Judgement.Instance.CheckContinousNote(_currentIndex);
        }
        if (Input.GetKeyUp(SettingManager.Instance.currentKey))
        {
            spriteRenderer.color = Color.white;
            Judgement.Instance.CheckLongNote(_currentIndex);
        }
    }

    private void RightClick()
    {
        var spriteRenderer = _oppositeTarget.transform.Find("Glow").GetComponent<SpriteRenderer>();
        if(Input.GetKeyDown(SettingManager.Instance.oppositeKey)) 
        {
            Judgement.Instance.Judge(_oppositeIndex);
        }
        if (Input.GetKey(SettingManager.Instance.oppositeKey))
        {
            spriteRenderer.color = Color.blue;
            Judgement.Instance.CheckContinousNote(_oppositeIndex);
        }
        if (Input.GetKeyUp(SettingManager.Instance.oppositeKey))
        {
            spriteRenderer.color = Color.white;
            Judgement.Instance.CheckLongNote(_oppositeIndex);
        }
    }
}
