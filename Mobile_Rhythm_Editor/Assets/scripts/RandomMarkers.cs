using UnityEngine;
using TMPro;
using System.Collections;

public class RandomMarkers : MonoBehaviour
{
    public Transform centerPoint; // 회전의 중심점
    public GameObject explosionEffectPrefab; // 터지는 효과 프리팹
    public TextMeshProUGUI countdownText; // TextMesh Pro UI 텍스트
    public float markerLifetime = 3f; // 마커의 생존 시간
    public int scorePerSuccess = 100; // 성공 시 부여될 점수
    public ObjectPoolManager objectPoolManager; // 오브젝트 풀 매니저 참조

    private GameObject markerA;
    private bool isRotating = false; // 회전이 시작되었는지 확인
    private bool shouldRotateRight; // 올바른 회전 방향이 오른쪽인지

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

            countdownText.text = "";
            isRotating = true; // 회전 시작 표시
            ActivateMarker(); // 마커 활성화
            yield return new WaitForSeconds(markerLifetime); // 마커의 생존 시간동안 대기

            RemoveMarkers();
            isRotating = false; // 회전 종료
        }
    }

    void ActivateMarker()
    {
        markerA = objectPoolManager.GetObject("Marker"); // 랜덤 마커 받아오기
        markerA.transform.position = GetPositionOnCircle(0); // 마커 위치 설정

        // 마커 방향을 체크하고 회전 방향 설정
        MarkerType markerType = markerA.GetComponent<MarkerType>();
        shouldRotateRight = markerType.IsUpMarker;
        OrientMarkerTowardsCenter(markerA);
    }

    public void UpdateRotation(float currentAngle, bool isRotatingRight)
    {
        if (!isRotating) return;

        if (shouldRotateRight == isRotatingRight)
        {
            CorrectRotation();
        }
    }

    void CorrectRotation()
    {
        ScoreManager.Instance.AddScore(scorePerSuccess);
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, centerPoint.position, Quaternion.identity);
            Destroy(explosion, 1f); // 1초 후 폭발 효과 제거
        }
        isRotating = false;
    }

    void RemoveMarkers()
    {
        if (markerA != null) objectPoolManager.ReturnObject(markerA, "Marker");
    }

    Vector3 GetPositionOnCircle(float angle)
    {
        float radius = 3f; // 반지름을 3으로 설정
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(centerPoint.position.x + Mathf.Cos(radian) * radius,
                           centerPoint.position.y + Mathf.Sin(radian) * radius,
                           0);
    }

    void OrientMarkerTowardsCenter(GameObject marker)
    {
        marker.transform.rotation = Quaternion.LookRotation(Vector3.forward, centerPoint.position - marker.transform.position);
    }
}
