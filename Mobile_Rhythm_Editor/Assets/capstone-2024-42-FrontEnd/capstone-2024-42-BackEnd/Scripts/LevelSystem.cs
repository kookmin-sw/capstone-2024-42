using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelSystem : MonoBehaviour {
    private readonly int increaseExperience = 25;

    public void User_Process()
    {
        int currentlevel = BackendGameData.Instance.UserGameData.level;

        BackendGameData.Instance.UserGameData.userExp += increaseExperience;
    
        if(BackendGameData.Instance.UserGameData.userExp >= BackendChartData.levelChart[currentlevel - 1].maxExperience && 
            BackendChartData.levelChart.Count >  currentlevel)
        {
            BackendGameData.Instance.UserGameData.money += BackendChartData.levelChart[currentlevel - 1].rewardGold;
            BackendGameData.Instance.UserGameData.userExp = 0;
            BackendGameData.Instance.UserGameData.level++;
        }

        BackendGameData.Instance.UserDataUpdate();
    }

    public void Character_Process(int character_ID)
    {
        int currentlevel = 0;

        if(character_ID < 0)
        {
            Debug.Log("레벨 - 캐릭터 ID 값이 잘못되었습니다.");
        }

        BackendGameData.Instance.PlayerCharacterGameData[character_ID].characterExp += increaseExperience;

        if (BackendGameData.Instance.PlayerCharacterGameData[character_ID].characterExp >= BackendChartData.levelChart[currentlevel - 1].maxExperience &&
            BackendChartData.levelChart.Count > currentlevel)
        {
            BackendGameData.Instance.UserGameData.money += BackendChartData.levelChart[currentlevel - 1].rewardGold;
            BackendGameData.Instance.PlayerCharacterGameData[character_ID].characterExp = 0;
            BackendGameData.Instance.PlayerCharacterGameData[character_ID].characterLevel++;
        }

        BackendGameData.Instance.UserDataUpdate();

    }
}