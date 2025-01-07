using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Tooltip("Increases timer when hitting a checkpoint")]
    [SerializeField] float increaseTime = 5f;
    [Tooltip("Decreases spawn interval when hitting a checkpoint")]
    [SerializeField] float obstacleDecreaseTime = .25f;

    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;

    const string playerTag = "Player";

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameManager.IncreaseGameTime(increaseTime);
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleDecreaseTime);
        }
    }
}
