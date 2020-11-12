using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using MEC;

namespace EcoMundi.Managers
{
    public class LoginSceneManager : MonoBehaviour
    {
        [Header("Canvas manager")]
        public LoginCanvasManager canvasManager;

        [Header("Game Services")]
        public TMP_Text authStatusLabel;
        public TMP_Text signInButtonLabel;

        private IEnumerator Start()
        {
            yield return Timing.WaitForOneFrame;
            // Trying silent sign-in
            //PlayServices.Instance.SignIn(SignInSuccessCallback, SignInFailCallback);

            if (!GameManager.HasFirstTimePlayed)
                canvasManager.ShowScreen(LoginCanvasManager.E_ScreenType.Welcome);
            else
                canvasManager.ShowScreen(LoginCanvasManager.E_ScreenType.Play);

            yield break;
        }

        #region [-----     GOOGLE PLAY GAMES SERVICES     -----]

        public void SignInToPlayServicesButton() //lgsus
        {
            if (!Social.localUser.authenticated)
            {
                PlayServices.Instance.SignIn(SignInSuccessCallback, SignInFailCallback);
            }
            else
            {
                PlayGamesPlatform.Instance.SignOut();

                signInButtonLabel.text = "Sign in";
                authStatusLabel.text = "";
            }
        }


        private void SignInSuccessCallback()
        {
            Debug.Log($"[GameServices] Signed in !");

            signInButtonLabel.text = "Sign out";
            authStatusLabel.text = $"Signed in as: {Social.Active.localUser.userName}";

            canvasManager.playButtonObject.SetActive(true);

            PlayServices.Instance.LoadGameData();
        }

        private void SignInFailCallback()
        {
            Debug.Log($"[GameServices] Sign-in failed...");

            signInButtonLabel.text = "Sign in";
            authStatusLabel.text = $"Sign-in failed...";
        }


        #endregion

        public void GoToGameScene()
        {
            AudioManager.Instance.PlaySound(E_SoundEffects.Accept);
            SceneManager.LoadScene("GameScene");
        }
    }
}
