using UnityEngine;

public class WhaleOrbit : MonoBehaviour
{
    // 초당 몇 도 회전할지 (시계 방향/반시계 방향 조절)
    public float orbitSpeed = 20f;

    void Update()
    {
        // Y축을 기준으로 계속 회전
        transform.Rotate(0f, orbitSpeed * Time.deltaTime, 0f);
    }
}
