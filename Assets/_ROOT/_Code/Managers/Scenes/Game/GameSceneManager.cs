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
        }

        private void OnDestroy()
        {
            GameManager.OnFakeUpdate -= OnUpdate;
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
