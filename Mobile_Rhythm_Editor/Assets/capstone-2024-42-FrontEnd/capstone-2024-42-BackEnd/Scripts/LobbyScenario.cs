using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScenario : MonoBehaviour
{
    [SerializeField]
    private UserInfo user;

    private void Awake()
    {
        user.GetUserInfoFromBackend();
    }

    private void Start()
    {
        RankRegister.Instance.LoadTopRankData(10);

        RankRegister.Instance.LoadNearbyRankData(5);

        BackendGameData.Instance.UserDataLoad();
        
        BackendGameData.Instance.PlayerSongDataLoad();

        BackendGameData.Instance.PlayerCharacterDataLoad();

        BackendGameData.Instance.PlayerSongDataSort();

        BackendGameData.Instance.PlayerCharacterDataSort();

        

        //        BackendGameData.Instance.UserGameData.nickname;

        //        BackendGameData.Instance.PlayerCharacterGameData.Count();

    }
}
