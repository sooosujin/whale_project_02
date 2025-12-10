using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class BubbleExitFollower : MonoBehaviour
{
    [Header("Follow Settings")]
    public float startDelayMin = 0f;     // 꼬리를 따라가기 시작 전 최소 딜레이
    public float startDelayMax = 1.5f;   // 최대 딜레이 (시간차 느낌)
    public float followSpeed = 3f;       // 꼬리 방향으로 붙는 속도
    public float heightOffset = 0f;      // 꼬리 기준 위/아래 오프셋

    [Header("Fade Out")]
    public float fadeDuration = 1.5f;    // 사라지는 데 걸리는 시간

    private bool isFollowing = false;
    private Transform target;
    private Material mat;
    private Color baseColor;

    public void BeginFollow(Transform tailTarget)
    {
        if (isFollowing) return;

        target = tailTarget;
        if (target == null) return;

        if (!gameObject.activeInHierarchy) return;

        StartCoroutine(FollowRoutine());
    }

    private IEnumerator FollowRoutine()
    {
        isFollowing = true;

        // 1) 시작 전 랜덤 딜레이
        float delay = Random.Range(startDelayMin, startDelayMax);
        yield return new WaitForSeconds(delay);

        // 물리 끄고 직접 움직이기
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        Renderer renderer = GetComponent<Renderer>();
        mat = renderer.material;          // 인스턴스 머티리얼
        baseColor = mat.color;

        float t = 0f;

        while (target != null && t < fadeDuration)
        {
            // 위치를 꼬리 쪽으로 서서히 끌어당김
            Vector3 targetPos = target.position + Vector3.up * heightOffset;
            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                Time.deltaTime * followSpeed
            );

            // 알파 서서히 줄이기
            t += Time.deltaTime;
            float a = Mathf.Clamp01(1f - (t / fadeDuration));
            Color c = baseColor;
            c.a = a;
            mat.color = c;

            yield return null;
        }

        // 최종 제거
        Destroy(gameObject);
    }
}
