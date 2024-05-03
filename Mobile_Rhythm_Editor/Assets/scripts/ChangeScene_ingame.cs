using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_ingame : MonoBehaviour
{
    public void Change_Character_Select_Scene()
    {
        SceneManager.LoadScene("Character_Select_Scene");
    }

    public void Change_Song_Select_Scene()
    {
        SceneManager.LoadScene("Song_Select_Scene");
    }

    public void Change_Game_Scene()
    {
        SceneManager.LoadScene("Game_Scene");
    }
}
