using UnityEngine;

public class RotateOnTouch : MonoBehaviour
{
    public Transform centerPoint;  // 회전의 중심점을 설정합니다.
    public RandomMarkers randomMarkers;  // RandomMarkers 스크립트 참조
    public float rotationSpeed = 100f;  // 회전 속도를 조절합니다.
    public float sensitivity = 0.5f;  // 터치 움직임에 대한 민감도를 조절합니다.
    public CircleCollider2D touchCollider;  // 원판의 Collider2D

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치를 가져옵니다.
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;  // Z축 조정, 원판이 XY 평면에 있을 경우

            if (touch.phase == TouchPhase.Moved)
            {
                // 터치 포인트가 원판의 Collider 안에 있는지 확인합니다.
                if (touchCollider.OverlapPoint(touchPos))
                {
                    Vector2 touchDeltaPosition = touch.deltaPosition;

                    // 터치 움직임을 기준으로 회전량을 계산하되, 민감도를 적용합니다.
                    float rotation = touchDeltaPosition.x * rotationSpeed * sensitivity * Time.deltaTime;
                    transform.RotateAround(centerPoint.position, Vector3.forward, -rotation);

                    // 현재 각도를 RandomMarkers 스크립트에 전달
                    float currentAngle = transform.eulerAngles.z;  // Z축을 기준으로 현재 각도를 얻습니다.
                    if (randomMarkers != null)
                    {
                        randomMarkers.UpdateRotation(currentAngle);  // 수정된 메소드 호출
                    }
                }
            }
        }
    }
}
