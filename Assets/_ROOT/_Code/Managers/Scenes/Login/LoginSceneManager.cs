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
            Social.Active.localUser.Authenticate(SignInCallback);

            yield break;
        }

        #region [-----     GOOGLE PLAY GAMES SERVICES     -----]

        public void SignInToPlayServicesButton()
        {
            if (!Social.Active.localUser.authenticated)
            {
                Social.Active.localUser.Authenticate(SignInCallback);
            }
            else
            {
                PlayGamesPlatform.Instance.SignOut();

                signInButtonLabel.text = "Sign in";
                authStatusLabel.text = "";
            }
        }


        private void SignInCallback(bool p_success)
        {
            if (p_success)
            {
                Debug.Log($"[GameServices] Signed in !");

                signInButtonLabel.text = "Sign out";
                authStatusLabel.text = $"Signed in as: {Social.Active.localUser.userName}";

                canvasManager.playButtonObject.SetActive(true);
            }
            else
            {
                Debug.Log($"[GameServices] Sign-in failed...");

                signInButtonLabel.text = "Sign in";
                authStatusLabel.text = $"Sign-in failed...";
            }
        }

        #endregion

        public void GoToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
