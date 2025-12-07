using UnityEngine;

public class WhaleSelfRotate : MonoBehaviour
{
    // 초당 몇 도 회전할지 (양수/음수로 방향 바뀜)
    public float rotateSpeed = 15f;

    void Update()
    {
        // 자기 자신의 Y축을 기준으로 회전
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
