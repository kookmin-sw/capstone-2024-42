using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SongSelectionManager : MonoBehaviour
{
    public static SongSelectionManager Instance { get; private set; }

    public string SelectedSongJsonPath { get; private set; }
    public string SelectedSongID { get; private set; } 
    public int ssongid;
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

    // 곡 ID를 기반으로 곡을 선택하고 JSON 경로를 설정
    public void SelectSong(string songId)
    {
        SelectedSongID = "Songs/" + songId;
        SelectedSongJsonPath = "JSONs/" + songId ;  // Resources/JSONs 폴더 내에 저장된다고 가정

        
    }
}
