using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Xml.Serialization;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager Instance;

    public static PlayGamesPlatform platform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeGPGS();
    }

    private void InitializeGPGS()
    {
        // <START> GooglePlayGames init
        PlayGamesClientConfiguration playGamesConfig = new PlayGamesClientConfiguration.Builder().Build();
        // Enable DEBUG output
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(playGamesConfig);
        platform = PlayGamesPlatform.Activate();
        // Trying silent sign-in
    }

    #region [-----     ACHIEVEMENTS     -----]

    public void OpenAchievementsScreen()
    {
        Social.ShowAchievementsUI();
    }

    public void UnlockAchievement(E_Achievement p_achievementType)
    {
        switch (p_achievementType)
        {
            case E_Achievement.WelcomeToEcoMundi:
                
                Social.ReportProgress(GPGSIds.achievement_tu_mundi_ha_nacido, 100f, null);

                break;

            default:
                break;
        }
    }

    #endregion

    #region [-----     LEADERBOARDS     -----]

    public void OpenLeaderboardScreen()
    {
        Social.ShowLeaderboardUI();
    }

    public void UpdateLeaderBoardScore()
    {
        //Social.ReportScore()
    }

    #endregion

}


public enum E_Achievement
{
    None,
    WelcomeToEcoMundi

}


public enum E_Leaderboard
{
    None,
}
