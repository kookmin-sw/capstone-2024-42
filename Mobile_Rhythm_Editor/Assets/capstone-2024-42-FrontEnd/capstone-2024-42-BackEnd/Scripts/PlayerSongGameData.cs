using BackEnd;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerSongGameData
{
    public string ownerIndate = Backend.UserInDate;
    public int songID;
    public int score;
    public int combo;
    public bool clear;

    public PlayerSongGameData()
    {

    }

    private void Reset()
    {
        songID = 0;
        score = 0;
        combo = 0;
        clear = false;
    }

    public Param ToParam()
    {
        Param param = new Param();
        param.Add("SongID", songID);
        param.Add("Score", score);
        param.Add("Combo", combo);
        param.Add("Clear", clear);

        return param;
    }

    public void Json_write(LitJson.JsonData gameDataJson, int ID)
    {   
        songID = int.Parse(gameDataJson[ID]["SongID"].ToString());
        score = int.Parse(gameDataJson[ID]["Score"].ToString());
        combo = int.Parse(gameDataJson[ID]["Combo"].ToString());
        clear = bool.Parse(gameDataJson[ID]["Clear"].ToString());
    }
}