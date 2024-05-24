using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public float time;
    public string type;
}

[System.Serializable]
public class NoteDataCollection
{
    public NoteData[] Notes;
}

public class NoteSpawner : MonoBehaviour
{
    public AudioSource audioSource;
    public ObjectPoolManager poolManager;
    private List<NoteData> notesToSpawn;
    public Transform[] spawnPoints;
    public Transform centerPoint;
    private Dictionary<string, int> typeToIndexMap = new Dictionary<string, int>
    {
        { "Note_R", 2 }, 
        { "Note_G", 1 }, 
        { "Note_Y", 0 }
    };

    void Start()
    {
        LoadNotesData();
    }

    void Update()
    {
        if (notesToSpawn != null && notesToSpawn.Count > 0 && audioSource.isPlaying)
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
        if (SongSelectionManager.Instance != null)
        {
            string path = SongSelectionManager.Instance.SelectedSongJsonPath;
            Debug.Log("Attempting to load JSON from: " + path);  // 로그 추가
            TextAsset jsonData = Resources.Load<TextAsset>(path);
            if (jsonData != null)
            {
                NoteDataCollection loadedData = JsonUtility.FromJson<NoteDataCollection>(jsonData.text);
                if (loadedData != null && loadedData.Notes != null)
                {
                    notesToSpawn = new List<NoteData>(loadedData.Notes.OrderBy(note => note.time));
                }
                else
                {
                    Debug.LogError("Loaded notes data is invalid or null.");
                }
            }
            else
            {
                Debug.LogError("Failed to load JSON data: " + path);
            }
        }
        else
        {
            Debug.LogError("SongSelectionManager instance is not initialized.");
        }
    }

    void SpawnNote(NoteData noteData)
    {
        GameObject note = poolManager.GetObject(noteData.type);
        if (!typeToIndexMap.TryGetValue(noteData.type, out int spawnIndex) || spawnIndex >= spawnPoints.Length)
        {
            Debug.LogError("Invalid note type or spawn index out of range: " + noteData.type);
            return;
        }

        Transform spawnLocation = spawnPoints[spawnIndex];
        note.transform.position = spawnLocation.position;

        CircularMotion motion = note.GetComponent<CircularMotion>();
        if (motion == null)
        {
            motion = note.AddComponent<CircularMotion>();
        }
        motion.centerPoint = centerPoint;
        motion.Initialize(spawnLocation.position);
    }
}
