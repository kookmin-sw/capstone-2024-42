using UnityEngine;
using TMPro;
using System.Collections;

public class RandomMarkers : MonoBehaviour
{
    public Transform centerPoint; // 회전의 중심점
    public GameObject markerPrefab; // 마커 프리팹
    public GameObject explosionEffectPrefab; // 터지는 효과 프리팹
    public TextMeshProUGUI countdownText; // TextMesh Pro UI 텍스트
    public float markerLifetime = 3f; // 마커의 생존 시간
    public float angleBetweenMarkers = 60f; // 두 마커 사이의 각도
    public int scorePerSuccess = 100; // 성공 시 부여될 점수

    private GameObject markerA, markerB;
    private float baseAngle; // 기준 각도
    private float accumulatedRotation = 0f; // 누적 회전 각도
    private float lastAngle = 0f; // 마지막 확인된 각도
    private bool isRotating = false; // 회전이 시작되었는지 확인

    void Start()
    {
        StartCoroutine(ShowMarkersWithCountdown());
    }

    IEnumerator ShowMarkersWithCountdown()
    {
        while (true)
        {
            int countdown = 3; // 카운트다운 3초 시작
            while (countdown > 0)
            {
                countdownText.text = countdown.ToString();
                yield return new WaitForSeconds(1f);
                countdown--;
            }

            countdownText.text = ""; // 카운트다운 텍스트 클리어
            baseAngle = Random.Range(0f, 360f); // 랜덤 기준 각도 설정
            lastAngle = baseAngle; // 초기 회전 각도 설정
            accumulatedRotation = 0f; // 누적 회전 초기화
            isRotating = true; // 회전 시작 표시
            ActivateMarkers(); // 마커 활성화
            yield return new WaitForSeconds(markerLifetime); // 마커의 생존 시간동안 대기

            if (markerA != null || markerB != null) // 시간이 지나면 마커 자동 제거
            {
                RemoveMarkers();
            }
            isRotating = false; // 회전 종료
        }
    }

    public void UpdateRotation(float currentAngle)
    {
        if (!isRotating) return; // 회전 중이 아니면 처리하지 않음

        float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);
        accumulatedRotation += Mathf.Abs(deltaAngle);

        if (accumulatedRotation >= angleBetweenMarkers) // 누적 회전 각도가 설정 각도를 초과하면 성공
        {
            CheckSuccess();
        }

        lastAngle = currentAngle;
    }

    void CheckSuccess()
    {
        if (markerA != null) Destroy(markerA);
        if (markerB != null) Destroy(markerB);

        GameObject explosion = Instantiate(explosionEffectPrefab, centerPoint.position, Quaternion.identity);
        Destroy(explosion, 1f); // 1초 후 폭발 효과 제거

        ScoreManager.Instance.AddScore(scorePerSuccess); // 점수 추가
        //countdownText.text = "Success! + " + scorePerSuccess + " points";

        accumulatedRotation = 0f; // 누적 회전 초기화
        isRotating = false; 
    }

    void ActivateMarkers()
    {
        Vector3 positionA = GetPositionOnCircle(baseAngle);
        Vector3 positionB = GetPositionOnCircle(baseAngle + angleBetweenMarkers);

        markerA = Instantiate(markerPrefab, positionA, Quaternion.identity);
        markerB = Instantiate(markerPrefab, positionB, Quaternion.identity);

        OrientMarkerTowardsCenter(markerA);
        OrientMarkerTowardsCenter(markerB);
    }

    void RemoveMarkers()
    {
        if (markerA != null)
        {
            Destroy(markerA);
        }
        if (markerB != null)
        {
            Destroy(markerB);
        }
    }

    Vector3 GetPositionOnCircle(float angle)
    {
        float x = centerPoint.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * 3f; // 반지름 3 가정
        float y = centerPoint.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * 3f;
        return new Vector3(x, y, 0);
    }

    void OrientMarkerTowardsCenter(GameObject marker)
    {
        Vector3 directionToCenter = (centerPoint.position - marker.transform.position).normalized;
        marker.transform.rotation = Quaternion.LookRotation(Vector3.forward, directionToCenter);
    }
}
