using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop_Manager : MonoBehaviour
{
    public TMP_Text[] shop_song_name;
    public Image[] shop_song_image;
   
    
    public GameObject panel;

    public int song_id;
    public TMP_Text Sname;
    public TMP_Text Aname;
    public TMP_Text Dif;
    public TMP_Text Gold;
    public Image song_image;
    public Sprite[] song_image_sprite;
    
    void Start()
    {
        for (int i = 0; i < Song.s_name.Length - 5; i++)
        {
            shop_song_name[i].text = Song.s_name[i + 5];
            shop_song_image[i].sprite = song_image_sprite[i];
        }
    }

    public void touched_song(int id)
    {
        song_id = id;
        Sname.text = Song.s_name[id];
        Aname.text = Song.artist[id];
        Dif.text = Song.difficulty[id];
        Gold.text = User.user.gold.ToString();
        song_image.sprite = song_image_sprite[id - 5];
        panel.gameObject.SetActive(true);
    }

    public void touched_buy_song()
    {
        Song.user_song[Song.user_song_count++] = song_id;
        User.user.gold -= 1000;
        panel.gameObject.SetActive(false);
    }
}
