using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs = new GameObject[2];
    [SerializeField] float obstacleSpawnTime = 1f;
    [SerializeField] GameObject obstacleParent;
    [SerializeField] float spawnWidth = 4f;
    
    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            yield return new WaitForSeconds(obstacleSpawnTime);
            Instantiate(obstacle, spawnPosition, Random.rotation, obstacleParent.transform);
        }
    }
}
