using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using Unity.VisualScripting;
using BackEnd.Game.Rank;

public class Rank
{
    public string gamerInDate;
    public string nickname;
    public int rank; 
    public int score;
}

public class RankRegister : MonoBehaviour
{
    public List<Rank> TopRankList;
    public List<Rank> NearbyRankList;

    private void Start()
    {
        TopRankList = new List<Rank>();
        NearbyRankList = new List<Rank>();
    }

    public List<Rank> GetTopRankData(int num)
    {
        TopRankList.Clear();
        List<Rank> rank_list = new List<Rank>();

        Backend.URank.User.GetRankList(Constants.USER_RANK_UUID, num, call_back => {
            if (call_back.IsSuccess())
            {
                Debug.Log(call_back.ToString());

                try
                {
                    LitJson.JsonData gameDataJson = call_back.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.Log("데이터가 존재하지 않습니다");
                        return;
                    }
                    else
                    {
                        Rank rowInRank = new Rank();

                        for (int i = 0; i < num; i++)
                        {
                            rowInRank.rank = int.Parse(gameDataJson[i]["rank"].ToString());
                            rowInRank.score = int.Parse(gameDataJson[i]["score"].ToString());
                            rowInRank.nickname = gameDataJson[i]["nickname"].ToString();
                            rowInRank.gamerInDate = gameDataJson[i]["gamerInDate"].ToString();
                            TopRankList.Add(rowInRank);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            else
            {
                Debug.LogError("랭크 불러오기 오류");
            }
        });

        return rank_list;
    }
    public Rank GetMyRankData()
    {
        Rank rank = new Rank();

        BackendReturnObject MyRank = Backend.URank.User.GetMyRank(Constants.USER_RANK_UUID);
        LitJson.JsonData gameDataJson = MyRank.FlattenRows();
        rank.rank = int.Parse(gameDataJson[0]["rank"].ToString());
        rank.score = int.Parse(gameDataJson[0]["score"].ToString());
        rank.nickname = gameDataJson[0]["nickname"].ToString();
        rank.gamerInDate = gameDataJson[0]["gamerInDate"].ToString();

        return rank;
    }

    public void LoadNearbyRankData(int num) //num = 주위 몇개의 랭크까지 불러올것인가
    {
        NearbyRankList.Clear();
        int myRank = GetMyRankData().rank; //내 현재 랭크
        int count = 0;

        Backend.URank.User.GetMyRank(Constants.USER_RANK_UUID, num, call_back =>
        {
            if(call_back.IsSuccess())
            {
                Debug.Log(call_back.ToString());

                try
                {
                    LitJson.JsonData gameDataJson = call_back.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.Log("데이터가 존재하지 않습니다");
                        return;
                    }
                    else
                    {
                        if(myRank < num)
                        {
                            count = num - myRank;
                        }

                        Rank rowInRank = new Rank();

                        for (int i = 0; i < gameDataJson.Count - count; i++)
                        {
                            rowInRank.rank = int.Parse(gameDataJson[i]["rank"].ToString());
                            rowInRank.score = int.Parse(gameDataJson[i]["score"].ToString());
                            rowInRank.nickname = gameDataJson[i]["nickname"].ToString();
                            rowInRank.gamerInDate = gameDataJson[i]["gamerInDate"].ToString();
                            NearbyRankList.Add(rowInRank);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            else
            {
                Debug.LogError("랭크 불러오기 오류");
            }
        });
    }

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