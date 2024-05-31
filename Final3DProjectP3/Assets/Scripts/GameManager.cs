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
    public int lives = 1;
    bool gamePaused = false;
    public GameObject pauseMenuUI;

    private LockCursor lockCursor;

    void Start()
    {
        lockCursor = GameObject.Find("LockCursorManager").GetComponent<LockCursor>();
        lockCursor.Unlock(); // Unlock cursor when on title screen
    }

    void Update()
    {
        PauseMenu();
    }

    public void GameOver()
    {
        isGameActive = false; // Set game active state to false
                              // Add any additional logic to stop gameplay (e.g., stopping movement, disabling player controls)
                              // Example: playerMovement.enabled = false;
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
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
        isGameActive = true; // Set game active when game starts
        titleScreen.SetActive(false);
        UpdateLives(); // Update UI
        SetDifficulty(difficulty); // Set difficulty when the game starts
        lockCursor.Lock(); // Lock cursor when the game starts
    }

    public void UpdateLives()
    {
        if (isGameActive)
        {
            lives--;
            Debug.Log("Lives: " + lives); // Add this line for debugging
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                GameOver(); // Trigger game over only when lives are depleted
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
                spawnRate = 1f;
                animalSpeed = 15.0f;
                break;
            case 3:
                spawnRate = 0.5f;
                animalSpeed = 25.0f;
                break;
            default:
                spawnRate = 0.1f;
                animalSpeed = 30.0f;
                break;
        }

        SpawnManager spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        spawnManager.SetSpawnRate(spawnRate);
        spawnManager.SetAnimalSpeed(animalSpeed);
    }
    public int Lives
    {
        get { return lives; }
        set
        {
            lives = value;
            livesText.text = "Lives: " + lives; // Update UI when lives change
        }
    }
    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gamePaused)
            {
                gamePaused = false;
                Time.timeScale = 1f;
                pauseMenuUI.SetActive(false);
                isGameActive = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
            {
                if (!gamePaused)
                {

                    Time.timeScale = 0f;
                    pauseMenuUI.SetActive(true);
                    gamePaused = true;
                    isGameActive = false;
                }

            }
        }
    }

}
