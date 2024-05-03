using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform centerPoint; // 원의 중심점
    public float speed = 60f; // 회전 속도 (도/초)

    private float radius; // 원의 반지름
    private float angle; // 초기 각도

    public void Initialize(Vector3 spawnPosition)
    {
        Vector3 offset = spawnPosition - centerPoint.position;
        radius = offset.magnitude; // 거리를 반지름으로 계산
        angle = Mathf.Atan2(offset.y, offset.x); // 초기 각도 설정

        UpdatePosition(); // 처음 위치를 설정
    }

    void Update()
    {
        angle -= speed * Time.deltaTime * Mathf.Deg2Rad; // 각도 업데이트
        UpdatePosition(); // 위치 업데이트
    }

    void UpdatePosition()
    {
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z); // 새 위치 설정
        transform.rotation = Quaternion.LookRotation(Vector3.forward, centerPoint.position - transform.position);
    }
}
