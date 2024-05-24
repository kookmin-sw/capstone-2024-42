using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class ClearSceneController : MonoBehaviour
{
    public Text scoreText;
    public Text comboText;
    public bool clear = true;
    public int characterExp_game;
    public int count;

    void Start()
    {
        int finalScore = ScoreManager.Instance.Score;
        int maxCombo = ComboManager.Instance.MaxCombo;
        int upsongid;
        int charid_game;
        charid_game = User.user.character;
        characterExp_game = finalScore / 1000;

        scoreText.text = "Final Score: " + finalScore.ToString();
        comboText.text = "Max Combo: " + maxCombo.ToString();
       

        for(int i =0; i < BackendGameData.Instance.PlayerCharacterGameData.Count; i++)
        {
            if(BackendGameData.Instance.PlayerCharacterGameData[i].characterID == charid_game)
            {
                BackendGameData.Instance.PlayerCharacterGameData[i].characterExp += characterExp_game;
            }
        }
        BackendGameData.Instance.PlayerCharacterDataUpdate(charid_game);

        SongSelectionManager ssman = new SongSelectionManager();
        upsongid = ssman.ssongid;
        Debug.Log(BackendGameData.Instance.PlayerSongGameData.Count);
        for(int i = 0; i < BackendGameData.Instance.PlayerSongGameData.Count; i++)
        {
            Debug.Log(BackendGameData.Instance.PlayerSongGameData[i].songID);

            if(BackendGameData.Instance.PlayerSongGameData[i].songID == upsongid)
            {
                BackendGameData.Instance.PlayerSongGameData[i].score = finalScore;
                Debug.Log("X");
                BackendGameData.Instance.PlayerSongGameData[i].combo = maxCombo;
                Debug.Log("O");
                BackendGameData.Instance.PlayerSongGameData[i].clear = true;

                break;
            }
        }

        BackendGameData.Instance.PlayerSongDataUpdate(upsongid);


    }
}