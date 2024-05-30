using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using Unity.VisualScripting;

public class RankRegister : MonoBehaviour
{
    public void Process()
    {
        //곡으로 업데이트
        UpdateMyBestRankData();
    }

    private void UpdateMyBestRankData()
    {
        int newScore = BackendGameData.Instance.UserGameData.userScore;

        Backend.URank.User.GetMyRank(Constants.USER_RANK_UUID, callback => { 
            if(callback.IsSuccess())
            {
                try
                {
                    LitJson.JsonData rankDataJson = callback.FlattenRows();

                    if(rankDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터 미존재");
                    }
                    else
                    {
                        int bestScore = int.Parse(rankDataJson[0]["Score"].ToString());

                        if(newScore > bestScore)
                        {
                            UpdateMyRankData(newScore);

                            Debug.Log($"최고 점수 갱신 {bestScore} -> {newScore}");
                        }
                    }
                }
                catch(System.Exception e)
                {
                    Debug.LogException(e);
                }
            }
            else
            {
                if (callback.GetMessage().Contains("userRank"))
                {
                    UpdateMyRankData(newScore);

                    Debug.Log($"새로운 랭킹 데이터 생성 및 등록 : {callback}");
                }
            }
        });
    }

    private void UpdateMyRankData(int newScore)
    {
        string rowInDate = string.Empty;

        Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where(), callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError($"데이터 조회 중 문제 발생 : {callback}");
                return;
            }

            Debug.Log($"데이터 조회 성공 : {callback}");

            if (callback.FlattenRows().Count > 0)
            {
                rowInDate = callback.FlattenRows()[0]["inDate"].ToString();
            }
            else
            {
                Debug.LogError("데이터 미존재");
                return;
            }
        });

        Param param = new Param()
            {
                { "UserBestScore", newScore }
            };

        Backend.URank.User.UpdateUserScore(Constants.USER_RANK_UUID, Constants.USER_DATA_TABLE, rowInDate, param, callback =>
        {
            if (callback.IsSuccess())
            {
                Debug.Log($"랭킹 등록 성공 : {callback}");
            }
            else
            {
                Debug.LogError($"랭킹 등록 실패 : {callback}");
            }
        });
    }
}