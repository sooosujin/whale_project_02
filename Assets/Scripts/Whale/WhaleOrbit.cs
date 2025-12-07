using UnityEngine;

public class WhaleOrbit : MonoBehaviour
{
    public float orbitSpeed = 5f;   // 1초에 몇 도 도는지

    void Update()
    {
        // 월드 기준 Y축으로 회전
        transform.Rotate(0f, orbitSpeed * Time.deltaTime, 0f, Space.World);
    }
}
