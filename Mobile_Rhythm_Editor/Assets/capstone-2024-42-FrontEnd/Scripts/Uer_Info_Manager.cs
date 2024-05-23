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

    public Slider user_exp_bar;
    public GameObject gauge;

    void Start()
    {
        Text_uid.text = User.user.uid.ToString();
        Text_user_name.text = BackendGameData.Instance.UserGameData.nickname;
        Text_user_level.text = BackendGameData.Instance.UserGameData.level.ToString();
        Text_user_ranking.text = User.user.ranking.ToString();
        Text_user_score.text = BackendGameData.Instance.UserGameData.userScore.ToString();

        daepyo_character_image.sprite = character_image_sprite[User.user.character];

        int exp = BackendGameData.Instance.UserGameData.userExp;
        if (exp == 0) gauge.gameObject.SetActive(false);
        else
        {
            gauge.gameObject.SetActive(true);
            user_exp_bar.value = (float)exp / (float)BackendChartData.levelChart[BackendGameData.Instance.UserGameData.level - 1].maxExperience;
        }
    }
}
