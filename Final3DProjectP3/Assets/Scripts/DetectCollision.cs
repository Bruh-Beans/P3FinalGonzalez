using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectCollision : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private static int score = 0;
    public static int lives = 1;
    private bool alive = true;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    public ParticleSystem explosionParticle;
    private const string HighScoreKey = "HighScore";
    public AudioClip hitSound;  // Reference to the audio clip for hitting an obstacle
    private AudioSource audioSource;  // Reference to the AudioSource component

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        highScoreText = GameObject.FindWithTag("HighScoreText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateHighScoreDisplay();
        audioSource = GetComponent<AudioSource>();  // Initialize the AudioSource component
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //when animal hits the player they are destroyed and do damage to the player(Takes a life)
        if (other.CompareTag("Player") && alive)
        {
            gameManager.lives--; // Decrease lives count
            Debug.Log("Lives: " + gameManager.lives);
            if (gameManager.lives <= 0)
            {
                gameManager.GameOver();
                alive = false;
                Debug.Log("GameOver");
                CheckAndSetHighScore();
                Destroy(other.gameObject);
            }
        }
        //bone is the tag for the sword projectile. a point is scored when ever the projectile makes contact with a aniaml
        else if (other.CompareTag("Bone"))
        {
            Destroy(gameObject);
            score += 1;
            UpdateScore(0);
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            //Play hit sound
            if (hitSound != null)
            {
                SoundManager.Instance.PlaySound(hitSound);
            }
        }
    }

    // the score updates and is added on top of eachother for a accumulated score
    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // this saves the highscore no matter the time period this helps create the goal for the game
    private void CheckAndSetHighScore()
    {
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save();
        }
        UpdateHighScoreDisplay();
    }

    //This displayes the high score onto the scene through tm pro
    private void UpdateHighScoreDisplay()
    {
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    // the score is reset after every game over, but the highscore is unaffected unless a new highscore was set
    public static void ResetScore()
    {
        score = 0;
    }
}