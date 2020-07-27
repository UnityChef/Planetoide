using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using EcoMundi.Data;

namespace EcoMundi.Utility
{
    [RequireComponent(typeof(BoxCollider))]
    public class Tappable : MonoBehaviour, IPointerClickHandler
    {
        [Header("QuizConfig")]
        public E_QuizType type;

        private UnityEvent _OnClick = new UnityEvent();


        private void Start()
        {
            _OnClick.AddListener(() => GameCanvasManager.Instance.ShowQuizQuestion());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _OnClick.Invoke();
        }
    }
}
