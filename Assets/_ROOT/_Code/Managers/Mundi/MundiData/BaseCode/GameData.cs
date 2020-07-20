using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace EcoMundi.Data
{
    [CreateAssetMenu(fileName ="MundiData", menuName = "EcoMundi/Data/MundiData", order = 50)]
    [Serializable]
    public class GameData : ScriptableObject
    {
        [Header("Game Configs")]
        private E_DifficultyType _difficultyType;
        public E_Province province;

        public bool IsDifficultySet { get { return _difficultyType != E_DifficultyType.None; } }

        [Header("Mundi Values")]
        public string mundiName;

        [Header("Points")]
        public int gameDays;
        public int gamePoints;
        public int shopPoints;
        [Space]
        public float difficultyModifier;
        [Space]
        public int MAX_HEALTH = 100;
        public int currentHealth = 75;
        public bool IsAlive { get { return currentHealth > 0; } }
        [Space]
        public DateTime birthDate;// = new DateTime();
        public DateTime logOutDate;// = new DateTime();

        // Events
        public delegate void VoidDelegate();
        public static event VoidDelegate OnGameDaysModified;
        public static event VoidDelegate OnGamePointsModified;
        public static event VoidDelegate OnShopPointsModified;
        public static event VoidDelegate OnHealthModified;

        // Resets all fields
        private void OnEnable()
        {
            _difficultyType = E_DifficultyType.None;
            province = E_Province.None;

            mundiName = string.Empty;
            gameDays = 0;
            gamePoints = 0;
            shopPoints = 0;

            currentHealth = 100;
        }

        #region [-----     GAME BEHAVIOURS     -----]

        public void SetDifficultyType(E_DifficultyType p_difficultyType)
        {
            if (p_difficultyType == E_DifficultyType.Easy)
                difficultyModifier = 1.5f;
            else
                difficultyModifier = 1f;

            _difficultyType = p_difficultyType;
        }

        //  GAME DAYS
        public void ModifyGameDays()
        {
            gameDays++;
            OnGameDaysModified?.Invoke();
        }


        public string GetGameDays()
        {
            return gameDays == 0 ? " 0" : gameDays.ToString("### ###");
        }

        //  GAME POINTS
        public void ModifyGamePoints(int p_value)
        {
            gamePoints += Mathf.CeilToInt(p_value * difficultyModifier);
            OnGamePointsModified?.Invoke();
        }

        public string GetGamePoints()
        {
            return gamePoints == 0 ? " 0" : gamePoints.ToString("### ###");
        }

        //  SHOP POINTS
        public void ModifyShopPoints(int p_value)
        {
            shopPoints += p_value;
            OnShopPointsModified?.Invoke();
        }

        public string GetShopPoints()
        {
            return shopPoints == 0 ? " 0" : shopPoints.ToString("### ###");
        }


        public void ModifyMundiHealth(int p_value)
        {
            currentHealth += p_value;

            if (currentHealth > MAX_HEALTH)
                currentHealth = MAX_HEALTH;
            else if (currentHealth < 0)
                currentHealth = 0;

            if (currentHealth == 0)
                Debug.Log("Mundi Died");

            OnHealthModified?.Invoke();
        }
        #endregion

        #region [-----     SAVE & LOAD     -----]



        #endregion

    }
}
