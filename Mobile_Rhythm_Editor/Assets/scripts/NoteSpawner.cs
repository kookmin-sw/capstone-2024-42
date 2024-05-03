using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public float time; // 노트가 나타날 시간
    public string type; // 노트의 타입
}

[System.Serializable]
public class NoteDataCollection
{
    public NoteData[] Notes;
}

public class NoteSpawner : MonoBehaviour
{
    public AudioSource audioSource; // 게임의 배경 음악 오디오 소스
    public GameObject[] notePrefabs; // 노트 프리팹 배열
    private List<NoteData> notesToSpawn; // 스폰할 노트 데이터 리스트
    public string jsonFilePath = "Assets/Guilty Bear_bpm120_Am.json";  // JSON 파일 경로
    private Dictionary<string, Transform> spawnPointMap; // 노트 타입별 스폰 위치
    public Transform[] spawnPoints; // Unity 인스펙터에서 설정할 스폰 포인트 배열
    public Transform centerPoint; // 원의 중심점

    void Start()
    {
        InitializeSpawnPoints();
        LoadNotesData();
        audioSource.Play();
    }

    void Update()
    {
        if (notesToSpawn.Count > 0 && audioSource.isPlaying)
        {
            while (notesToSpawn.Count > 0 && audioSource.time >= notesToSpawn[0].time)
            {
                SpawnNote(notesToSpawn[0]);
                notesToSpawn.RemoveAt(0);
            }
        }
    }

    void LoadNotesData()
    {
        string jsonText = File.ReadAllText(jsonFilePath);
        NoteDataCollection loadedData = JsonUtility.FromJson<NoteDataCollection>(jsonText);
        if (loadedData != null && loadedData.Notes != null)
        {
            notesToSpawn = new List<NoteData>(loadedData.Notes.OrderBy(note => note.time));
        }
        else
        {
            Debug.LogError("Failed to load notes data or notes are null.");
        }
    }

    void InitializeSpawnPoints()
    {
        spawnPointMap = new Dictionary<string, Transform>();
        for (int i = 0; i < notePrefabs.Length; i++)
        {
            spawnPointMap[notePrefabs[i].name] = spawnPoints[i];
        }
    }

    void SpawnNote(NoteData noteData)
    {
        GameObject prefab = notePrefabs.FirstOrDefault(p => p.name == noteData.type);
        if (prefab != null)
        {
            Transform spawnLocation = spawnPointMap[noteData.type];
            GameObject spawnedNote = Instantiate(prefab, spawnLocation.position, Quaternion.identity);
            InitializeCircularMotion(spawnedNote, spawnLocation);
        }
        else
        {
            Debug.LogError("Prefab not found for type: " + noteData.type);
        }
    }

    void InitializeCircularMotion(GameObject note, Transform spawnLocation)
    {
        CircularMotion motion = note.GetComponent<CircularMotion>();
        if (motion == null)
        {
            motion = note.AddComponent<CircularMotion>();
        }
        motion.centerPoint = centerPoint;
        motion.Initialize(spawnLocation.position); // 원의 중심점과 스폰 위치를 설정하여 원형 이동을 초기화
    }
}
