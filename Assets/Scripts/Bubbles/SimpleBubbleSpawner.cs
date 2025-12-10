using UnityEngine;

public class SimpleBubbleSpawner : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject bubblePrefab;       // TestBubblePrefab
    public Transform bubbleParent;        // BubbleRoot

    [Header("Pile & Whale")]
    public Transform pileCenter;          // BubblePileCenter
    public WhaleExitController whaleExit; // WhaleCenter_Old 에 붙은 스크립트

    [Header("Count & Interval")]
    public int totalCount = 25;           // 총 구슬 개수
    public float interval = 1.2f;         // 몇 초마다 하나씩 생성 (느리게!)

    [Header("Pile Shape")]
    public float baseRadius = 1.5f;       // 기본 반지름
    public float radiusGrow = 0.03f;      // 개수가 늘어날수록 조금씩 퍼짐
    public float spawnHeight = 3f;        // 바닥에서 얼마나 위에서 떨어질지
    public float verticalJitter = 0.2f;   // 살짝 높이 랜덤

    private float timer = 0f;
    private int spawnedCount = 0;
    private bool exitTriggered = false;

    private void Update()
    {
        // 1) 아직 덜 만들었으면 계속 생성
        if (spawnedCount < totalCount)
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                timer = 0f;
                SpawnOne();
            }
        }
        // 2) 다 만들었으면 한 번만 고래 출발 신호
        else if (!exitTriggered)
        {
            exitTriggered = true;

            if (whaleExit != null)
            {
                Debug.Log("[Spawner] All bubbles spawned → StartExit()");
                whaleExit.StartExit();
            }
            else
            {
                Debug.LogWarning("[Spawner] whaleExit is NULL on SimpleBubbleSpawner.");
            }
        }
    }

    private void SpawnOne()
    {
        if (bubblePrefab == null || bubbleParent == null || pileCenter == null)
        {
            Debug.LogError("SimpleBubbleSpawner: bubblePrefab / bubbleParent / pileCenter 중 비어 있습니다.");
            return;
        }

        // 이번 구슬의 "최종 자리"를 원형 영역에서 계산
        int index = spawnedCount; // 0,1,2,...
        float radius = baseRadius + radiusGrow * index;

        float angle = Random.Range(0f, Mathf.PI * 2f);
        float dist  = Random.Range(0f, radius);

        float offsetX = Mathf.Cos(angle) * dist;
        float offsetZ = Mathf.Sin(angle) * dist;

        float offsetY = Random.Range(0f, verticalJitter);

        // 바닥 위 위치(최종 자리)
        Vector3 groundPos = new Vector3(
            pileCenter.position.x + offsetX,
            pileCenter.position.y + offsetY,
            pileCenter.position.z + offsetZ
        );

        // 실제 스폰 위치 = 그 자리 바로 위에서 spawnHeight 만큼 올리기
        Vector3 spawnPos = groundPos + Vector3.up * spawnHeight;

        GameObject bubble = Instantiate(
            bubblePrefab,
            spawnPos,
            Quaternion.identity,
            bubbleParent
        );

        // (BubbleStackOnGround은 이제 그냥 "멈추기"만 하므로 Init 불필요)

        spawnedCount++;
        bubble.name = $"SpawnedBubble_{spawnedCount}";
        Debug.Log($"[Spawner] Bubble #{spawnedCount} 생성 at {spawnPos}, target {groundPos}");
    }
}
