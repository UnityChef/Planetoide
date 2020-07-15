using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using EcoMundi.Data;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.SqlServer.Server;
using System.IO;
using System.Runtime.Serialization;

public class PlayServices : MonoBehaviour
{
    public static PlayServices Instance;

    // SAVE
    [SerializeField]
    public GameData _gameData;
    private string cloudSaveName = "EcoMundiData";
    private BinaryFormatter _formatter;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeGPGS();

        // SaveData Init
        _formatter = new BinaryFormatter();
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

    public void SignIn(Action p_successCallback = null, Action p_errorCallback = null)
    {
        try
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                    p_successCallback?.Invoke();
            });
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
            p_errorCallback?.Invoke();
        }
    }

    public void SignOut()
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.SignOut();
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

    // <--  SERIALIZE  -->
    private byte[] SerializeGameData()
    {
        using (MemoryStream stream = new MemoryStream())
        {
            _formatter.Serialize(stream, _gameData);
            return stream.GetBuffer();
        }
    }

    private GameData DeserializeGameData(byte[] p_data)
    {
        using (MemoryStream stream = new MemoryStream(p_data))
        {
            return (GameData)_formatter.Deserialize(stream);
        }
    }



    // <--  SAVE DATA  -->
    private void OpenCloudSave(Action<SavedGameRequestStatus, ISavedGameMetadata> p_callback)
    {
        var platform = (PlayGamesPlatform)Social.Active;
        platform.SavedGame.OpenWithAutomaticConflictResolution(cloudSaveName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, p_callback);
    }

    public void SaveGameData()
    {
        OpenCloudSave(OnSaveGameData);
    }


    public void OnSaveGameData(SavedGameRequestStatus p_status, ISavedGameMetadata p_meta)
    {
        if (p_status == SavedGameRequestStatus.Success)
        {
            byte[] savedGameData = SerializeGameData();
            SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder()
            .WithUpdatedDescription($"Last save: {System.DateTime.Now}")
            .Build();

            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.CommitUpdate(p_meta, update, savedGameData, DataSavedCallback);
        }
    }

    private void DataSavedCallback(SavedGameRequestStatus p_status, ISavedGameMetadata p_metadata)
    {
        print("DataSaved");
    }



    // <--  LOAD DATA  -->

    public void LoadGameData()
    {
        OpenCloudSave(OnLoadGameData);
    }

    private void OnLoadGameData(SavedGameRequestStatus p_status, ISavedGameMetadata p_meta)
    {
        if(p_status == SavedGameRequestStatus.Success)
        {
            var platform = (PlayGamesPlatform)Social.Active;
            platform.SavedGame.ReadBinaryData(p_meta, LoadCallback);
        }
    }

    private void LoadCallback(SavedGameRequestStatus p_status, byte[] p_gameData)
    {
        _gameData = DeserializeGameData(p_gameData);
        Debug.Log("Game Data Loaded");
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
