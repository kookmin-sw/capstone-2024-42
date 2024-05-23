using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_info
{
    //BackendGameData.Instance.User-.
    public int uid;      // 유저 UID
    public string name; // 유저 닉네임
    public int character = 0;   // 유저가 선택한 현재 캐릭터 id
    public int level = 1;       // 유저 레벨
    public int exp = 80;        // 유저 경험치
    public int gold;    // 유저가 보유한 재화
    public int ranking;    // 유저 랭킹
    public int score;     // 유저가 클리어한 곡 점수의 합
}

 public class User : MonoBehaviour
{
    public static User_info user = new User_info();
    
    public void load_user_data()
    {
        user.uid = 1000;
        user.name = "min";
        user.character = 0;
        user.level = 1;
        user.exp = 80;
        user.gold = 10000;
        user.ranking = 10;
        user.score = 999;
    }
}
