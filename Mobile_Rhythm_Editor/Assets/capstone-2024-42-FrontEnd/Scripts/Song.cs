using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public static string[] s_name = {"AAA", "BBB", "CCC", "DDD", "EEE", "FFF", "GGG", "HHH", "III", "JJJ", "KKK", "LLL"};
    public static string[] artist = { "kang", "kim", "lee", "choi", "soe", "han", "jung", "yang", "lim", "ko", "ku", "song"};
    public static string[] difficulty = { "5", "3", "1", "4", "6", "2", "3", "1", "4", "2", "5", "1"};

    public static int user_song_count = 5; // ������ ������ �� ����
    public static int[] user_song = {0, 1, 2, 3, 4, 0, 0, 0, 0, 0}; // ������ ������ �� ����Ʈ
    public int[] clear; // �� Ŭ���� ����Ʈ (1�̸� Ŭ����)
    public static int[] score = { 100000, 0, 99999, 12345, 0, 0, 0, 0, 0, 0, 0, 0}; // Ŭ���� �� ���� �ִ� ����
    public int[] combo; // Ŭ���� �� ���� �ִ� �޺� ��

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
