using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace EcoMundi.Data
{
    [CreateAssetMenu(fileName ="MundiData", menuName = "EcoMundi/Data/MundiData", order = 50)]
    [Serializable]
    public class GameData : ScriptableObject
    {
        public string mundiName;

        public int daysAlive;

        public int MAX_HEALTH = 100;
        public int currentHealth = 75;

        public int secondsPlayed;


        #region [-----     SAVE & LOAD     -----]

        public void SaveMundiName()
        {
            if (!PlayerPrefs.HasKey("MundiName"))
                PlayerPrefs.SetString("MundiName", mundiName);
        }

        public void LoadGameData()
        {
            mundiName = PlayerPrefs.GetString("MundiName");
        }

        public void SaveData(string p_SaveKey, string p_KeyValue)
        {
            if(Social.Active.localUser.authenticated)
            {

            }
            else
            {
                if (!PlayerPrefs.HasKey(p_SaveKey))
                    PlayerPrefs.SetString(p_SaveKey, p_KeyValue);
            }
        }

        #endregion

    }

    public enum E_SaveKey
    {
        MundiName,

    }
}
