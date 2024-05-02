using UnityEngine;
using TMPro; // TextMesh Pro 네임스페이스 추가

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public TextMeshProUGUI scoreText; // 점수를 표시할 TextMeshProUGUI

    private int currentScore = 0; // 현재 점수

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText(); // 시작할 때 점수 텍스트 업데이트
    }

    // 점수를 추가하는 메서드
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreText(); // 점수 텍스트 업데이트
    }

    // 점수 텍스트를 업데이트하는 메서드
    private void UpdateScoreText()
    {
        if (scoreText != null) // null 체크
        {
            scoreText.text = "Score: " + currentScore.ToString(); // 현재 점수 표시
        }
    }

    // 현재 점수를 가져오는 메서드
    public int GetCurrentScore()
    {
        return currentScore;
    }
}
