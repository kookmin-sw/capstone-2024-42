using BackEnd;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCharacterGameData
{
    public string ownerIndate = Backend.UserInDate;
    public int characterID;
    public int characterLevel;
    public int characterExp;
    public int count;

    public PlayerCharacterGameData()
    {

    }

    private void Reset()
    {
        characterID = 0;
        characterLevel = 1;
        characterExp = 0;
        count = 0;
    }

    public Param ToParam()
    {
        Param param = new Param();
        param.Add("CharacterLevel", characterLevel);
        param.Add("CharacterExp", characterExp);
        param.Add("CharacterID", characterID);
        param.Add("Count", count);

        return param;
    }

    public void Json_write(LitJson.JsonData gameDataJson, int ID)
    {
        characterLevel = int.Parse(gameDataJson[ID]["CharacterLevel"].ToString());
        characterExp = int.Parse(gameDataJson[ID]["CharacterExp"].ToString());
        characterID = int.Parse(gameDataJson[ID]["CharacterID"].ToString());
        count = int.Parse(gameDataJson[ID]["Count"].ToString());
    }
}