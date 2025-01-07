using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject levelGenerator;
    [SerializeField] GameManager gameManager;

    [Header("Trip penalties")]
    [SerializeField] float levelMoveSpeed = 3f;
    [SerializeField] float decreaseGameTime = 5f;

    LevelGenerator levelGEN;
    PlayerController playerCTR;
    Animator playerANI;
    CameraController cameraController;

    [Header("Tags and Parameters")]
    const string fenceTag = "Fence";
    const string obstacleTag = "Obstacle";
    const string fenceParameter = "Hit";
    const string obstacleParameter = "HitByObstacle";

    public bool isHit;
    float collisionStartTime = 0f;

    void Start()
    {
        levelGEN = levelGenerator.GetComponent<LevelGenerator>();
        gameManager = gameManager.GetComponent<GameManager>();
        playerCTR = GetComponent<PlayerController>();
        playerANI = GetComponentInChildren<Animator>();
        cameraController = FindFirstObjectByType<CameraController>();

        isHit = false;
    }

    void Update()
    {
        collisionStartTime = Time.time;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == fenceTag)
        {
            // So player can't trip right when starting the game
            if (collisionStartTime < 1) return;

            IsTripped();
            gameManager.DecreaseGameTime(decreaseGameTime);

        }

        else if (collision.gameObject.tag == obstacleTag && !isHit)
        {
            IsHitByObstacle();
        }
    }

    void IsTripped()
    {
        playerANI.SetTrigger(fenceParameter);
        levelGEN.ChangeChunkMoveSpeed(-levelMoveSpeed);
    }

    void IsHitByObstacle()
    {
        isHit = true;
        gameManager.PlayerGameOver();
        levelGEN.minMoveSpeed = 0;
        playerANI.SetTrigger(obstacleParameter);
        playerCTR.ChangePlayerMoveSpeed(-playerCTR.moveSpeed);
        levelGEN.ChangeChunkMoveSpeed(-levelGEN.moveSpeed);
    }
}
