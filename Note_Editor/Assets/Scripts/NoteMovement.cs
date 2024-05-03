using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float approachSpeed = 5f; // 접근 속도
    private float startZ = 30f; // 시작 Z 위치
    private float endZ = 0f; // 종료 Z 위치 (게임 카메라 쪽)

    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 참조

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // 초기 투명도를 0으로 설정
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y,
            Mathf.MoveTowards(transform.position.z, endZ, approachSpeed * Time.deltaTime));

        float alpha = 1f - (transform.position.z - endZ) / (startZ - endZ);
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

        if (transform.position.z <= endZ)
        {
            gameObject.SetActive(false);
        }
    }
}
