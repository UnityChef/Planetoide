using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{

    public void AchievementsButton()
    {
        GPGSManager.Instance.OpenAchievementsScreen();
    }

    public void LeaderboardButton()
    {
        GPGSManager.Instance.OpenLeaderboardScreen();
    }

}
