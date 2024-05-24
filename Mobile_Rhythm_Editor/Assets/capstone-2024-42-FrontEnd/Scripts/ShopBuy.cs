using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBuy : MonoBehaviour
{
    public GameObject panel;

    public int song_id;
    public int original_song_count = 5;

    public TMP_Text Sname;
    public TMP_Text Aname;
    public TMP_Text Dif;

    public void touched_song(int id)
    {
        song_id = id;
        Sname.text = Song.s_name[id];
        Aname.text = Song.artist[id];
        Dif.text = Song.difficulty[id];
        panel.gameObject.SetActive(true);
    }

    public void touched_buy_song()
    {
        Song.user_song[Song.user_song_count++] = song_id;
        panel.gameObject.SetActive(false);
    }
}
