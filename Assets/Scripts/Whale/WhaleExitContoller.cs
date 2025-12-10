using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleExitController : MonoBehaviour
{
    [Header("Whale Settings")]
    public Transform whaleRoot;       // 고래 전체를 대표하는 Transform
    public Transform carryPoint;      // 버블들을 붙일 위치 (고래 자식)

    [Header("Exit Settings")]
    public float delayBeforeExit = 1f;  // 버블 붙인 후 잠시 멈추는 시간
    public float exitSpeed = 3f;        // 씬 밖으로 나가는 속도

    private bool isExiting = false;

    // BubbleController에서 25개 생성 후 호출할 함수
    public void StartExit(List<Transform> bubbles)
    {
        if (whaleRoot == null || carryPoint == null)
        {
            Debug.LogError("WhaleExitController: whaleRoot 또는 carryPoint가 비어 있습니다.");
            return;
        }

        // 1) 모든 버블을 고래의 carryPoint에 붙인다
        foreach (Transform b in bubbles)
        {
            if (b != null)
            {
                b.SetParent(carryPoint, true);  // true → 현재 위치 유지
            }
        }

        // 2) 잠시 있다가 나가기 시작
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        yield return new WaitForSeconds(delayBeforeExit);
        isExiting = true;
    }

    private void Update()
    {
        if (isExiting && whaleRoot != null)
        {
            // 월드 기준으로 오른쪽 방향으로 이동 (카메라 밖으로 나가게)
            whaleRoot.Translate(Vector3.right * exitSpeed * Time.deltaTime, Space.World);
        }
    }
}
