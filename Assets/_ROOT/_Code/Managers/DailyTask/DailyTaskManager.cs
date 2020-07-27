using GooglePlayGames.BasicApi.Multiplayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoMundi.Managers
{
    public class DailyTaskManager : MonoBehaviour
    {


        
        public List<DailyTaskData> dailyTaskList;

        private int _currentDailyTaskIndex;

        public void SetRandomTask()
        {
            if (PlayerPrefs.HasKey("DailyTaskIndex"))
            {
                _currentDailyTaskIndex = PlayerPrefs.GetInt("DailyTaskIndex");
                return;
            }


        }
    }

    [Serializable]
    public class DailyTaskData
    {
        public E_ZoneType taskGroup;
        public int taskCurrentValue;
        public int taskGoalValue;

        public void ModifyTaskValue()
        {
            taskCurrentValue++;

            if (taskCurrentValue > taskGoalValue)
                taskCurrentValue = taskGoalValue;
        }

        public string TaskValuesString
        {
            get { return $"{taskCurrentValue} / {taskGoalValue}"; }
        }
    }
}
