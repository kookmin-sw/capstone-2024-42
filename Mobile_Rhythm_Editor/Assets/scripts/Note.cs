using UnityEngine;

public class Note : MonoBehaviour
{/*
    public Transform judgmentLine; // 판정선
    public float perfectThreshold = 0.05f;
    public float greatThreshold = 0.1f;
    public float goodThreshold = 0.15f;
    public GameObject effectPrefab;
    public CharacterSpriteManager spriteManager;
    private float missThreshold = 0.15f;

    void Update()
    {
        CheckIfMissed();
    }
    private void CheckIfMissed()
    {
        // 판정선을 기준으로 노트가 일정 거리 이상 지나갔는지 확인
        if (transform.position.y < judgmentLine.position.y - missThreshold)
        {
            PerformMissAction();
            Destroy(gameObject); // 노트 제거
        }
    }

    // 노트 판정 메서드
    public void JudgeNote()
    {
        float distance = Mathf.Abs(transform.position.y - judgmentLine.position.y);
        string result;

        if (distance <= perfectThreshold)
        {
            result = "Perfect";
            PerformAction(100, 1, 1);
        }
        else if (distance <= greatThreshold)
        {
            result = "Great";
            PerformAction(80, 1, 1);
        }
        else if (distance <= goodThreshold)
        {
            result = "Good";
            PerformAction(50, 1, 1);
        }
        else
        {
            result = "Miss";
            PerformMissAction();
        }

        if (spriteManager != null)
        {
            spriteManager.ChangeSprite(result);
        }

        // 이펙트 생성 및 노트 제거
        if (result != "Miss" && effectPrefab != null)
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject); // 노트 제거
    }

    private void PerformAction(int score, int combo, int points)
    {
        if (ScoreManager.Instance != null) 
        {
            ScoreManager.Instance.AddScore(score);
            ComboManager.Instance.AddCombo(combo);
            LifeManager.Instance.Plus(points);
        }
    }

    private void PerformMissAction()
    {
        Debug.Log("Miss Action Triggered");
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(0);
            ComboManager.Instance.ResetCombo();
            LifeManager.Instance.Minus(3);
        }
        if (spriteManager != null)
        {
            spriteManager.ChangeSprite("Miss");
        }
    }
*/}
