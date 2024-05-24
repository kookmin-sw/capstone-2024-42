using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public Text scoreText; // 점수를 표시할 TextMeshProUGUI
    public int Score {get; set;}
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
    public void AddScore(int point)
    {
        currentScore += point;
        Score = currentScore;
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
    //초기화
    public void ResetScore()
    {
        Score = 0;
        UpdateScoreText();
    }

}
