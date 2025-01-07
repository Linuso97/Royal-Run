using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclesPrefab;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float obstacleSpawnTime = 2;
    [SerializeField] float spawnWidth = 4f;

    float minObstacleSpawnTime = .2f;

    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    public void DecreaseObstacleSpawnTime(float amount)
    {
        obstacleSpawnTime -= amount;

        if (obstacleSpawnTime <= minObstacleSpawnTime)
        {
            obstacleSpawnTime = minObstacleSpawnTime;
        }
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);

            yield return new WaitForSeconds(obstacleSpawnTime);
            Instantiate(obstaclesPrefab[Random.Range(0, obstaclesPrefab.Length)],
                spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
