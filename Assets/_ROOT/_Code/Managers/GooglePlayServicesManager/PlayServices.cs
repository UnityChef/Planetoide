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
    private string _cloudFileName = "EcoMundiData";
    private BinaryFormatter _formatter;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //InitializeGPGS();

        // SaveData Init
        //_formatter = new BinaryFormatter();
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
                {

                    p_successCallback?.Invoke();
                }
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

    public void UnlockAchievement(E_AchievementType p_achievementType) //lgsus
    {
        if (!Social.localUser.authenticated)
            return;

        switch (p_achievementType)
        {
            case E_AchievementType.WelcomeToEcoMundi:
                Social.ReportProgress(GPGSIds.achievement_tu_mundi_ha_nacido, 100f, null);
                break;

            case E_AchievementType.EcologicalActionFirst:
                Social.ReportProgress(GPGSIds.achievement_primera_accin_ecolgica, 100f, null);
                break;
        }
    }

    public void UpdateAchievementValue(E_AchievementType p_achievementType) //lgsus
    {
        if (!Social.localUser.authenticated)
            return;

        switch (p_achievementType)
        {
            case E_AchievementType.EcologicalActionBronze:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_accin_ecolgica_bronce, 1, null);
                break;
            case E_AchievementType.EcologicalActionSilver:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_accin_ecolgica_plata, 1, null);
                break;
            case E_AchievementType.EcologicalActionGold:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_accin_ecolgica_oro, 1, null);
                break;

            case E_AchievementType.DailyTaskBronze:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_misiones_diarias_bronce, 1, null);
                break;
            case E_AchievementType.DailyTaskSilver:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_misiones_diarias_plata, 1, null);
                break;
            case E_AchievementType.DailyTaskGold:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.leaderboard_puntaje_el_oro, 1, null);
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
        if (!Social.localUser.authenticated)
            return;

        switch (p_leaderboardType)
        {
            case E_LeaderboardType.ElderMundi:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_mundi_ms_viejo, null);
                break;
            case E_LeaderboardType.Ecuador:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_ecuador, null);
                break;
            case E_LeaderboardType.Galapagos:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_galpagos, null);
                break;
            case E_LeaderboardType.Esmeraldas:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_esmeraldas, null);
                break;
            case E_LeaderboardType.Manabi:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_manab, null);
                break;
            case E_LeaderboardType.SantaElena:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_santa_elena, null);
                break;
            case E_LeaderboardType.LosRios:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_los_rios, null);
                break;
            case E_LeaderboardType.Guayas:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_guayas, null);
                break;
            case E_LeaderboardType.ElOro:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_el_oro, null);
                break;
            case E_LeaderboardType.SantoDomingo:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_santo_domingo, null);
                break;
            case E_LeaderboardType.Pichincha:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_pichincha, null);
                break;
            case E_LeaderboardType.Tungurahua:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_tungurahua, null);
                break;
            case E_LeaderboardType.Cotopaxi:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_cotopaxi, null);
                break;
            case E_LeaderboardType.Carchi:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_carchi, null);
                break;
            case E_LeaderboardType.Chimborazo:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_chimborazo, null);
                break;
            case E_LeaderboardType.Imbabura:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_imbabura, null);
                break;
            case E_LeaderboardType.Loja:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_loja, null);
                break;
            case E_LeaderboardType.Bolivar:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_bolivar, null);
                break;
            case E_LeaderboardType.Azuay:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_azuay, null);
                break;
            case E_LeaderboardType.Cañar:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_caar, null);
                break;
            case E_LeaderboardType.Sucumbios:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_sucumbios, null);
                break;
            case E_LeaderboardType.MoronaSantiago:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_morona_santiago, null);
                break;
            case E_LeaderboardType.Napo:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_napo, null);
                break;
            case E_LeaderboardType.ZamoraChinchipe:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_zamora_chinchipe, null);
                break;
            case E_LeaderboardType.Orellana:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_orellana, null);
                break;
            case E_LeaderboardType.Pastaza:
                Social.ReportScore(p_updateValue, GPGSIds.leaderboard_puntaje_pastaza, null);
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
            //_formatter.Serialize(stream, _gameData);
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

    public void ReadSavedGame(Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(
            _cloudFileName,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            callback);
    }


    public void WriteSavedGame(ISavedGameMetadata game, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
            .WithUpdatedPlayedTime(TimeSpan.FromMinutes(game.TotalTimePlayed.Minutes + 1))
            .WithUpdatedDescription("Saved at: " + System.DateTime.Now);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.CommitUpdate(game, updatedMetadata, SerializeGameData(), callback);
    }


    public void SaveCurrentGameData()
    {
        // Local variable
        ISavedGameMetadata currentGame = null;

        // CALLBACK: Handle the result of a write
        Action<SavedGameRequestStatus, ISavedGameMetadata> writeCallback =
        (SavedGameRequestStatus status, ISavedGameMetadata game) => {
            Debug.Log("(Lollygagger) Saved Game Write: " + status.ToString());
        };

        // CALLBACK: Handle the result of a binary read
        Action<SavedGameRequestStatus, byte[]> readBinaryCallback =
        (SavedGameRequestStatus status, byte[] data) => {
            if (status == SavedGameRequestStatus.Success)
            {
                //try
                //{
                //    _gameData = DeserializeGameData(data);
                //}
                //catch (Exception e)
                //{
                //    Debug.Log("Saved Game Write: convert exception");
                //}

                WriteSavedGame(currentGame, writeCallback);
            }
        };

        // CALLBACK: Handle the result of a read, which should return metadata
        Action<SavedGameRequestStatus, ISavedGameMetadata> readCallback =
        (SavedGameRequestStatus status, ISavedGameMetadata game) => {
            Debug.Log("(Lollygagger) Saved Game Read: " + status.ToString());
            if (status == SavedGameRequestStatus.Success)
            {
                // Read the binary game data
                currentGame = game;
                PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game,
                                                    readBinaryCallback);
            }
        };

        // Read the current data and kick off the callback chain
        Debug.Log("(Lollygagger) Saved Game: Reading");
        ReadSavedGame(readCallback);
    }

   public void LoadGameData()
    {
        ISavedGameMetadata currentGame = null;


        // CALLBACK: Handle the result of a binary read
        Action<SavedGameRequestStatus, byte[]> readBinaryCallback =
        (SavedGameRequestStatus status, byte[] data) => {
            if (status == SavedGameRequestStatus.Success)
            {
                //try
                //{
                //    _gameData = DeserializeGameData(data);
                //    Debug.Log($"Data Loaded: {data}");
                //}
                //catch (Exception e)
                //{
                //    Debug.Log("Game Data was not loaded");
                //}
            }
        };

        // CALLBACK: Handle the result of a read, which should return metadata
        Action<SavedGameRequestStatus, ISavedGameMetadata> readCallback =
        (SavedGameRequestStatus status, ISavedGameMetadata game) => {
            Debug.Log("(Lollygagger) Saved Game Read: " + status.ToString());
            if (status == SavedGameRequestStatus.Success)
            {
                // Read the binary game data
                currentGame = game;
                PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game,
                                                    readBinaryCallback);
            }
        };

        // Read the current data and kick off the callback chain
        Debug.Log("(Lollygagger) Saved Game: Reading");
        ReadSavedGame(readCallback);
    }

    /*
    // <--  SAVE DATA  -->
    private void OpenCloudSave(Action<SavedGameRequestStatus, ISavedGameMetadata> p_callback)
    {
        var platform = (PlayGamesPlatform)Social.Active;
        platform.SavedGame.OpenWithAutomaticConflictResolution(_cloudSaveName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, p_callback);
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

            var platform = (PlayGamesPlatform)Social.Active;
            platform.SavedGame.CommitUpdate(p_meta, update, savedGameData, DataSavedCallback);
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
        else
        {
            Debug.Log("Cannot load data from server");
        }
    }

    private void LoadCallback(SavedGameRequestStatus p_status, byte[] p_gameData)
    {
        _gameData = DeserializeGameData(p_gameData);
        Debug.Log("Game Data Loaded");
    }
    */
    #endregion

}


public enum E_AchievementType
{
    None,
    WelcomeToEcoMundi,
    EcologicalActionFirst,
    EcologicalActionBronze,
    EcologicalActionSilver,
    EcologicalActionGold,
    DailyTaskBronze,
    DailyTaskSilver,
    DailyTaskGold
}

public enum E_LeaderboardType
{
    None,
    ElderMundi,

    Ecuador,

    Galapagos,

    Esmeraldas,
    Manabi,
    SantaElena,
    LosRios,
    Guayas,
    ElOro,
    SantoDomingo,

    Pichincha,
    Tungurahua,
    Cotopaxi,
    Carchi,
    Chimborazo,
    Imbabura,
    Loja,
    Bolivar,
    Azuay,
    Cañar,

    Sucumbios,
    MoronaSantiago,
    Napo,
    ZamoraChinchipe,
    Orellana,
    Pastaza

}

public enum E_DataFiles
{
    None,
    EcoMundiData
}
