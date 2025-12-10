using System.Collections;
using UnityEngine;

public class WhaleExitController : MonoBehaviour
{
    [Header("Whale & Bubbles")]
    public Transform whaleRoot;   // 고래 루트 (WhaleCenter_Old)
    public Transform carryPoint;  // BubbleCarryPoint (고래 자식)
    public Transform pileCenter;  // BubblePileCenter (바닥의 구슬 더미)

    [Header("Collect Motion")]
    public float collectDuration = 2f;  // 구슬 더미가 위로 올라오는 시간

    [Header("Exit Motion")]
    public float delayBeforeExit = 1f;  // 들고 난 뒤 잠시 멈춤
    public float exitSpeed = 3f;        // 나가는 속도
    public Vector3 exitDirection = new Vector3(1f, 0f, 0f); // 오른쪽으로 나가기

    private bool isExiting = false;

    public void StartExit()
    {
        if (whaleRoot == null || carryPoint == null || pileCenter == null)
        {
            Debug.LogError("WhaleExitController: whaleRoot / carryPoint / pileCenter 중 비어 있는 값이 있습니다.");
            return;
        }

        Debug.Log("[WhaleExit] StartExit 호출됨 → CollectAndExitRoutine 시작");
        StartCoroutine(CollectAndExitRoutine());
    }

    private IEnumerator CollectAndExitRoutine()
    {
        // 1단계: 바닥 → 고래 배까지 구슬 더미 올리기
        Vector3 startPos = pileCenter.position;
        Vector3 targetPos = carryPoint.position;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / collectDuration;
            t = Mathf.Clamp01(t);

            pileCenter.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        // 고래 몸에 완전히 붙이기
        pileCenter.SetParent(carryPoint, true);

        // 2단계: 잠시 멈춤
        yield return new WaitForSeconds(delayBeforeExit);

        // 3단계: 고래 퇴장 시작
        isExiting = true;
    }

    private void Update()
    {
        if (!isExiting || whaleRoot == null) return;

        whaleRoot.Translate(exitDirection.normalized * exitSpeed * Time.deltaTime, Space.World);
    }
}
