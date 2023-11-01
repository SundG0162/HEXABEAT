using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEffect : MonoBehaviour
{
    [SerializeField]
    GameObject _mouseParticle;
    Vector2 mousePos;

    private void Start()
    {
        Cursor.visible = true;
        //StartCoroutine(IEParticle());
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    IEnumerator IEParticle()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        while (true)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Instantiate(_mouseParticle, mousePos, Quaternion.identity);
            yield return wait;
        }
    }
}
