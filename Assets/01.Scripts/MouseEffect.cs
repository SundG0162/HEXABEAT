using UnityEngine;

public class MouseEffect : MonoBehaviour
{
    [SerializeField]
    GameObject _mouseParticle;
    Vector2 mousePos;

    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
