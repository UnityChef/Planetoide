using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class Tappable : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }
}
