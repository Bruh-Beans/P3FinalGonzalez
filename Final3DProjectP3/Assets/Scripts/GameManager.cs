using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winOverText;
    public TextMeshProUGUI highScoreText; // Add this reference in the inspector
    public Button restartButton;
    public Button startButton;
    public GameObject titleScreen;
    public bool isGameActive;
    public int lives;

    private LockCursor lockCursor;

    void Start()
    {
        lockCursor = GameObject.Find("LockCursorManager").GetComponent<LockCursor>();
        lockCursor.Unlock(); // Unlock cursor when on title screen
    }

    void Update()
    {
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        lockCursor.Unlock(); // Unlock cursor when game is over
    }

    public void Win()
    {
        startButton.gameObject.SetActive(true);
        winOverText.gameObject.SetActive(true);
        isGameActive = false;
        lockCursor.Unlock(); // Unlock cursor when game is over
    }

    public void RestartGame()
    {
        DetectCollision.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        lockCursor.Lock(); // Lock cursor when game restarts
    }

    public void StartAgain()
    {
        DetectCollision.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        lockCursor.Lock(); // Lock cursor when game restarts
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        lives++;
        UpdateLives();
        SetDifficulty(difficulty); // Set difficulty when the game starts
        lockCursor.Lock(); // Lock cursor when the game starts

    }

    public void UpdateLives()
    {
        if (isGameActive)
        {
            lives--;
            livesText.text = "Lives: " + lives;
            if (lives == 0)
            {
                GameOver();
            }
        }
    }

    // Set spawn rate and speed based on difficulty
    public void SetDifficulty(int difficulty)
    {
        float spawnRate;
        float animalSpeed;
        switch (difficulty)
        {
            case 1:
                spawnRate = 2.0f;
                animalSpeed = 5.0f;
                break;
            case 2:
                spawnRate = 1.5f;
                animalSpeed = 10.0f;
                break;
            case 3:
                spawnRate = 1.0f;
                animalSpeed = 15.0f;
                break;
            default:
                spawnRate = 1.5f;
                animalSpeed = 10.0f;
                break;
        }

        SpawnManager spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        spawnManager.SetSpawnRate(spawnRate);
        spawnManager.SetAnimalSpeed(animalSpeed);
    }

}
