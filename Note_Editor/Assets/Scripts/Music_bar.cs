using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro 네임스페이스 추가

public class Music_bar : MonoBehaviour
{
    public Slider progressSlider;
    public AudioSource audioSource;
    public TMP_Text timeText;  // TextMeshPro 텍스트 객체 참조

    void Start() {
        progressSlider.onValueChanged.AddListener(delegate { UpdateTimeDisplay(); });
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (audioSource.isPlaying) {
            progressSlider.value = audioSource.time / audioSource.clip.length;
            UpdateTimeDisplay();
        }
    }

    void UpdateTimeDisplay() {
        float time = progressSlider.value * audioSource.clip.length;
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);  // 텍스트 업데이트
    }

    public void SetMusicPosition(float value) {
        audioSource.time = value * audioSource.clip.length;
        UpdateTimeDisplay();  // 위치 조정 시 텍스트도 업데이트
    }
}
