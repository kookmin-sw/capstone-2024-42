using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;

public class UserGameData
{
    public string nickname = Backend.UserNickName;
    public string ownerIndate = Backend.UserInDate;
    public int level;
    public int userExp;
    public int userScore;
    public int money;
    public int selectCharacter_num;

    public UserGameData()
    {

    }
    public void Reset()
    {
        level = 1;
        userExp = 0;
        userScore = 0;
        money = 0;
        selectCharacter_num = 0;
    }

    public Param ToParam()
    {
        Param param = new Param();
        param.Add("Level", level);
        param.Add("Money", money);
        param.Add("SelectCharacter", selectCharacter_num);
        param.Add("UserExp", userExp);
        param.Add("UserScore", userScore);

        return param;
    }

    public void Json_write(LitJson.JsonData gameDataJson)
    {
        level = int.Parse(gameDataJson[0]["Level"].ToString());
        money = int.Parse(gameDataJson[0]["Money"].ToString());
        userExp = int.Parse(gameDataJson[0]["UserExp"].ToString());
        userScore = int.Parse(gameDataJson[0]["UserScore"].ToString());
        selectCharacter_num = int.Parse(gameDataJson[0]["SelectCharacter"].ToString());
    }
}
