using UnityEngine;

public class RotateOnTouch : MonoBehaviour
{
    public Transform centerPoint;  // 회전의 중심점을 설정합니다.
    public RandomMarkers randomMarkers;  // RandomMarkers 스크립트 참조
    public float rotationSpeed = 100f;  // 회전 속도를 조절합니다.
    public float sensitivity = 0.5f;  // 터치 움직임에 대한 민감도를 조절합니다.
    public CircleCollider2D touchCollider;  // 원판의 Collider2D

    private float previousAngle;

    void Start()
    {
        previousAngle = transform.eulerAngles.z;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치를 가져옵니다.
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;

            if (touch.phase == TouchPhase.Moved && touchCollider.OverlapPoint(touchPos))
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                float rotation = touchDeltaPosition.x * rotationSpeed * sensitivity * Time.deltaTime;
                bool shouldRotateClockwise = touchDeltaPosition.x > 0; // x 이동 거리가 양수면 시계 방향 회전
                transform.RotateAround(centerPoint.position, Vector3.forward, shouldRotateClockwise ? -rotation : rotation);

                float currentAngle = transform.eulerAngles.z;
                randomMarkers.UpdateRotation(currentAngle, !shouldRotateClockwise); // 회전 방향을 반대로 전달
                previousAngle = currentAngle;
            }
        }
    }
}
