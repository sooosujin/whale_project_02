using UnityEngine;

public class SimpleBubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;   // 생성할 버블 프리팹
    public Transform bubbleParent;    // 버블들을 정리할 부모 (BubbleRoot)

    public int totalCount = 25;       // 몇 개까지 만들지
    public float interval = 1f;       // 몇 초마다 하나씩 만들지

    private float timer = 0f;
    private int spawnedCount = 0;

    private void Update()
    {
        if (spawnedCount >= totalCount) return;   // 다 만들었으면 종료

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        if (bubblePrefab == null || bubbleParent == null)
        {
            Debug.LogError("SimpleBubbleSpawner: bubblePrefab 또는 bubbleParent가 비었습니다.");
            return;
        }

        // ★ 메인 카메라 앞쪽에 생성 ★
        Camera cam = Camera.main;
        Vector3 spawnPos;

        if (cam != null)
        {
            float distance = 15f;      // 카메라에서 앞쪽으로 15
            spawnPos = cam.transform.position
                       + cam.transform.forward * distance
                       + Vector3.up * 3f;   // 살짝 위로
        }
        else
        {
            spawnPos = new Vector3(0, 5, 0);
        }

        GameObject bubble = Instantiate(
            bubblePrefab,
            spawnPos,
            Quaternion.identity,
            bubbleParent
        );

        bubble.name = $"SpawnedBubble_{spawnedCount + 1}";
        spawnedCount++;

        Debug.Log($"Bubble #{spawnedCount} 생성 at {spawnPos}");
    }
}
