using EcoMundi.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoMundi.Managers
{
    public class MundiManager : MonoBehaviour
    {
        public static MundiManager Instance;

        public GameData data;

        private void Awake()
        {
            Instance = this;
        }

        #region [-----     METHODS     -----]

        public void ModifyMundiHealth(int p_value)
        {
            data.currentHealth += p_value;

            if (data.currentHealth > data.MAX_HEALTH)
                data.currentHealth = data.MAX_HEALTH;
            else if (data.currentHealth < 0)
                data.currentHealth = 0;

            if (data.currentHealth == 0)
                Debug.Log("Mundi Died");
        }

        #endregion
    }
}
