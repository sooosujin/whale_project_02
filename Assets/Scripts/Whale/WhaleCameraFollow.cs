using UnityEngine;

public class WhaleCameraFollow : MonoBehaviour
{
    public Transform target;      // 고래
    public Vector3 offset = new Vector3(0f, 1.5f, -8f);  // 고래 기준 카메라 위치
    public float smooth = 3f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smooth * Time.deltaTime);

        // 항상 고래 쪽을 보게
        transform.LookAt(target.position);
    }
}
