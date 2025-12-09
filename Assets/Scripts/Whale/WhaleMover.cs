using UnityEngine;

public class WhaleMover : MonoBehaviour
{
    [Header("Path")]
    public Transform startPoint;
    public Transform middlePoint;
    public Transform endPoint;

    [Header("Grab")]
    public Transform grabPoint;          // 고래 입 앞
    public Transform bubbleStackRoot;    // 구 더미(예: Bubble 또는 Bubble_test 안의 자식)
    public GameObject grabBubblePrefab;  // 입에 물고 갈 구슬 프리팹

    [Header("Timing")]
    public float moveDuration = 8f;      // 전체 이동 시간
    [Range(0f, 1f)]
    public float grabTime = 0.5f;        // 이동 진행도 중 언제 물지 (0~1)

    bool moving = false;
    bool grabbed = false;
    float startTime;

    void Start()
    {
        // 씬이 시작되면 자동으로 고래 출발
        BeginMove();
    }

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

        // 부드러운 속도 곡선
        float easeT = Mathf.SmoothStep(0f, 1f, t);

        // 경로 보간 (start -> middle -> end)
        Vector3 pos;
        if (easeT < 0.5f)
        {
            float tt = easeT / 0.5f;
            pos = Vector3.Lerp(startPoint.position, middlePoint.position, tt);
        }
        else
        {
            float tt = (easeT - 0.5f) / 0.5f;
            pos = Vector3.Lerp(middlePoint.position, endPoint.position, tt);
        }

        transform.position = pos;

        // 진행 방향으로 자연스럽게 회전
        // (선택사항이지만 있으면 훨씬 자연스러움)
        // 바로 이전 프레임 위치를 저장하고 비교하는 방식이 제일 안정적이지만
        // 단순화를 위해 여기서는 LookRotation만 써도 충분함.
        // 필요 없으면 아래 줄은 빼도 됨.
        // transform.LookAt(pos + (endPoint.position - startPoint.position));

        // 정해진 타이밍에 한 번만 "구슬 물기" 실행
        if (!grabbed && t >= grabTime)
        {
            grabbed = true;
            DoGrab();
        }
    }

    void DoGrab()
    {
        // 1) 고래 입 앞에 새로운 구슬 하나 생성해서 자식으로 붙이기
        if (grabBubblePrefab != null && grabPoint != null)
        {
            Instantiate(grabBubblePrefab, grabPoint.position, grabPoint.rotation, grabPoint);
        }

        // 2) 구 더미에서 맨 위 구슬 하나만 "숨기기"
        if (bubbleStackRoot != null && bubbleStackRoot.childCount > 0)
        {
            Transform topBubble = bubbleStackRoot.GetChild(bubbleStackRoot.childCount - 1);
            var renderer = topBubble.GetComponent<Renderer>();
            if (renderer != null)
                renderer.enabled = false;
        }
    }
}
