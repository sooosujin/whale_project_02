using System.Collections;
using UnityEngine;

public class BubbleStackAnimator : MonoBehaviour
{
    public float fallHeight = 3f;
    public float fallDuration = 0.5f;
    public float delayBetween = 0.15f;
    public WhaleMover whaleMover;

    private Transform[] bubbles;
    private Vector3[] targetPositions;

    // ▶ 새로 추가: 마지막에 쌓인 구슬
    private Transform lastBubble;

    void Start()
    {
        int count = transform.childCount;
        bubbles = new Transform[count];
        targetPositions = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            Transform b = transform.GetChild(i);
            bubbles[i] = b;
            targetPositions[i] = b.position;
        }

        StartCoroutine(PlayStackAnimation());
    }

    IEnumerator PlayStackAnimation()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            Transform b = bubbles[i];
            Vector3 target = targetPositions[i];
            Vector3 start = target + Vector3.up * fallHeight;

            b.position = start;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / fallDuration;
                float tt = Mathf.SmoothStep(0f, 1f, t);
                b.position = Vector3.Lerp(start, target, tt);
                yield return null;
            }

            b.position = target;

            // ▶ 마지막에 떨어진 구슬 기억해 두기
            lastBubble = b;

            yield return new WaitForSeconds(delayBetween);
        }

        // ▶ 모든 구슬이 다 떨어지면,
        //    고래에게 "이 구슬을 물고 가"라고 알려주기
        if (whaleMover != null)
        {
            whaleMover.BeginMove();
        }

    }
    
}
