using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Xml.Serialization;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager Instance;

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
        PlayGamesClientConfiguration playGamesConfig = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        // Enable DEBUG output
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(playGamesConfig);
        PlayGamesPlatform.Activate();
        // Trying silent sign-in
        Social.Active.localUser.Authenticate((bool p_success) =>
        {
            if(p_success)
                Debug.Log($"[GameServices] Signed in as {Social.Active.localUser.userName}!");
            else
            Debug.Log($"[GameServices] Sign-in failed...");
        });
    }

    #region [-----     ACHIEVEMENTS     -----]

    public void OpenAchievementsScreen()
    {
        Social.ShowAchievementsUI();
    }

    public void UnlockAchievement(E_AchievementType p_achievementType)
    {
        switch (p_achievementType)
        {
            case E_AchievementType.WelcomeToEcoMundi:
                
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

    public void UpdateLeaderBoardScore(E_LeaderboardType p_leaderboardType, int p_updateValue)
    {
        switch (p_leaderboardType)
        {
            case E_LeaderboardType.ElderMundi:

                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_mundi_ms_viejo, null);

                break;
            
            default:
                break;
        }
    }

    #endregion

    #region [-----     SAVE AND LOAD DATA     -----]



    #endregion

}


public enum E_AchievementType
{
    None,

    WelcomeToEcoMundi

}


public enum E_LeaderboardType
{
    None,

    ElderMundi
}
