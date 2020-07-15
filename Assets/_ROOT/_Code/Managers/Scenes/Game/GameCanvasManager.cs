using EcoMundi.Data;
using EcoMundi.Managers;
using MEC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameCanvasManager : MonoBehaviour
{
    [Header("Data")]
    public GameLocalDatabase localDatabase;
    private int _randomQuizIndex;

    [Header("QuizQuestionScreen")]
    public GameObject quizQuestionScreen;
    public TMP_Text quizQuestionLabel;
    [Space]
    public List<GameObject> answerButtonList;
    [Space]
    public List<TMP_Text> answerButtonLabelList;
    
    [Header("QuizResultScreen")]
    public GameObject quizResultScreen;
    [Space]
    public TMP_Text quizResultFeedbackLabel;

    [Header("Health Bar")]
    public RectTransform healthBarRectTransform;
    public TMP_Text healthBarValueLabel;



    private void Start()
    {
        Timing.RunCoroutine(C_DecreaseHealth());
    }

    #region [-----     SCREENS     -----]

    public void ShowQuizQuestionScreen()
    {
        _randomQuizIndex = Random.Range(0, localDatabase.quizDatabase.Count);

        quizQuestionLabel.text = localDatabase.quizDatabase[_randomQuizIndex].question;

        foreach (GameObject button in answerButtonList)
            button.SetActive(false);

        for (int i = 0; i < localDatabase.quizDatabase[_randomQuizIndex].answerOptions.Count; i++)
        {
            answerButtonList[i].SetActive(true);
            answerButtonLabelList[i].text = localDatabase.quizDatabase[_randomQuizIndex].answerOptions[i].answer;
        }
       

        quizQuestionScreen.SetActive(true);


    }

    public void ShowQuizResultScreen() 
    {
        quizResultFeedbackLabel.text = localDatabase.quizDatabase[_randomQuizIndex].answerFeedback;

        MundiManager.Instance.ModifyMundiHealth(1);
        ModifyHealthBar(MundiManager.Instance.data.currentHealth);


        quizQuestionScreen.SetActive(false);
        quizResultScreen.SetActive(true);

    }

    public void CloseQuizResultScreen()
    {
        quizResultScreen.SetActive(false);
    }

    #endregion

    public void ModifyHealthBar(int p_currentHealth)
    {
        healthBarRectTransform.sizeDelta = new Vector2(p_currentHealth * 5f, 0f);

        healthBarValueLabel.text = $"{p_currentHealth}%";
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
        while(MundiManager.Instance.data.currentHealth > 0)
        {
            MundiManager.Instance.ModifyMundiHealth(-1);
            ModifyHealthBar(MundiManager.Instance.data.currentHealth);

            yield return Timing.WaitForSeconds(4f);
        }

        yield break;
    }
}
