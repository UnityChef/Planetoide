using EcoMundi.Data;
using EcoMundi.Managers;
using MEC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameCanvasManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text mundiNameLabel;
    [Space]
    public TMP_Text gameDaysLabel;
    public TMP_Text gamePointsLabel;
    public TMP_Text shopPointsLabel;


    [Header("LeftSideBar")]
    public GameObject achievementsButtonObject;
    public GameObject leaderboardButtonObject;

    [Header("Game Data")]
    public GameData gameData;

    [Header("Quizes Data")]
    public GameLocalDatabase localDatabase;
    private int _randomQuizIndex;

    [Header("QuizQuestionScreen")]
    public GameObject quizQuestionScreen;
    public TMP_Text quizQuestionLabel;
    [Space]
    public List<Button> answerButtonList;
    [Space]
    public List<TMP_Text> answerButtonLabelList;
    
    [Header("QuizResultScreen")]
    public GameObject quizResultScreen;
    [Space]
    public TMP_Text quizResultFeedbackLabel;

    [Header("Health Bar")]
    public RectTransform healthBarRectTransform;
    public TMP_Text healthBarValueLabel;

    // QUIZZES
    private int _cachedAnswerIndex;

    private void Start()
    {
        Timing.RunCoroutine(C_DecreaseHealth());

        mundiNameLabel.text = gameData.mundiName;
        ModifyHealthBar();

        gameDaysLabel.text = 
        gamePointsLabel.text = gameData.GetGamePoints();
        shopPointsLabel.text = gameData.GetShopPoints();

        achievementsButtonObject.SetActive(Social.localUser.authenticated);
        leaderboardButtonObject.SetActive(Social.localUser.authenticated);


        GameData.OnGameDaysModified += ModifyGameDaysLabel;
        GameData.OnGamePointsModified += ModifyGamePointsLabel;
        GameData.OnShopPointsModified += ModifyShopPointsLabel;
        GameData.OnHealthModified += ModifyHealthBar;
    }

    private void OnDestroy()
    {
        GameData.OnGameDaysModified -= ModifyGameDaysLabel;
        GameData.OnGamePointsModified -= ModifyGamePointsLabel;
        GameData.OnShopPointsModified -= ModifyShopPointsLabel;
        GameData.OnHealthModified -= ModifyHealthBar;
    }

    #region [-----     POINTS DELEGATES SUSCRIPTION METHODS     -----]

    public void ModifyGameDaysLabel()
    {
        gameDaysLabel.text = gameData.GetGameDays();
    }

    public void ModifyGamePointsLabel()
    {
        gamePointsLabel.text = gameData.GetGamePoints();
    }

    public void ModifyShopPointsLabel()
    {
        shopPointsLabel.text = gameData.GetShopPoints();
    }

    #endregion

    #region [-----     SCREENS     -----]

    public void ShowQuizQuestionScreen()
    {
        _randomQuizIndex = Random.Range(0, localDatabase.quizDatabase.Count);

        quizQuestionLabel.text = localDatabase.quizDatabase[_randomQuizIndex].question;

        foreach (Button button in answerButtonList)
            button.gameObject.SetActive(false);

        for (int i = 0; i < localDatabase.quizDatabase[_randomQuizIndex].answerOptions.Count; i++)
        {
            int index = i;
            answerButtonList[i].gameObject.SetActive(true);
            answerButtonLabelList[i].text = localDatabase.quizDatabase[_randomQuizIndex].answerOptions[i].answer;
        }

        quizQuestionScreen.SetActive(true);
    }

    // Called from the buttons at inspector
    public void ButtonAnswerSetAnswerIndex(int p_index)
    {
        _cachedAnswerIndex = p_index;

        quizQuestionScreen.SetActive(false);
        ShowQuizResultScreen();
    }

    public void ShowQuizResultScreen() 
    {
        if(localDatabase.quizDatabase[_randomQuizIndex].answerOptions[_cachedAnswerIndex].isCorrect)
        {
            gameData.ModifyMundiHealth(1);
            gameData.ModifyGamePoints(100);
            gameData.ModifyShopPoints(5);

            GameSceneManager.Instance.ModifyZonesValues(1, localDatabase.quizDatabase[_randomQuizIndex].affectedZoneOne);
            GameSceneManager.Instance.ModifyZonesValues(1, localDatabase.quizDatabase[_randomQuizIndex].affectedZoneTwo);

            //  THIS NEEDS TO BE REFACTORED
            PlayServices.Instance.UpdateAchievementValue(E_AchievementType.EcologicalActionFirst);
            PlayServices.Instance.UpdateAchievementValue(E_AchievementType.EcologicalActionBronze);
            PlayServices.Instance.UpdateAchievementValue(E_AchievementType.EcologicalActionSilver);
            PlayServices.Instance.UpdateAchievementValue(E_AchievementType.EcologicalActionGold);
        }
        else
        {
            gameData.ModifyMundiHealth(-1);

            GameSceneManager.Instance.ModifyZonesValues(-1, localDatabase.quizDatabase[_randomQuizIndex].affectedZoneOne);
            GameSceneManager.Instance.ModifyZonesValues(-1, localDatabase.quizDatabase[_randomQuizIndex].affectedZoneTwo);
        }

        quizResultFeedbackLabel.text = localDatabase.quizDatabase[_randomQuizIndex].answerFeedback;

        quizResultScreen.SetActive(true);
    }


    public void CloseQuizResultScreen()
    {
        quizResultScreen.SetActive(false);
    }

    #endregion


    public void ModifyHealthBar()
    {
        healthBarRectTransform.sizeDelta = new Vector2(gameData.currentHealth * 5f, 0f);
        healthBarValueLabel.text = $"{gameData.currentHealth}%";
    }


    public void AchievementsButton()
    {
        PlayServices.Instance.OpenAchievementsScreen();
    }

    public void LeaderboardButton()
    {
        PlayServices.Instance.OpenLeaderboardScreen();
    }


    private IEnumerator<float> C_DecreaseHealth()
    {
        while(gameData.IsAlive)
        {
            gameData.ModifyMundiHealth(-1);

            yield return Timing.WaitForSeconds(4f);
        }

        yield break;
    }
}
