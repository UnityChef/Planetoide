using EcoMundi.Data;
using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace EcoMundi.Managers
{
    public class GameSceneManager : MonoBehaviour
    {
        public static GameSceneManager Instance;

        [Header("GameData")]
        public GameData gameData;

        [Header("Planet")]
        public SpriteRenderer gestureSpriteRenderer;
        [Space]
        public Sprite gestureHappy;
        public Sprite gestureNormal;
        public Sprite gestureSad;
        public Sprite gestureWorried;
        public Sprite gestureSick;
        public Sprite gestureDead;

        [Header("Stars")]
        public ParticleSystem starsParticles;

        [Header("Sun")]
        public Transform sunTransform;
        [Range(0f,100f)]
        public float sunRotationSpeed = 10f;

        [Header("Zones")]
        public ZoneManager carbonZoneManager;
        public ZoneManager cropsZoneManager;
        public ZoneManager forestZoneManager;
        public ZoneManager farmingZoneManager;
        public ZoneManager fisheryZoneManager;
        public ZoneManager cityZoneManager;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if(!GameManager.HasFirstTimePlayed)
            {
                gameData.birthDate = DateTime.Now;
            }

            if(Social.localUser.authenticated)
            {
                Timing.CallDelayed(2f, () => PlayServices.Instance.UnlockAchievement(E_AchievementType.WelcomeToEcoMundi));
            }

            // Pauses Stars
            Timing.CallDelayed(1f, () => starsParticles.Pause());

            carbonZoneManager.InitZoneTier(5);
            cropsZoneManager.InitZoneTier(5);
            forestZoneManager.InitZoneTier(5);
            farmingZoneManager.InitZoneTier(5);
            fisheryZoneManager.InitZoneTier(5);
            cityZoneManager.InitZoneTier(5);


            GameManager.OnFakeUpdate += OnUpdate;
            GameData.OnHealthModified += UpdatePlanetFaceHandle;
        }

        private void OnDestroy()
        {
            GameManager.OnFakeUpdate -= OnUpdate;
            GameData.OnHealthModified -= UpdatePlanetFaceHandle;
        }

        private void OnUpdate()
        {
            sunTransform.Rotate(Vector3.up, sunRotationSpeed * Time.deltaTime);
        }

        private void OnApplicationQuit()
        {
            gameData.logOutDate = DateTime.Now;

            gameData.UpdateLeaderboardsValue();
            
            //PlayServices.Instance.SaveCurrentGameData();
        }

        #region [-----     BEHAVIOUR     -----]

        public void ModifyZonesValues(int p_value, E_ZoneType p_zoneType)
        {
            if (p_zoneType == E_ZoneType.Carbon)
                carbonZoneManager.ModifyZoneTier(p_value);

            if (p_zoneType == E_ZoneType.Crops)
                cropsZoneManager.ModifyZoneTier(p_value);

            if (p_zoneType == E_ZoneType.Forest)
                forestZoneManager.ModifyZoneTier(p_value);

            if (p_zoneType == E_ZoneType.Farming)
                farmingZoneManager.ModifyZoneTier(p_value);

            if (p_zoneType == E_ZoneType.Fishery)
                fisheryZoneManager.ModifyZoneTier(p_value);

            if (p_zoneType == E_ZoneType.City)
                cityZoneManager.ModifyZoneTier(p_value);
        }


        private void UpdatePlanetFaceHandle()
        {
            if (gameData.currentHealth > 90)
                gestureSpriteRenderer.sprite = gestureHappy;
            else if(gameData.currentHealth > 70)
                gestureSpriteRenderer.sprite = gestureNormal;
            else if (gameData.currentHealth > 50)
                gestureSpriteRenderer.sprite = gestureSad;
            else if (gameData.currentHealth > 30)
                gestureSpriteRenderer.sprite = gestureWorried;
            else if (gameData.currentHealth > 0)
                gestureSpriteRenderer.sprite = gestureSick;
            else 
                gestureSpriteRenderer.sprite = gestureDead;
        }

        #endregion

        public void QuitGame()
        {
            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
