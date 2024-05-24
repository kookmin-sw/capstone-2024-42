using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame_song_image : MonoBehaviour
{
    public static Ingame_song_image Instance { get; private set; }
    public SpriteRenderer Ingame_song_imagesprite;
    public Sprite Ingame_song_0;
    public Sprite Ingame_song_1;
    public Sprite Ingame_song_2;
    public Sprite Ingame_song_3;
    public Sprite Ingame_song_4;
    public Sprite Ingame_song_5;
    public Sprite Ingame_song_6;
    public Sprite Ingame_song_7;
    public Sprite Ingame_song_8;
    public Sprite Ingame_song_9;
    int wantid;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환시 객체 유지
        }
        else
        {
            Destroy(gameObject);
        } 
    }
    void Start()
    {
        wantid = SongSelectionManager.Instance.ssongid;
        Change_Song_Sprite(wantid);
    }

    public void Change_Song_Sprite(int a)
    {
        if(a == 0)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_0;
        }
        else if(a == 1)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_1;
        }
        else if(a == 2)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_2;
        }
        else if(a == 3)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_3;
        }
        else if(a == 4)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_4;
        }
        else if(a == 5)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_5;
        }
        else if(a == 6)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_6;
        }
        else if(a == 7)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_7;
        }
        else if(a == 8)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_8;
        }
        else if(a == 9)
        {
            Ingame_song_imagesprite.sprite = Ingame_song_9;
        }                                                        
    }
}
