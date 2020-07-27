using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using EcoMundi.Data;
using MEC;
using EcoMundi.Managers;

namespace EcoMundi.Utility
{
    [RequireComponent(typeof(BoxCollider))]
    public class GameTask : MonoBehaviour, IPointerClickHandler
    {
        [Header("QuizConfig")]
        public E_QuizType quizType;
        [Space]
        public GameObject taskMarkerObject;

        [HideInInspector]
        public bool canBeTapped;
        private UnityEvent _OnClick = new UnityEvent();


        private void Start()
        {
            GameSceneManager.taskObjectList.Add(this);
            _OnClick.AddListener(() => GameCanvasManager.Instance.ShowQuizQuestion(quizType));
        }

        public void ActivateGameTask()
        {
            canBeTapped = true;
            taskMarkerObject.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(canBeTapped)
            {
                _OnClick.Invoke();
                canBeTapped = false;
                taskMarkerObject.SetActive(false);

                GameSceneManager.ActivateTimedRandomTask(5f);
            }
        }
    }
}
