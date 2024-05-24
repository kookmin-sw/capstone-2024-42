using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Change_Main_Scene()
    {
        SceneManager.LoadScene("Main_Scene");
    }

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

    public void Change_User_Information_Scene()
    {
        SceneManager.LoadScene("User_Information_Scene");
    }

    public void Change_Shop_Scene()
    {
        SceneManager.LoadScene("Shop_Scene");
    }

    public void Change_Ranking_Scene()
    {
        SceneManager.LoadScene("Ranking_Scene");
    }

    public void Change_Ingame_Scene()
    {
        SceneManager.LoadScene("Ingame");
    }
}
