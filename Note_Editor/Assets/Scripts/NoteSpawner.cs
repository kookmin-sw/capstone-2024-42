using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public ObjectPoolManager objectPoolManager;
    public Transform[] spawnPoints;
    public AudioManager audioManager;
    public NoteManager noteManager;
    public Transform centerPoint;
    public GameObject notePrefabR, notePrefabG, notePrefabY;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) {
            SpawnNote("Note_Y", spawnPoints[0].position, notePrefabY);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            SpawnNote("Note_G", spawnPoints[1].position, notePrefabG);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SpawnNote("Note_R", spawnPoints[2].position, notePrefabR);
        }
    }

    void SpawnNote(string type, Vector3 spawnPosition, GameObject notePrefab)
    {
        GameObject note = objectPoolManager.GetObject(type);
        if (note == null) return;

        note.transform.position = spawnPosition;
        note.SetActive(true);

        CircularMotion motionScript = note.GetComponent<CircularMotion>();
        if (motionScript != null)
        {
            motionScript.centerPoint = centerPoint;
            motionScript.InitializeMotion(spawnPosition);
        }

        float musicTime = audioManager.GetMusicTime();
        noteManager.AddNote(musicTime, type, notePrefab);
    }
}
