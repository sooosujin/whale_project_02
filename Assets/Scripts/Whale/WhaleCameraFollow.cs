using UnityEngine;

public class WhaleCameraFollow : MonoBehaviour
{
    public Transform target;      // 고래(고래 단독 모션)
    public Vector3 offset = new Vector3(0f, 2f, -6f);  // 고래 기준 카메라 위치
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 = 고래 위치 + 오프셋(고래의 회전 기준)
        Vector3 desiredPos = target.TransformPoint(offset);

        transform.position = Vector3.Lerp(transform.position, desiredPos,
                                          followSpeed * Time.deltaTime);

        // 항상 고래를 바라보게
        transform.LookAt(target.position);
    }
}
