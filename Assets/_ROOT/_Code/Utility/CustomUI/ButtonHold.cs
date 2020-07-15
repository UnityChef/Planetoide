using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using EcoMundi.Managers;
using UnityEngine.UI;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnTapDown;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        GameManager.OnFakeUpdate += OnUpdate;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        GameManager.OnFakeUpdate -= OnUpdate;
    }

    private void OnUpdate()
    {
        OnTapDown.Invoke();
    }

    
}
