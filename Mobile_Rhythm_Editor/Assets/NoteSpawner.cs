using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject greenNotePrefab; // 초록색 노트 프리팹
    public GameObject yellowNotePrefab; //노란색 노트 프리팹
    public GameObject lineNotePrefab; // 슬라이드판정 생성할 노트의 프리팹
    public AudioSource audioSource; // 분석할 오디오 소스
    public Transform greenSpawnPoint; // 초록노트 판정 노트를 생성할 위치
    public Transform yellowSpawnPoint; // 노랑노트 판정 노트를 생성할 위치
    public Transform lineSpawnPoint; // 슬라이드판정 노트를 생성할 위치
    public float threshold = 0.02f; // 에너지 임계값
    private float[] spectrumData = new float[1024]; // 스펙트럼 데이터

    void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // 임계값을 초과하는 주파수 대역 감지
        if (spectrumData[0] > threshold)
        {
            SpawnNote();
        }
    }

    void SpawnNote()
    {
        GameObject notePrefab;
        Transform spawnPoint;

        // 랜덤 값에 따라 노트 종류 결정
        if (Random.value <= 0.4) // 터치 판정 노트 생성
        {
            spawnPoint = greenSpawnPoint;
            notePrefab = greenNotePrefab;
        }
        else if (Random.value <= 0.8 && Random.value > 0.4)
        {
            spawnPoint = yellowSpawnPoint;
            notePrefab = yellowNotePrefab;
        }
        else // 슬라이드 판정 노트 생성
        {
            spawnPoint = lineSpawnPoint;
            notePrefab = lineNotePrefab;
        }

        // 노트 생성
        Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
    }
}