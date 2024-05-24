using UnityEngine;

public class Judgment : MonoBehaviour
{
    private ObjectPoolManager objectPoolManager;

    void Start()
    {
        objectPoolManager = FindObjectOfType<ObjectPoolManager>(); // 오브젝트 풀 매니저 찾기
    }

    void OnTriggerEnter2D(Collider2D other) // 2D Collider로 변경
    {
        if (other.CompareTag("Note_G") || other.CompareTag("Note_Y") || other.CompareTag("Note_R"))
        {
            ProcessJudgment(other.gameObject, other.tag);
            // 오브젝트 풀로 반환
            objectPoolManager.ReturnObject(other.gameObject, other.tag);
        }
    }

    void ProcessJudgment(GameObject note, string noteType)
    {
        // 판정 처리 로직
        Debug.Log(noteType + " hit at: " + note);
        // 여기서 추가 판정 처리 가능
    }
}
