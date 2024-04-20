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
    public void Change_Ingame()
    {
        SceneManager.LoadScene("Ingame");
    }
    public void Change_Start_Scene()
    {
        SceneManager.LoadScene("Start_Scene");
    }
    public void Change_logo_Scene()
    {
        SceneManager.LoadScene("LogoScene");
    }
}
