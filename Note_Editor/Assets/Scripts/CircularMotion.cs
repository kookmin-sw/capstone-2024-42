using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform centerPoint; // 원의 중심점
    public float radius = 1.0f; // 원의 반지름
    public float speed = 60f; // 회전 속도 (도/초), 예를 들어 매분 6도

    private float angle; // 초기 각도

    void Start()
    {
        // 초기 각도를 90도로 설정하여 12시 방향에서 시작
        angle = Mathf.PI / 2;  // 라디안 값으로 변환
    }

    void Update()
    {
        // 각도를 시간에 따라 시계 방향으로 업데이트
        angle -= speed * Time.deltaTime * Mathf.Deg2Rad; // 라디안 값으로 계산

        // 각도를 라디안으로 사용하여 새 위치 계산
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z);

        // 노트의 회전을 업데이트하여 원의 중심을 바라보도록 설정
        transform.rotation = Quaternion.LookRotation(Vector3.forward, centerPoint.position - transform.position);
    }
}
