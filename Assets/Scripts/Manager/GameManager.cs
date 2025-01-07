using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController playerController;

    [Header("Text")]
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject increaseTimeText;
    [SerializeField] GameObject decreaseTimeText;
    [SerializeField] GameObject gameOverText;

    [Header("Time")]
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver = false;

    public bool GameOver => gameOver;

    void Start()
    {
        timeLeft = startTime;
        Time.timeScale = 1f;
    }

    void Update()
    {
        DecreaseTime();
    }

    void DecreaseTime()
    {
        if (gameOver) { return; }

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0)
        {
            PlayerGameOver();
        }
    }

    public void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = .1f;
        Invoke("ReloadLevel", .5f);
    }

    public void IncreaseGameTime(float amount)
    {
        timeLeft += amount;
        StartCoroutine(AnimateFloatingText(increaseTimeText));
    }
    public void DecreaseGameTime(float amount)
    {
        timeLeft -= amount;
        StartCoroutine(AnimateFloatingText(decreaseTimeText));
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }


    IEnumerator AnimateFloatingText(GameObject textObject)
    {
        textObject.SetActive(true);
        TMP_Text text = textObject.GetComponent<TMP_Text>();
        Vector3 startPos = textObject.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 100, 0);

        float duration = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            textObject.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f - (elapsedTime / duration)); // Minska transparens
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textObject.SetActive(false);
        textObject.transform.position = startPos;
    }

}

