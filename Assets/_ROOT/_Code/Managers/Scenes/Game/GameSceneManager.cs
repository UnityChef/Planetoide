using EcoMundi.Data;
using EcoMundi.Utility;
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
        public GameLocalDatabase gameLocalData;

        [Header("Planet")]
        public SpriteRenderer gestureSpriteRenderer;
        [Space]
        public Sprite gestureHappy;
        public Sprite gestureNormal;
        public Sprite gestureSad;
        public Sprite gestureWorried;
        public Sprite gestureSick;
        public Sprite gestureDead;

        [Header("Tasks")]
        public static List<GameTask> taskObjectList = new List<GameTask>();
        private const int maxTaskValue = 5;

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

        [Header("Monument")]
        public Transform monumentParentTransform;

        // Extras
        private const string MORE_INFO_URL = "https://gk.city/ecomundi/";

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            //if(!GameManager.HasFirstTimePlayed)
            //{
            //    gameData.birthDate = DateTime.Now;
            //}

            //if(Social.localUser.authenticated)
            //{
            //    Timing.CallDelayed(2f, () => PlayServices.Instance.UnlockAchievement(E_AchievementType.WelcomeToEcoMundi));
            //}

            carbonZoneManager.InitZoneTier(gameData.ecofootprintCarbonValue);
            cropsZoneManager.InitZoneTier(gameData.ecofootprintCropsValue);
            forestZoneManager.InitZoneTier(gameData.ecofootprintForestValue);
            farmingZoneManager.InitZoneTier(gameData.ecofootprintFarmingValue);
            fisheryZoneManager.InitZoneTier(gameData.ecofootprintFisheriesValue);
            cityZoneManager.InitZoneTier(gameData.ecofootprintCityValue);

            GameManager.OnFakeUpdate += OnUpdate;
            GameData.OnHealthModified += UpdatePlanetFaceHandle;

            // Pauses Stars
            Timing.CallDelayed(1f, () => starsParticles.Pause());

            // Activate 5 Task
            ActivateTimedRandomTask(5f);
            ActivateTimedRandomTask(5f);
            ActivateTimedRandomTask(5f);
            ActivateTimedRandomTask(5f);
            ActivateTimedRandomTask(5f);

            // Monument
            foreach (ProvincePropsData provinceData in gameLocalData.provinceDatabase)
            {
                if (gameData.province == provinceData.province)
                {
                    Instantiate(provinceData.symbolPrefab, monumentParentTransform);
                    break;
                }
            }

            UpdatePlanetFaceHandle();
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

            //gameData.UpdateLeaderboardsValue(); //lgsus
            
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
            if (gameData.currentHealth > 80)
                gestureSpriteRenderer.sprite = gestureHappy;
            else if(gameData.currentHealth > 60)
                gestureSpriteRenderer.sprite = gestureNormal;
            else if (gameData.currentHealth > 40)
                gestureSpriteRenderer.sprite = gestureWorried;
            else if (gameData.currentHealth > 20)
                gestureSpriteRenderer.sprite = gestureSad;
            else if (gameData.currentHealth > 0)
                gestureSpriteRenderer.sprite = gestureSick;
            else 
                gestureSpriteRenderer.sprite = gestureDead;
        }

        public static void ActivateTimedRandomTask(float p_minTime)
        {
            Timing.CallDelayed(UnityEngine.Random.Range(p_minTime, p_minTime * 3f), () => ActivateRandomTask());
        }

        private static void ActivateRandomTask()
        {
            if(taskObjectList.Count < maxTaskValue)
            {
                Debug.LogError($"[GameSceneManager] Please add more GameTasks to the game, you need at least 5 to allow Task System to work :: At the moment you have {taskObjectList.Count} GameTask in your scene");
                return;
            }

            int randomIndex;

            do
            {
                randomIndex = UnityEngine.Random.Range(0, taskObjectList.Count);
                if(!taskObjectList[randomIndex].canBeTapped)
                {
                    taskObjectList[randomIndex].ActivateGameTask();
                    break;
                }
            }
            while (true);

            taskObjectList[randomIndex].ActivateGameTask();
        }

        #endregion

        public void QuitGame()
        {
            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        public void OpenWebURL()
        {
#if UNITY_ANDROID
            Application.OpenURL(MORE_INFO_URL);
#endif
        }
    }
}
