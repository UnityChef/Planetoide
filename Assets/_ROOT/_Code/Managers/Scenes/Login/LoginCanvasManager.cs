using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using EcoMundi.Data;
using TMPro;
using System.Linq;

namespace EcoMundi.Managers
{
    public class LoginCanvasManager : MonoBehaviour
    {
        [Header("Game Data")]
        public GameData gameData;

        [Header("Sign-in")]
        public GameObject signInButtonObject;
        public GameObject playButtonObject;

        [Header("Welcome Screen")]
        public GameObject welcomeScreenObject;
        
        [Header("Mundi Data Screen")]
        public GameObject mundiDataScreenObject;
        public TMP_InputField mundiNameInputField;
        [Space]
        public TMP_Dropdown provinceDropdown;
        [Space]
        public TMP_Text validationMessageLabel;

        [Header("Play Screen")]
        public GameObject playScreenObject;


        private IEnumerator Start()
        {
           // playButtonObject.SetActive(false);
            signInButtonObject.SetActive(false);

            yield return Timing.WaitForOneFrame;

            if (Social.Active.localUser.authenticated)
                playButtonObject.SetActive(true);
            else
                signInButtonObject.SetActive(true);

            yield break;
        }


        #region [-----     SCREENS BEHAVIOUR     -----]

        public void ContinueButton_WelcomeScreen()
        {
            ShowScreen(E_ScreenType.MundiData);
        }

        public void ValidateMundiData()
        {
            if (mundiNameInputField.text == string.Empty || mundiNameInputField.text.Length < 3)
            {
                validationMessageLabel.text = "El nombre de tu Mundi no puede ser vacio, mínimo 4 letras.";
                return;
            }

            if (provinceDropdown.value == 0)
            {
                validationMessageLabel.text = "Por favor, selecciona una provincia para continuar";
                return;
            }

            if (!gameData.IsDifficultySet)
            {
                validationMessageLabel.text = "Por favor, selecciona una dificultad para continuar";
                return;
            }

            validationMessageLabel.text = string.Empty;

            gameData.mundiName = mundiNameInputField.text;
            gameData.province = (E_Province)provinceDropdown.value;

            ShowScreen(E_ScreenType.Play);
        }

        public void EasyDifficultyButton()
        {
            gameData.SetDifficultyType(E_DifficultyType.Easy);
        }

        public void NormalDifficultyButton()
        {
            gameData.SetDifficultyType(E_DifficultyType.Normal);
        }

        #endregion


        public void ShowScreen(E_ScreenType p_screenType)
        {
            welcomeScreenObject.SetActive(false);
            mundiDataScreenObject.SetActive(false);

            switch (p_screenType)
            {
                case E_ScreenType.Welcome:
                    welcomeScreenObject.SetActive(true);
                    break;

                case E_ScreenType.MundiData:
                    mundiDataScreenObject.SetActive(true);
                    break;

                case E_ScreenType.Play:
                    playScreenObject.SetActive(true);
                    break;
            }
        }

        public enum E_ScreenType
        {
            Welcome,
            MundiData,
            Play
        }
    }
}

