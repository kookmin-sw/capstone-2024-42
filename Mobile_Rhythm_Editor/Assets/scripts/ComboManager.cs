using UnityEngine;
using TMPro; // TextMesh Pro 네임스페이스 추가
public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance { get; private set;}
    public TextMeshProUGUI comboText; // 점수를 표시할 TextMeshProUGUI

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
    private int currentComboIndex = 0;
    void Start()
    {
        UpdateComboText(); // 시작할 때 점수 텍스트 업데이트
    }

    // 점수를 추가하는 메서드
    public void AddCombo(int combo)
    {
        currentCombo += combo;
        UpdateComboText(); // 점수 텍스트 업데이트
    }
    public void ResetCombo()
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
}

