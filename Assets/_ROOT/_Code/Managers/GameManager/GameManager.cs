using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcoMundi.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public delegate void FakeUpdate();
        public static event FakeUpdate OnFakeUpdate;

        public static bool HasFirstTimePlayed { get { return PlayerPrefs.HasKey("FirstTimePlay"); } }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            Application.targetFrameRate = 30;
        }

        private void Start()
        {
            //ErasePlayerPrefs();
            Debug.Log($"=== {HasFirstTimePlayed}");
            SceneManager.LoadScene("LoginScene");
        }

        private void Update()
        {
            if (OnFakeUpdate != null)
                OnFakeUpdate.Invoke();
        }


        [ContextMenu("EraseCachedData")]
        private void ErasePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("[Erase Cached Data] Player Prefs Keys Deleted");
        }

    }
}
