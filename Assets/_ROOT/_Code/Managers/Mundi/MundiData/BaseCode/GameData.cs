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
        public int gamePoints;
        public int shopPoints;
        [Space]
        public float difficultyModifier;
        [Space]
        public int MAX_HEALTH = 100;
        public int currentHealth = 0;
        public bool IsAlive { get { return currentHealth > 0; } }
        [Space]
        public DateTime birthDate;// = new DateTime();
        public DateTime logOutDate;// = new DateTime();
        [Space]
        // Ecofootprints values
        public int ecofootprintCityValue;
        public int ecofootprintCropsValue;
        public int ecofootprintForestValue;
        public int ecofootprintFarmingValue;
        public int ecofootprintFisheriesValue;
        public int ecofootprintCarbonValue;

        // Achievements values
        public int ecologicalActionsDone = 0;

        // Events
        public delegate void VoidDelegate();
        public static event VoidDelegate OnGamePointsModified;
        public static event VoidDelegate OnShopPointsModified;
        public static event VoidDelegate OnHealthModified;

        // Resets all fields
        private void OnEnable()
        {
            //_difficultyType = E_DifficultyType.None;
            //province = E_Province.None;

            //mundiName = string.Empty;
            //gamePoints = 0;
            //shopPoints = 0;

            //currentHealth = 100;
        }

        #region [-----     GAME BEHAVIOURS     -----]

        public void SetNewGameData(string p_mundiName, E_Province p_provinceType)
        {
            // PlayerPrefsDataRelated
            PlayerPrefs.SetString("FirstTimePlay", "true");

            mundiName = p_mundiName;
            province = p_provinceType;
            currentHealth = 0;
            gamePoints = 0;
            shopPoints = 0;

            ecofootprintCityValue = 0;
            ecofootprintCropsValue = 0;
            ecofootprintForestValue = 0;
            ecofootprintFarmingValue = 0;
            ecofootprintFisheriesValue = 0;
            ecofootprintCarbonValue = 0;
        }

        public void SetDifficultyType(E_DifficultyType p_difficultyType)
        {
            if (p_difficultyType == E_DifficultyType.Easy)
                difficultyModifier = 1f;
            else
                difficultyModifier = 3f;

            _difficultyType = p_difficultyType;
        }

        public E_DifficultyType GetDifficulty()
        {
            return _difficultyType;
        }

        //  GAME POINTS
        public void ModifyGamePoints(int p_value)
        {
            gamePoints += Mathf.CeilToInt(p_value * difficultyModifier);

            if (gamePoints < 0)
                gamePoints = 0;

            OnGamePointsModified?.Invoke();
        }

        public string GetGamePoints()
        {
            return gamePoints == 0 ? " 0" : gamePoints.ToString("### ###");
        }

        //  SHOP POINTS
        public void ModifyShopPoints(int p_value)
        {
            shopPoints += Mathf.CeilToInt(p_value * difficultyModifier);

            if (shopPoints < 0)
                shopPoints = 0;

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

        public void UpdateLeaderboardsValue()
        {
            PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Ecuador, gamePoints);

            switch (province)
            {
                case E_Province.Galapagos:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Galapagos, gamePoints);
                    break;
                case E_Province.Esmeraldas:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Esmeraldas, gamePoints);
                    break;
                case E_Province.Manabi:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Manabi, gamePoints);
                    break;
                case E_Province.SantaElena:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.SantaElena, gamePoints);
                    break;
                case E_Province.LosRios:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.LosRios, gamePoints);
                    break;
                case E_Province.Guayas:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Guayas, gamePoints);
                    break;
                case E_Province.ElOro:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.ElOro, gamePoints);
                    break;
                case E_Province.SantoDomingo:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.SantoDomingo, gamePoints);
                    break;
                case E_Province.Pichincha:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Pichincha, gamePoints);
                    break;
                case E_Province.Tungurahua:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Tungurahua, gamePoints);
                    break;
                case E_Province.Cotopaxi:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Cotopaxi, gamePoints);
                    break;
                case E_Province.Carchi:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Carchi, gamePoints);
                    break;
                case E_Province.Chimborazo:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Chimborazo, gamePoints);
                    break;
                case E_Province.Imbabura:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Imbabura, gamePoints);
                    break;
                case E_Province.Loja:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Loja, gamePoints);
                    break;
                case E_Province.Bolivar:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Bolivar, gamePoints);
                    break;
                case E_Province.Azuay:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Azuay, gamePoints);
                    break;
                case E_Province.Cañar:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Cañar, gamePoints);
                    break;
                case E_Province.Sucumbios:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Sucumbios, gamePoints);
                    break;
                case E_Province.MoronaSantiago:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.MoronaSantiago, gamePoints);
                    break;
                case E_Province.Napo:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Napo, gamePoints);
                    break;
                case E_Province.ZamoraChinchipe:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.ZamoraChinchipe, gamePoints);
                    break;
                case E_Province.Orellana:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Orellana, gamePoints);
                    break;
                case E_Province.Pastaza:
                    PlayServices.Instance.UpdateLeaderBoardScore(E_LeaderboardType.Pastaza, gamePoints);
                    break;
            }
        }

        #endregion

    }
}
