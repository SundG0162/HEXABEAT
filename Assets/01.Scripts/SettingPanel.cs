using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SettingPanel : MonoBehaviour, IPointerClickHandler
{
    public int index;
    public UnityEvent<int> OnClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(index);
    }
}
