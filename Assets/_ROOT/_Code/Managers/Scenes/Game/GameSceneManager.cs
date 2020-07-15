using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace EcoMundi.Managers
{
    public class GameSceneManager : MonoBehaviour
    {
        [Header("Sun")]
        public Transform sunTransform;
        [Range(0f,100f)]
        public float sunRotationSpeed = 10f;

        private void Start()
        {
            PlayServices.Instance.UnlockAchievement(E_AchievementType.WelcomeToEcoMundi);

            GameManager.OnFakeUpdate += OnUpdate;
        }

        private void OnDestroy()
        {
            GameManager.OnFakeUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            sunTransform.Rotate(Vector3.up, sunRotationSpeed * Time.deltaTime);
        }

        public void QuitGame()
        {
            PlayServices.Instance.SaveCurrentGameData();

            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
