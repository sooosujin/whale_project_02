using UnityEngine;

public class WhaleMover : MonoBehaviour
{
    public Transform startPoint;
    public Transform middlePoint;
    public Transform endPoint;
    public Transform grabPoint;      // 고래 입 위치(GrabPoint)
    public Transform bubbleRoot;     // Bubble_test

    public float moveDuration = 8f;  // 전체 이동 시간
    public float grabTime = 0.5f;    // 0~1 사이, 언제 구슬을 물지 (0.5 = 중간)

    bool moving = false;
    bool grabbed = false;
    float startTime;

    public void BeginMove()
    {
        startTime = Time.time;
        moving = true;
        grabbed = false;
    }

    void Update()
    {
        if (!moving || startPoint == null || middlePoint == null || endPoint == null)
            return;

        float t = (Time.time - startTime) / moveDuration;
        if (t >= 1f)
        {
            t = 1f;
            moving = false;
        }

        // 경로 보간 (start -> middle -> end)
        Vector3 pos;
        if (t < 0.5f)
        {
            float tt = t / 0.5f;
            pos = Vector3.Lerp(startPoint.position, middlePoint.position, tt);
        }
        else
        {
            float tt = (t - 0.5f) / 0.5f;
            pos = Vector3.Lerp(middlePoint.position, endPoint.position, tt);
        }

        transform.position = pos;

        // 중간쯤에서 구슬을 물기
        if (!grabbed && t >= grabTime && bubbleRoot != null && grabPoint != null)
        {
            bubbleRoot.SetParent(grabPoint);
            grabbed = true;
        }
    }
}
