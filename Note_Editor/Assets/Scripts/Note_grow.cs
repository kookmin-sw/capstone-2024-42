using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_grow : MonoBehaviour
{
    public float growSpeed = 1f; // 노트가 커지는 속도

    void Start()
    {
        transform.localScale = Vector3.zero; // 노트의 초기 크기를 0으로 설정
    }

    void Update()
    {
        // 노트를 점점 커지게 함
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
    }
}