using UnityEngine;

public class WhaleOrbit : MonoBehaviour
{
    // 인스펙터 창에서 설정된 값 (현재 5로 설정됨)
    public float orbitSpeed = 10f;   
    
    // 공전할 중심점 (월드 원점(0,0,0)을 중심으로 공전)
    public Vector3 orbitCenter = Vector3.zero;

    void Update()
    {
        // 1. 자신의 Y축(두 번째 인수)을 기준으로 자전하는 대신,
        // 2. orbitCenter(첫 번째 인수)를 중심으로, Vector3.up(Y축, 두 번째 인수)을 회전 축으로 삼아,
        // 3. orbitSpeed 속도(세 번째 인수)로 공전합니다.
        transform.RotateAround(orbitCenter, Vector3.up, orbitSpeed * Time.deltaTime);

        // 추가: 고래가 항상 이동 방향(궤도)을 바라보게 하려면 아래 코드를 추가할 수 있습니다.
        if (GetComponent<Rigidbody>() == null)
        {
            transform.LookAt(orbitCenter);
        }
    }
}