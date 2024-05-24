using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Test : MonoBehaviour
{
   public void Checking()
    {
        for (int i = 0; i < Constants.SONG_NUMBER; i++)
        {
            Debug.Log(BackendGameData.Instance.PlayerSongGameData[i].songID);
        }

        for (int i = 0; i < Constants.CHARACTER_NUMBER; i++)
        {
            Debug.Log(BackendGameData.Instance.PlayerCharacterGameData[i].characterID);
        }

        for (int i = 0; i < Constants.SONG_NUMBER; i++)
        {
            if (BackendGameData.Instance.PlayerSongGameData[i].songID == 0)
            {
                BackendGameData.Instance.PlayerSongGameData[i].score = 100;
            }
        }

        for (int i = 0; i < Constants.CHARACTER_NUMBER; i++)
        {
            if (BackendGameData.Instance.PlayerCharacterGameData[i].characterID == 0)
            {
                BackendGameData.Instance.PlayerCharacterGameData[0].characterExp = 100;
            }
        }

        BackendGameData.Instance.UserGameData.userExp = 100;

        BackendGameData.Instance.UserDataUpdate();
        BackendGameData.Instance.PlayerCharacterDataUpdate(0);
        BackendGameData.Instance.PlayerSongDataUpdate(0);
    }
}
