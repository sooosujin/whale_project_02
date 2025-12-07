using System.Collections;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public Transform[] spawnPoints; 
    public float spawnInterval = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        foreach (var point in spawnPoints)
        {
            Instantiate(spherePrefab, point.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
