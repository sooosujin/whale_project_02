using UnityEngine;

public class WhaleOrbitAround : MonoBehaviour
{
    // 고래가 빙글 도는 중심점 (WhaleCenter)
    public Transform center;

    // 초당 몇 도 회전할지 (양수/음수로 방향 바뀜)
    public float orbitSpeed = 10f;

    void Update()
    {
        if (center == null) return;

        // center.position을 기준으로 Y축을 따라 회전
        transform.RotateAround(center.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
