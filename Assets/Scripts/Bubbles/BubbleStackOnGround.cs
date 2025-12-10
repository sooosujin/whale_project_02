using System.Collections;
using UnityEngine;

// 역할: 바닥(Ground)에 닿으면 조금 있다가 그 자리에서 멈추게만 한다.
public class BubbleStackOnGround : MonoBehaviour
{
    public float stopDelay = 0.2f;   // 바닥에 닿은 후 멈추기까지의 시간

    private bool stopping = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (stopping) return;
        if (!collision.gameObject.CompareTag("Ground")) return;

        stopping = true;
        StartCoroutine(StopRoutine());
    }

    private IEnumerator StopRoutine()
    {
        yield return new WaitForSeconds(stopDelay);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}
