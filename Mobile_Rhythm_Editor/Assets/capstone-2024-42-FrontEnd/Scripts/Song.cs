using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public static string[] s_name = {"AAA", "BBB", "CCC", "DDD", "EEE", "FFF", "GGG", "HHH", "III", "JJJ", "KKK", "LLL"};
    public static string[] artist = { "kang", "kim", "lee", "choi", "soe", "han", "jung", "yang", "lim", "ko", "ku", "song"};
    public static string[] difficulty = { "5", "3", "1", "4", "6", "2", "3", "1", "4", "2", "5", "1"};

    public static int user_song_count = 5; // 유저가 보유한 곡 개수
    public static int[] user_song = {0, 1, 2, 3, 4, 0, 0, 0, 0, 0}; // 유저가 보유한 곡 리스트
    public int[] clear; // 곡 클리어 리스트 (1이면 클리어)
    public static int[] score = { 100000, 0, 99999, 12345, 0, 0, 0, 0, 0, 0, 0, 0}; // 클리어 한 곡의 최대 점수
    public int[] combo; // 클리어 한 곡의 최대 콤보 수

    void Start()
    {
        for(int i = 0; i < 12 ; i++)
        {
            Debug.Log(BackendChartData.songChart[i].songName);
            s_name[i] = BackendChartData.songChart[i].songName;
        }
    }
    public void user_song_sort()
    {
        Array.Sort(user_song, 0, user_song_count - 1);
    }
}
