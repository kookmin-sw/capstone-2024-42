using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{

    private AudioSource audioSource;
    public TMP_Text musicTimeText;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            UpdateMusicTimeUI(audioSource.time);
        }
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public float GetMusicTime()
    {
        return audioSource.time;
    }
    public float GetClipLength() 
    {
        return audioSource.clip.length;
    }

    private void UpdateMusicTimeUI(float currentTime)
    {
        int minutes = (int)currentTime / 60;
        int seconds = (int)currentTime % 60;
        musicTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetMusicTime(float time)
    {
        audioSource.time = time;
        UpdateMusicTimeUI(time);
    }

    public void RewindMusicBy(float seconds)
    {
        SetMusicTime(Mathf.Max(0, audioSource.time - seconds));
    }
}
