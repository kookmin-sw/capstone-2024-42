using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Networking;
using System.Collections;
public class Music_bar : MonoBehaviour
{
    public Slider progressSlider; // 진행 상태를 나타내는 슬라이더
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (progressSlider != null)
        {
            progressSlider.onValueChanged.AddListener(SetMusicPosition);
        }
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            UpdateSliderPosition();
        }
    }

    void UpdateSliderPosition()
    {
        if (audioSource.clip != null)
        {
            progressSlider.value = audioSource.time / audioSource.clip.length;
        }
    }

    public void SetMusicPosition(float value)
    {
        if (audioSource.clip != null)
        {
            audioSource.time = value * audioSource.clip.length;
        }
    }
}