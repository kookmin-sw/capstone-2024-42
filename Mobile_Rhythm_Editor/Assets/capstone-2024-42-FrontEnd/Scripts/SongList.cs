using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongList : MonoBehaviour
{
    private int Song_Count = 12;
    public GameObject[] song_object = new GameObject[10];

    public TMP_Text[] list_name;
    public TMP_Text[] list_difficulty;

    void Start() // �� ���� â���� �ڽ��� ������ ��鸸 �����ִ� ���
    {
        /*int a;
        for (int i = 0; i < Song.user_song.Length ; i++)
        {
            a = Song.user_song[i];

            list_name[a].text = Song.s_name[a];
            list_difficulty[a].text = Song.difficulty[a];
            song_object[a].gameObject.SetActive(true);

        }*/

        for (int i = 0; i < BackendChartData.songChart.Count ; i++)
        {
            list_name[i].text = BackendChartData.songChart[i].songName;
            list_difficulty[i].text = BackendChartData.songChart[i].songLevel.ToString();
        }
    }
}
