using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class Tappable : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClick;

    private float growRatio = 1.1f;
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale *= growRatio;
        OnClick.Invoke();
        print("GROW");
    }
}
