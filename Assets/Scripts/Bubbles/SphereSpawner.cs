using System.Collections;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;      // 구 프리팹
    public Transform[] spawnPoints;      // 쌓일 위치들z
    public float spawnInterval = 1.0f;   // 몇 초 간격으로 생성할지
    public WhaleMover whaleMover;        // 고래 이동 스크립트 참조

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        // 포인트마다 구 하나씩 생성
        foreach (Transform p in spawnPoints)
        {
            Instantiate(spherePrefab, p.position, p.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }

        // 다 쌓인 뒤에 고래 이동 시작
        if (whaleMover != null)
        {
            whaleMover.BeginMove();
        }
    }
}
