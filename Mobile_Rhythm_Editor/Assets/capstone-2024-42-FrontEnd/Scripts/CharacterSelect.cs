using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public GameObject panel;

    public TMP_Text Cname;
    public TMP_Text Cteam;
    public TMP_Text Cprofile;

    public Slider character_exp_bar;
    public GameObject gauge;
    public TMP_Text Clevel;
    public TMP_Text Cexp;

    public Image image;
    public Sprite[] sprites;

    public int characterId;

    public void cliked_character(int id)
    {
        Debug.Log(Character.character_name[id]);
        Debug.Log(Character.character_level);
        Debug.Log(Character.character_exp);
        characterId = id;
        Cname.text = Character.character_name[id];
        Cteam.text = Character.character_team[id];
        Cprofile.text = Character.character_profile[id];
        image.sprite = sprites[id];

        // 캐릭터 경험치 바 활성 스크립트
        int exp = Character.character_exp;
        int max_exp = 100;
        
        Clevel.text = "Lv " + Character.character_level.ToString();
        Cexp.text = "(" + exp.ToString() +" / " + max_exp.ToString() +")";

        if (exp == 0) gauge.gameObject.SetActive(false);
        else
        {
            gauge.gameObject.SetActive(true);
            character_exp_bar.value = (float)exp / (float)max_exp;
        }
        panel.gameObject.SetActive(true);
        CharacterSpriteManager.Instance.character_select_ID = id;
    }

    public void touch_character_select_button()
    {
        User.user.character = characterId;
    }
}

