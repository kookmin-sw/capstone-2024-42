using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get; private set; }
    public int Point = 100; // 초기 포인트
    public AudioSource audioSource; // 노래를 재생할 오디오 소스
    public TextMeshProUGUI resultText; // 결과를 표시할 TMP 텍스트
    public Slider scoreSlider; // 점수를 표시할 슬라이더

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        scoreSlider.maxValue = 100;
        scoreSlider.value = Point;
        StartCoroutine(CheckMusicStatus());
    }

    public void Plus(int points)
    {
        Point += points;
        UpdateSlider();
    }

    public void Minus(int points)
    {
        Point -= points;
        UpdateSlider();
        if (Point <= 0)
        {
            EvaluateGameImmediateFailure();
        }
    }

    private void UpdateSlider()
    {
        scoreSlider.value = Point;
        Debug.Log("Current Point: " + Point);
    }

    private IEnumerator CheckMusicStatus()
    {
        yield return new WaitForSeconds(5.0f);
        while (audioSource.isPlaying)
        {
            if (Point <= 0) break;
            yield return new WaitForSeconds(1.0f); // 1초마다 확인
        }
        EvaluateEndOfSong();
    }

    private void EvaluateEndOfSong()
    {
        if (Point > 0)
        {
            resultText.text = "성공! 게임을 클리어했습니다!";
            SceneManager.LoadScene("SuccessScene");
        }
        else
        {
            EvaluateGameImmediateFailure();
        }
    }

    private void EvaluateGameImmediateFailure()
    {
        resultText.text = "Fail!! Try Again";
        SceneManager.LoadScene("FailureScene");
    }
}
