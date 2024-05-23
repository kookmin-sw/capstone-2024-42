using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Uer_Info_Manager : MonoBehaviour
{
    public TMP_Text Text_uid;
    public TMP_Text Text_user_name;
    public TMP_Text Text_user_level;
    public TMP_Text Text_user_ranking;
    public TMP_Text Text_user_score;
    public TMP_Text Text_gold;

    public Image daepyo_character_image;
    public Sprite[] character_image_sprite;

    void Start()
    {
        Text_uid.text = User.user.uid.ToString();
        Text_user_name.text = User.user.name.ToString();
        Text_user_level.text = User.user.level.ToString();
        Text_user_ranking.text = User.user.ranking.ToString();
        Text_user_score.text = User.user.score.ToString();

        daepyo_character_image.sprite = character_image_sprite[User.user.character];
    }
}
