using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop_Manager : MonoBehaviour
{
    public TMP_Text[] shop_song_name;
    public Image[] shop_song_image;
    public TMP_Text shop_user_gold;

    
    public GameObject panel;

    public int song_id;
    public TMP_Text Sname;
    public TMP_Text Aname;
    public TMP_Text Dif;
    public TMP_Text Gold;
    public TMP_Text Cant_Buy;
    public Image song_image;
    public Sprite[] song_image_sprite;
    
    void Start()
    {
        shop_user_gold.text = BackendGameData.Instance.UserGameData.money.ToString();
        for (int i = 0; i < 5; i++)
        {
            shop_song_name[i].text = BackendChartData.songChart[i + 7].songName;
            //shop_song_image[i].sprite = song_image_sprite[i];
        }
    }

    public void touched_song(int id)
    {
        song_id = id;
        Sname.text = BackendChartData.songChart[id].songName;
        Aname.text = BackendChartData.songChart[id].artist;
        Dif.text = BackendChartData.songChart[id].songLevel.ToString();
        //song_image.sprite = song_image_sprite[id - 7];

        Gold.text = BackendGameData.Instance.UserGameData.money.ToString();
        if (BackendGameData.Instance.UserGameData.money < 1000)
        {
            Cant_Buy.gameObject.SetActive(true);
        }
        else Cant_Buy.gameObject.SetActive(false);

        panel.gameObject.SetActive(true);
    }

    public void touched_buy_song()
    {
        //Song.user_song[Song.user_song_count++] = song_id;
        User.user.gold -= 1000;
        panel.gameObject.SetActive(false);
    }
}
