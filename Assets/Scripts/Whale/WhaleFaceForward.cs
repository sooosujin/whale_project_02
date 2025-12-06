using UnityEngine;

public class WhaleFaceForward : MonoBehaviour
{
    // 고래가 도는 중심 오브젝트 (WhaleCenter)
    public Transform center;

    void LateUpdate()
    {
        if (center == null) return;

        // 중심에서 고래까지의 방향(반지름 방향)
        Vector3 radiusDir = transform.position - center.position;
        radiusDir.y = 0f; // 위아래는 무시하고 수평 방향만 사용

        if (radiusDir.sqrMagnitude < 0.0001f) return;

        // 궤도의 진행 방향(접선 방향) 계산
        // 반시계 방향: (-z, 0, x)
        Vector3 tangentDir = new Vector3(-radiusDir.z, 0f, radiusDir.x).normalized;

        // 고래가 진행 방향을 바라보도록 회전
        transform.rotation = Quaternion.LookRotation(tangentDir, Vector3.up);
    }
}
