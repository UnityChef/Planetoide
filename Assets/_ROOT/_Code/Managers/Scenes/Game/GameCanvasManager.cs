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
    public static GameCanvasManager Instance;

    [Header("UI")]
    public TMP_Text mundiNameLabel;
    [Space]
    public TMP_Text gamePointsLabel;
    public TMP_Text shopPointsLabel;

    [Header("LeftSideBar")]
    public GameObject achievementsButtonObject;
    public GameObject leaderboardButtonObject;

    [Header("Game Data")]
    public GameData gameData;

    [Header("Quizes Data")]
    public GameLocalDatabase localDatabase;
    private int _quizIndex;

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
    public TMP_Text quizTitleLabel;
    public Image quizResultImage;
    // Ecofootprint 1 related
    public Image ecofootprint1Image;
    public TMP_Text ecofootprint1NameLabel;
    public Image ecofootprint1ValueBackgroundImage;
    public TMP_Text ecofootprint1Valuelabel;
    // Ecofootprint 2 related
    public Image ecofootprint2Image;
    public TMP_Text ecofootprint2NameLabel;
    public Image ecofootprint2ValueBackgroundImage;
    public TMP_Text ecofootprint2Valuelabel;
    // Ecofootprint 2 related
    public TMP_Text healthValueLabel;
    public Image healthValuebackgroundImage;
    public GameObject healthPlanetCorrectFaceObject;
    public GameObject healthPlanetIncorrectFaceObject;
    //Prizes related
    public TMP_Text resultPrizeLabel;
    public GameObject wonPrizeObject;
    public GameObject noPrizeObject;
    public TMP_Text pointsWonLabel;
    public TMP_Text coinsWonLabel;
    [Header("Assets")]
    public Sprite quizCorrectSprite;
    public Sprite quizWrongSprite;
    public Color32 correctColor;
    public Color32 incorrectColor;

    [Header("Health Bar")]
    public RectTransform healthBarRectTransform;
    public TMP_Text healthBarValueLabel;

    [Header("EcoFootprintInfoScreen")]
    public GameObject ecoFootprintInfoScreen;
    public GameObject ecoFootprintInfoDialogScreen;
    // EcoFootprints dialogs
    public GameObject ecoFootprintDialogGroupCarbono;
    public GameObject ecoFootprintDialogGroupPastoreo;
    public GameObject ecoFootprintDialogGroupCultivos;
    public GameObject ecoFootprintDialogGroupPesca;
    public GameObject ecoFootprintDialogGroupForestal;
    public GameObject ecoFootprintDialogGroupurbanismo;

    [Header("TutorialScreen")]
    public GameObject tutorialScreen;

    // QUIZZES
    private int _cachedAnswerIndex;
    private EcoFootprint _cachedEcofootprint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Timing.RunCoroutine(C_DecreaseHealth());

        mundiNameLabel.text = gameData.mundiName;
        ModifyHealthBar();

        gamePointsLabel.text = gameData.GetGamePoints();
        shopPointsLabel.text = gameData.GetShopPoints();

        //achievementsButtonObject.SetActive(Social.localUser.authenticated);
        //leaderboardButtonObject.SetActive(Social.localUser.authenticated);

        achievementsButtonObject.SetActive(false);
        leaderboardButtonObject.SetActive(false);

        GameData.OnGamePointsModified += ModifyGamePointsLabel;
        GameData.OnShopPointsModified += ModifyShopPointsLabel;
        GameData.OnHealthModified += ModifyHealthBar;
    }

    private void OnDestroy()
    {
        GameData.OnGamePointsModified -= ModifyGamePointsLabel;
        GameData.OnShopPointsModified -= ModifyShopPointsLabel;
        GameData.OnHealthModified -= ModifyHealthBar;
    }

    #region [-----     POINTS DELEGATES SUSCRIPTION METHODS     -----]

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

    // This function is used to call a new Question from the QuizQuestionList
    public void ShowQuizQuestion(E_QuizType p_quizType)
    {
        _quizIndex = localDatabase.GetRandomQuizIndex(p_quizType);

        quizQuestionLabel.text = localDatabase.quizDatabase[_quizIndex].question;

        foreach (Button button in answerButtonList)
            button.gameObject.SetActive(false);

        for (int i = 0; i < localDatabase.quizDatabase[_quizIndex].answerOptions.Count; i++)
        {
            answerButtonList[i].gameObject.SetActive(true);
            answerButtonLabelList[i].text = localDatabase.quizDatabase[_quizIndex].answerOptions[i].answer;
        }

        quizQuestionScreen.SetActive(true);
    }

    public void CloseQuizQuestionScreen()
    {
        quizQuestionScreen.SetActive(false);
    }

    public void CloseEcoFootprintInfoScreen()
    {
        ecoFootprintInfoScreen.SetActive(false);
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
        healthPlanetCorrectFaceObject.SetActive(false);
        healthPlanetIncorrectFaceObject.SetActive(false);

        wonPrizeObject.SetActive(false);
        noPrizeObject.SetActive(false);

        // Affected zone 1
        _cachedEcofootprint = localDatabase.GetZoneTypeInformation(localDatabase.quizDatabase[_quizIndex].affectedZoneOne);
        ecofootprint1Image.sprite = _cachedEcofootprint.zoneSprite;
        ecofootprint1NameLabel.text = _cachedEcofootprint.zoneName;

        // Affected zone 2
        _cachedEcofootprint = localDatabase.GetZoneTypeInformation(localDatabase.quizDatabase[_quizIndex].affectedZoneTwo);
        ecofootprint2Image.sprite = _cachedEcofootprint.zoneSprite;
        ecofootprint2NameLabel.text = _cachedEcofootprint.zoneName;


        if (localDatabase.quizDatabase[_quizIndex].answerOptions[_cachedAnswerIndex].isCorrect)
        {
            resultPrizeLabel.text = "Recompensas";
            if (gameData.GetDifficulty() == E_DifficultyType.Easy)
            {
                gameData.ModifyMundiHealth(5);
                healthValueLabel.text = "+5";
            }
            else
            {
                gameData.ModifyMundiHealth(2);
                healthValueLabel.text = "+2";
            }

            gameData.ModifyGamePoints(100);
            gameData.ModifyShopPoints(1);

            GameSceneManager.Instance.ModifyZonesValues(1, localDatabase.quizDatabase[_quizIndex].affectedZoneOne);
            GameSceneManager.Instance.ModifyZonesValues(1, localDatabase.quizDatabase[_quizIndex].affectedZoneTwo);

            //  THIS NEEDS TO BE REFACTORED
            //PlayServices.Instance.UpdateAchievementValue(E_AchievementType.EcologicalActionFirst);
            //PlayServices.Instance.UpdateAchievementValue(E_AchievementType.EcologicalActionBronze);
            //PlayServices.Instance.UpdateAchievementValue(E_AchievementType.EcologicalActionSilver);
            //PlayServices.Instance.UpdateAchievementValue(E_AchievementType.EcologicalActionGold);

            quizTitleLabel.text = "¡CORRECTO!";
            AudioManager.Instance.PlaySound(E_SoundEffects.AnswerCorrect);
            quizResultImage.sprite = quizCorrectSprite;

            // Affected zone 1
            ecofootprint1ValueBackgroundImage.color = correctColor;
            ecofootprint1Valuelabel.text = "+1";

            // Affected zone 2
            ecofootprint2ValueBackgroundImage.color = correctColor;
            ecofootprint2Valuelabel.text = "+1";

            // Affected planet health
            healthValuebackgroundImage.color = correctColor;
            healthPlanetCorrectFaceObject.SetActive(true);

            pointsWonLabel.text = $"+{100 * gameData.difficultyModifier} Ecopuntos";
            coinsWonLabel.text = $"+{1 * gameData.difficultyModifier} Ecomonedas";
            wonPrizeObject.SetActive(true);
        }
        else
        {
            if (gameData.GetDifficulty() == E_DifficultyType.Easy)
            {
                gameData.ModifyMundiHealth(0);
                healthValueLabel.text = "0";
            }
            else
            {
                gameData.ModifyMundiHealth(-2);
                healthValueLabel.text = "-2";
            }

            GameSceneManager.Instance.ModifyZonesValues(-1, localDatabase.quizDatabase[_quizIndex].affectedZoneOne);
            GameSceneManager.Instance.ModifyZonesValues(-1, localDatabase.quizDatabase[_quizIndex].affectedZoneTwo);

            quizTitleLabel.text = "INCORRECTO";
            AudioManager.Instance.PlaySound(E_SoundEffects.AnswerWrong);
            quizResultImage.sprite = quizWrongSprite;

            // Affected zone 1
            ecofootprint1ValueBackgroundImage.color = incorrectColor;
            ecofootprint1Valuelabel.text = "-1";

            // Affected zone 2
            ecofootprint2ValueBackgroundImage.color = incorrectColor;
            ecofootprint2Valuelabel.text = "-1";

            // Affected planet health
            healthValuebackgroundImage.color = incorrectColor;
            healthPlanetIncorrectFaceObject.SetActive(true);

            if (gameData.GetDifficulty() == E_DifficultyType.Normal)
            {
                resultPrizeLabel.text = "Penalización";
                gameData.ModifyGamePoints(-100);
                gameData.ModifyShopPoints(-1);

                pointsWonLabel.text = $"-{(100 * gameData.difficultyModifier)} Ecopuntos";
                coinsWonLabel.text = $"-{1 * gameData.difficultyModifier} Ecomonedas";

                wonPrizeObject.SetActive(true);
            }
            else
            {
                resultPrizeLabel.text = "Recompensas";
                noPrizeObject.SetActive(true);
            }

        }

        quizResultFeedbackLabel.text = localDatabase.quizDatabase[_quizIndex].answerFeedback;

        quizResultScreen.SetActive(true);
    }


    public void CloseQuizResultScreen()
    {
        quizResultScreen.SetActive(false);
    }

    // Eco footprint Info related
    public void OpenEcoFootprintInfo()
    {
        ecoFootprintInfoScreen.SetActive(true);
    }

    // Eco footprint dialogs Info   

    /// <summary>
    /// Opens the dialog that shows the ecofootprint info
    /// </summary>
    /// <param name="p_ecofootprint">1: Carbono, 2: pastoreo, 3: cultivos, 4: pesca, 5: forestal, 6: urbanismo</param>
    public void OpenEcoFootprintDialog(int p_ecofootprint)
    {
        ecoFootprintInfoDialogScreen.SetActive(true);

        ecoFootprintDialogGroupCarbono.SetActive(false);
        ecoFootprintDialogGroupPastoreo.SetActive(false);
        ecoFootprintDialogGroupCultivos.SetActive(false);
        ecoFootprintDialogGroupPesca.SetActive(false);
        ecoFootprintDialogGroupForestal.SetActive(false);
        ecoFootprintDialogGroupurbanismo.SetActive(false);

        switch (p_ecofootprint)
        {
            case 1:
                ecoFootprintDialogGroupCarbono.SetActive(true);
                break;
            case 2:
                ecoFootprintDialogGroupPastoreo.SetActive(true);
                break;
            case 3:
                ecoFootprintDialogGroupCultivos.SetActive(true);
                break;
            case 4:
                ecoFootprintDialogGroupPesca.SetActive(true);
                break;
            case 5:
                ecoFootprintDialogGroupForestal.SetActive(true);
                break;
            case 6:
                ecoFootprintDialogGroupurbanismo.SetActive(true);
                break;
        }
    }

    public void CloseEcoFootprintDialog()
    {
        ecoFootprintInfoDialogScreen.SetActive(false);
    }

    public void OpenTutorialScreen()
    {
        tutorialScreen.SetActive(true);
    }

    public void CloseTutorialScreen()
    {
        tutorialScreen.SetActive(false);
    }

    #endregion


    public void ModifyHealthBar()
    {
        healthBarRectTransform.sizeDelta = new Vector2(gameData.currentHealth * 5f, 0f);
        healthBarValueLabel.text = $"{gameData.currentHealth}%";
    }


    public void AchievementsButton()
    {
        //PlayServices.Instance.OpenAchievementsScreen();
    }

    public void LeaderboardButton()
    {
        //PlayServices.Instance.OpenLeaderboardScreen();
    }


    private IEnumerator<float> C_DecreaseHealth()
    {
        while (gameData.IsAlive)
        {
            gameData.ModifyMundiHealth(-1);
            yield return Timing.WaitForSeconds(30f);
        }

        yield break;
    }
}
