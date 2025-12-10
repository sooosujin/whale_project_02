using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject bubblePrefab;   // 떨어질 구 프리팹
    public Transform spawnPoint;      // 위쪽에서 떨어지는 시작 위치
    public Transform bubbleParent;    // 생성된 구들을 정리할 부모

    [Header("Bubble Count")]
    public int maxBubbles = 25;       // 목표 개수
    public float spawnInterval = 1f;  // 몇 초에 하나씩 떨어질지

    [Header("Whale")]
    public WhaleExitController whaleExit;  // 고래 스크립트 참조

    private int currentCount = 0;
    private List<Transform> spawnedBubbles = new List<Transform>();

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (currentCount < maxBubbles)
        {
            SpawnOneBubble();
            yield return new WaitForSeconds(spawnInterval);
        }

        // 25개 다 생성되면 고래에게 "이제 가져가!" 신호 보내기
        if (whaleExit != null)
        {
            whaleExit.StartExit(spawnedBubbles);
        }
        else
        {
            Debug.LogWarning("WhaleExitController가 설정되지 않았습니다.");
        }
    }

    private void SpawnOneBubble()
    {
        if (bubblePrefab == null || spawnPoint == null)
        {
            Debug.LogError("BubbleController: bubblePrefab 또는 spawnPoint가 비어 있습니다.");
            return;
        }

        GameObject bubble = Instantiate(
            bubblePrefab,
            spawnPoint.position,
            Quaternion.identity,
            bubbleParent
        );

        spawnedBubbles.Add(bubble.transform);
        currentCount++;
    }
}
