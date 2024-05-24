using UnityEngine;
using UnityEngine.UI; // TextMeshPro 대신 Unity UI를 사용합니다.

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance { get; private set; }
    public Text comboText; // 점수를 표시할 Text 컴포넌트
    public int MaxCombo { get; set; }
    private int currentCombo = 0; // 현재 점수

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
        UpdateComboText();
    }

    // 점수를 추가하는 메서드
    public void AddCombo(int combo)
    {
        currentCombo += combo;
        if (currentCombo > MaxCombo)
        {
            MaxCombo = currentCombo;
        }
        UpdateComboText();
    }

    public void ResetCombo_play()
    {
        currentCombo = 0;
        UpdateComboText();
    }

    // 점수 텍스트를 업데이트하는 메서드
    private void UpdateComboText()
    {
        if (comboText != null) // null 체크
        {
            comboText.text = "Combo: " + currentCombo.ToString(); // 현재 점수 표시
        }
    }

    // 현재 점수를 가져오는 메서드
    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    // 초기화
    public void ResetCombo()
    {
        MaxCombo = 0;
        currentCombo = 0;
        UpdateComboText();
    }
}
