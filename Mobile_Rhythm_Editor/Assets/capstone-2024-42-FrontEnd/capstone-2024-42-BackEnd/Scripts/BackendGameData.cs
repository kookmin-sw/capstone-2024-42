using UnityEngine;
using BackEnd;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using JetBrains.Annotations;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;
using System;

public class BackendGameData
{
    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private static BackendGameData instance = null;
    public static BackendGameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendGameData();
            }

            return instance;
        }
    }

    private UserGameData userGameData = new UserGameData();
    private List<PlayerSongGameData> playerSongGameData = new List<PlayerSongGameData>();
    private List<PlayerCharacterGameData> playerCharacterGameData = new List<PlayerCharacterGameData>();

    public UserGameData UserGameData => userGameData;
    public List<PlayerSongGameData> PlayerSongGameData => playerSongGameData;
    public List<PlayerCharacterGameData> PlayerCharacterGameData => playerCharacterGameData;

    private string UserDataRowInDate = string.Empty;
    private string SongDataRowInDate = string.Empty;
    private string CharacterDataRowInDate = string.Empty;

    public void UserDataInsert()
    {
        UserGameData gameData = new UserGameData();

        Param param = gameData.ToParam();

        Backend.GameData.Insert("User", param, callback =>
        {
            if (callback.IsSuccess())
            {
                UserDataRowInDate = callback.GetInDate();
                Debug.Log("내 User Info의 indate : " + UserDataRowInDate);
            }
            else
            {
                Debug.LogError("유저 게임 정보 삽입 실패 : " + callback.ToString());
                if (callback.IsClientRequestFailError())
                {
                    Debug.Log("네트워크가 일시적으로 끊어졌을 경우");
//                    UserDataInsert();
                }
            }
        });
    }

    public void UserDataLoad()
    {
        Backend.GameData.GetMyData("User", new Where(), call_back =>
        {
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
                        UserDataRowInDate = gameDataJson[0]["inDate"].ToString();
                        userGameData.Json_write(gameDataJson);

                        onGameDataLoadEvent?.Invoke();

                        Debug.Log($"유저 정보 데이터 로드 성공");
                    }
                }
                catch (System.Exception e)
                {
                    userGameData.Reset();
                    Debug.LogError(e);
                }
            }
            else
            {
                Debug.Log($"유저 정보 데이터 로드 실패");
            }
        });
    }

    public void UserDataUpdate(UnityAction action=null)
    {
        if(userGameData == null)
        {
            Debug.Log("Insert or Load 먼저 필요함");
            return;
        }

        Param param = userGameData.ToParam();

        if(string.IsNullOrEmpty(UserDataRowInDate))
        {
            Debug.LogError("유저의 inDate 정보가 X");
        }
        else
        {
            Debug.Log($"{UserDataRowInDate}의 게임 정보 데이터 수정 요청");

            Backend.GameData.UpdateV2("User", UserDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if(callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정 성공 : {callback}");

                    action?.Invoke();

//                    BackendGameData.Instance.UserDataUpdate();
                }
                else
                {
                    Debug.Log($"게임 정보 데이터 수정 실패 : {callback}");
                    if (callback.IsClientRequestFailError())
                    {
                        Debug.Log("네트워크가 일시적으로 끊어졌을 경우");
                        UserDataUpdate(action);
                    }
                }
            });
        }
    }

    public void PlayerSongDataInsert()
    {
        PlayerSongGameData gameData = new PlayerSongGameData();
        for (int i = 0; i < Constants.SONG_NUMBER; i++)
        {
            gameData.songID = i;
            Param param = gameData.ToParam();

            Backend.GameData.Insert("PlayerSong", param, callback =>
            {
                if (callback.IsSuccess())
                {
                    SongDataRowInDate = callback.GetInDate();
                    Debug.Log("내 Player Song Info의 indate : " + SongDataRowInDate);
                }
                else
                {
                    Debug.LogError("곡 게임 정보 삽입 실패 : " + callback.ToString());
                    if (callback.IsClientRequestFailError())
                    {
                        Debug.Log("네트워크가 일시적으로 끊어졌을 경우");
                        //                    UserDataInsert();
                    }
                }
            });
        }
    }

    public void PlayerSongDataLoad()
    {
        Backend.GameData.GetMyData("PlayerSong", new Where(), Constants.SONG_NUMBER, call_back =>
        {
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
                        for (int i = 0; i < Constants.SONG_NUMBER; i++)
                        {
                            SongDataRowInDate = gameDataJson[i]["inDate"].ToString();
                            PlayerSongGameData SongDataRow = new PlayerSongGameData();
                            SongDataRow.Json_write(gameDataJson, i);
                            playerSongGameData.Add(SongDataRow);

                            onGameDataLoadEvent?.Invoke();

                            Debug.Log($"{i}번째 보유 곡 정보 데이터 로드 성공");
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
                Debug.Log($"보유 곡 정보 데이터 로드 실패");
            }
        });
    }

    public void PlayerSongDataUpdate(int songID, UnityAction action = null)
    {
        if (playerSongGameData == null)
        {
            Debug.Log("Insert or Load 먼저 필요함");
            return;
        }

        Param param = new Param();

        for (int i = 0; i < playerSongGameData.Count; i++)
        {
            if (playerSongGameData[i].songID == songID)
            {
                param = playerSongGameData[i].ToParam();
            }
        }

        if (string.IsNullOrEmpty(SongDataRowInDate))
        {
            Debug.LogError("보유 곡의 inDate 정보가 X");
        }
        else
        {
            Debug.Log($"{SongDataRowInDate}의 보유 곡 정보 데이터 수정 요청");

            Backend.GameData.UpdateV2("PlayerSong", SongDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"보유 곡 정보 데이터 수정 성공 : {callback}");

                    action?.Invoke();
                }
                else
                {
                    Debug.Log($"보유 곡 정보 데이터 수정 실패 : {callback}");
                    if (callback.IsClientRequestFailError())
                    {
                        Debug.Log("네트워크가 일시적으로 끊어졌을 경우");
                        PlayerSongDataUpdate(songID, action);
                    }
                }
            });
        }
    }

    public void PlayerSongDataSort()
    {
        playerSongGameData.Sort((x, y) =>
        {
            return x.songID.CompareTo(y.songID);
        });

        foreach (PlayerSongGameData data in playerSongGameData)
        {
            Console.WriteLine($"SID : {data.songID}");
        }
    }

    public void PlayerCharacterDataInsert()
    {
        PlayerCharacterGameData gameData = new PlayerCharacterGameData();
        for (int i = 0; i < Constants.CHARACTER_NUMBER; i++)
        {
            gameData.characterID = i;
            Param param = gameData.ToParam();

            Backend.GameData.Insert("PlayerCharacter", param, callback =>
            {
                if (callback.IsSuccess())
                {
                    CharacterDataRowInDate = callback.GetInDate();
                    Debug.Log("내 Player Character Info의 indate : " + CharacterDataRowInDate);
                }
                else
                {
                    Debug.LogError("보유 캐릭터 게임 정보 삽입 실패 : " + callback.ToString());
                    if (callback.IsClientRequestFailError())
                    {
                        Debug.Log("네트워크가 일시적으로 끊어졌을 경우");
                        //                    UserDataInsert();
                    }
                }
            });
        }
    }
 
    public void PlayerCharacterDataLoad()
    {
        Backend.GameData.GetMyData("PlayerCharacter", new Where(), Constants.CHARACTER_NUMBER, call_back =>
        {
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
                        for (int i = 0; i < Constants.CHARACTER_NUMBER; i++)
                        {
                            CharacterDataRowInDate = gameDataJson[i]["inDate"].ToString();
                            PlayerCharacterGameData CharacterDataRow = new PlayerCharacterGameData();
                            CharacterDataRow.Json_write(gameDataJson, i);
                            playerCharacterGameData.Add(CharacterDataRow);

                            onGameDataLoadEvent?.Invoke();

                            Debug.Log($"{i}번째 보유 캐릭터 정보 데이터 로드 성공");
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
                Debug.Log($"보유 캐릭터 정보 데이터 로드 실패");
            }
        });
    }

    public void PlayerCharacterDataUpdate(int characterID, UnityAction action = null)
    {
        if (playerCharacterGameData == null)
        {
            Debug.Log("Insert or Load 먼저 필요함");
            return;
        }

        Param param = new Param();

        for (int i = 0; i < playerCharacterGameData.Count; i++)
        {
            if (playerCharacterGameData[i].characterID == characterID)
            {
                param = playerCharacterGameData[i].ToParam();
            }
        }

        if (string.IsNullOrEmpty(CharacterDataRowInDate))
        {
            Debug.LogError("보유 캐릭터의 inDate 정보가 X");
        }
        else
        {
            Debug.Log($"{CharacterDataRowInDate}의 보유 캐릭터 정보 데이터 수정 요청");

            Backend.GameData.UpdateV2("PlayerCharacter", CharacterDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"보유 캐릭터 정보 데이터 수정 성공 : {callback}");

                    action?.Invoke();
                }
                else
                {
                    Debug.Log($"보유 캐릭터 정보 데이터 수정 실패 : {callback}");
                    if (callback.IsClientRequestFailError())
                    {
                        Debug.Log("네트워크가 일시적으로 끊어졌을 경우");
                        PlayerCharacterDataUpdate(characterID, action);
                    }
                }
            });
        }
    }

    public void PlayerCharacterDataSort()
    {
        playerCharacterGameData.Sort((x, y) =>
        {
            return x.characterID.CompareTo(y.characterID);
        });

        foreach (PlayerCharacterGameData data in playerCharacterGameData)
        {
            Console.WriteLine($"CID : {data.characterID}");
        }
    }
}