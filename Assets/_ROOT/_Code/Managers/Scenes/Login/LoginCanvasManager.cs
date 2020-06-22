using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace EcoMundi.Managers
{
    public class LoginCanvasManager : MonoBehaviour
    {
        [Header("Sign-in")]
        public GameObject signInButtonObject;
        public GameObject playButtonObject;

        private IEnumerator Start()
        {
            playButtonObject.SetActive(false);
            signInButtonObject.SetActive(false);

            yield return Timing.WaitForOneFrame;

            if (Social.Active.localUser.authenticated)
                playButtonObject.SetActive(true);
            else
                signInButtonObject.SetActive(true);

            yield break;
        }
    }
}

