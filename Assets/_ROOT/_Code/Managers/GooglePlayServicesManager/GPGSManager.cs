using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Xml.Serialization;
using System;
using GooglePlayGames.BasicApi.SavedGame;

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

    public void LoadGameData(E_DataFiles p_filename, Action<SavedGameRequestStatus, ISavedGameMetadata> p_callback)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(p_filename.ToString(),
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseMostRecentlySaved,
                                                            p_callback);
    }

    public void SaveGameData(ISavedGameMetadata p_gameMetadata, byte[] p_savedData, Action<SavedGameRequestStatus, ISavedGameMetadata> p_callback)
    {
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
            .WithUpdatedPlayedTime(TimeSpan.FromMinutes(p_gameMetadata.TotalTimePlayed.Minutes + 1))
            .WithUpdatedDescription($"Saved at: {System.DateTime.Now}");

        SavedGameMetadataUpdate updatedMetadata = builder.Build();

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.CommitUpdate(p_gameMetadata, updatedMetadata, p_savedData, p_callback);
    }

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

public enum E_DataFiles
{
    None,
    EcoMundiData
}
