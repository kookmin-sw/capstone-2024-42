using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform centerPoint; // 원의 중심점
    public float speed = 60f; // 회전 속도 (도/초), 예를 들어 매분 6도

    private float radius; // 원의 반지름
    private float angle; // 현재 각도

    public void InitializeMotion(Vector3 spawnPosition)
    {
        // 반지름을 중심점과 스폰 지점의 거리로 설정
        radius = Vector3.Distance(centerPoint.position, spawnPosition);
        
        // 시작 각도 계산
        Vector3 relativePosition = spawnPosition - centerPoint.position;
        angle = Mathf.Atan2(relativePosition.y, relativePosition.x);

        // 위치 초기 설정
        UpdatePosition();
    }

    void Update()
    {
        // 시간에 따라 각도를 감소시켜 시계 방향으로 회전
        angle -= speed * Time.deltaTime * Mathf.Deg2Rad;

        // 위치 업데이트
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // 새 위치 계산
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z);

        // 객체가 원의 중심을 바라보게 회전
        transform.rotation = Quaternion.LookRotation(Vector3.forward, centerPoint.position - transform.position);
    }
}
