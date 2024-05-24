using System.Collections;
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

        if (audioSource != null && SongSelectionManager.Instance != null)
        {
            AudioClip clip = Resources.Load<AudioClip>(SongSelectionManager.Instance.SelectedSongID);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.PlayDelayed(delay);
            }
            else
            {
                Debug.LogError("Audio clip not found for the selected song.");
            }
        }
        else
        {
            Debug.LogError("AudioSource component not found or song not selected properly.");
        }
    }
}
