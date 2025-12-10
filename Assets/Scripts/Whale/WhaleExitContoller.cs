using UnityEngine;

public class WhaleExitController : MonoBehaviour
{
    [Header("Whale Move")]
    public Transform whaleRoot;          // 고래 루트 (WhaleCenter_Old)
    public float exitSpeed = 3f;         // 나가는 속도
    public Vector3 exitDirection = new Vector3(1f, 0f, 0f); // 오른쪽으로 나가기

    [Header("Tail Target")]
    public Transform tailTarget;         // 고래 꼬리 끝 (WhaleTailTarget)

    private bool isExiting = false;

    public void StartExit()
    {
        if (whaleRoot == null)
        {
            Debug.LogError("WhaleExitController: whaleRoot가 비어 있습니다.");
            return;
        }

        isExiting = true;

        // ★ 현재 씬에 있는 모든 구슬에게 "꼬리를 따라와라" 신호 보내기
        BubbleExitFollower[] bubbles = FindObjectsOfType<BubbleExitFollower>();
        foreach (var b in bubbles)
        {
            b.BeginFollow(tailTarget);
        }

        Debug.Log("[WhaleExit] StartExit → 고래 이동 + 구슬 꼬리 따라가기 시작");
    }

    private void Update()
    {
        if (!isExiting || whaleRoot == null) return;

        whaleRoot.Translate(exitDirection.normalized * exitSpeed * Time.deltaTime, Space.World);
    }
}
