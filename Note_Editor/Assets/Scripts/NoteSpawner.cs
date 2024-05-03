using UnityEngine;
using System.Collections;

public class NoteSpawner : MonoBehaviour
{
    public NoteManager noteManager; // NoteManager 참조
    public GameObject yellowNotePrefab, greenNotePrefab, redNotePrefab; // 노트 프리팹
    public GameObject[] prefabs;
    public Transform spawnPoint1, spawnPoint2, spawnPoint3; // 고정된 스폰 지점  
    public Transform[] spawnPoints;
    public Transform centerPoint; // 회전의 중심점
    public AudioManager audioManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) {
            SpawnCircularNote(yellowNotePrefab, "Note_Y", spawnPoint1.position);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            SpawnCircularNote(greenNotePrefab, "Note_G", spawnPoint2.position);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SpawnCircularNote(redNotePrefab, "Note_R", spawnPoint3.position);
        }
    } 
    public void SpawnNoteAtIndex(int index)
    {
        if (index < 0 || index >= prefabs.Length || index >= spawnPoints.Length) return; // 인덱스 유효성 검사

        GameObject note = Instantiate(prefabs[index], spawnPoints[index].position, Quaternion.identity);
        note.transform.SetParent(centerPoint); // 필요한 경우 중심점에 맞춰 설정
        note.tag = "Note_" + index; // 태그 설정도 인덱스에 따라 동적으로 할당

        // CircularMotion 컴포넌트가 있을 경우 설정
        CircularMotion motionScript = note.GetComponent<CircularMotion>();
        if (motionScript != null)
        {
            motionScript.centerPoint = centerPoint;
            motionScript.radius = Vector3.Distance(centerPoint.position, spawnPoints[index].position);
            motionScript.speed = 50.0f;
        }

        // 음악 시간 기록
        float musicTime = audioManager.GetMusicTime();
        noteManager.AddNote(musicTime, "Note_" + index);

    }
    void SpawnCircularNote(GameObject prefab, string tag, Vector3 position)
    {
        GameObject note = Instantiate(prefab, position, Quaternion.identity);
        note.transform.SetParent(centerPoint); 
        note.tag = tag;
        CircularMotion motionScript = note.GetComponent<CircularMotion>();
        if (motionScript != null)
        {
            motionScript.centerPoint = centerPoint;
            motionScript.radius = Vector3.Distance(centerPoint.position, position);
            motionScript.speed = 50.0f;
        }
        float musicTime = audioManager.GetMusicTime(); 
        noteManager.AddNote(musicTime, tag);
        StartCoroutine(DisableNoteAfterTime(note, 3f));
    }

    IEnumerator DisableNoteAfterTime(GameObject note, float time)
    {
        yield return new WaitForSeconds(time);
        note.SetActive(false);
    }
}
