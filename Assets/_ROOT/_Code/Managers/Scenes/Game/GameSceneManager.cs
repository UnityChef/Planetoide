using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoMundi.Managers
{
    public class GameSceneManager : MonoBehaviour
    {
        private void Start()
        {
            GPGSManager.Instance.UnlockAchievement(E_Achievement.WelcomeToEcoMundi);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
