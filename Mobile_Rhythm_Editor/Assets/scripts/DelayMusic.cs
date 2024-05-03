using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayMusic : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource 컴포넌트
    public float delay = 2.0f; // 오디오 재생을 지연시킬 시간 (초 단위)

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource != null)
        {
            audioSource.PlayDelayed(delay);
        }
        else
        {
            Debug.LogError("AudioSource 컴포넌트가 없습니다.");
        }
    }
}
