using System.Collections;
using UnityEngine;

public class BubbleStackAnimator : MonoBehaviour
{
    public float fallHeight = 3f;       // 얼마나 위에서 떨어질지 (Y 방향)
    public float fallDuration = 0.5f;   // 한 개가 떨어지는 시간
    public float delayBetween = 0.15f;  // 다음 구슬까지 대기 시간
    public WhaleMover whaleMover;       // 고래 움직임 스크립트

    private Transform[] bubbles;        // 자식 구슬들
    private Vector3[] targetPositions;  // 원래 자리

    void Start()
    {
        int count = transform.childCount;
        bubbles = new Transform[count];
        targetPositions = new Vector3[count];

        // 자식 구슬들 정보 저장
        for (int i = 0; i < count; i++)
        {
            Transform b = transform.GetChild(i);
            bubbles[i] = b;
            targetPositions[i] = b.position;
        }

        // 애니메이션 시작
        StartCoroutine(PlayStackAnimation());
    }

    IEnumerator PlayStackAnimation()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            Transform b = bubbles[i];
            Vector3 target = targetPositions[i];
            Vector3 start = target + Vector3.up * fallHeight;

            // 시작 위치를 위로 올려 놓기
            b.position = start;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / fallDuration;
                float tt = Mathf.SmoothStep(0f, 1f, t); // 살짝 부드럽게
                b.position = Vector3.Lerp(start, target, tt);
                yield return null;
            }

            b.position = target;
            yield return new WaitForSeconds(delayBetween);
        }

        // 모든 구슬 쌓기 끝나면 고래 출발
        if (whaleMover != null)
        {
            whaleMover.BeginMove();
        }
    }
}
