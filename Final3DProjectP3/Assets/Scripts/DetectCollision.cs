using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetectCollision : MonoBehaviour
{
    private static int score = 0;
    private static int lives = 3;
    private bool alive = true;
    private TextMeshProUGUI scoreText;
    public ParticleSystem explosionParticle;
    void Start()
    {

    }
    void Update()
    {

    }

    //Detects that it is the player and takes away one life, if more lives, GameOver.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && alive)
        {
            Destroy(gameObject);
            lives -= 1;
            Debug.Log("Lives: " + lives);
            if (lives <= 0)
            {
                alive = false;
                Debug.Log("GAME OVER DESU !");
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
        //Detects if it is a projectile, if yes, destroys both entities and gives a score of +1
        else if (other.CompareTag("Bone"))
        {

            Destroy(gameObject);
            score += 1;
            UpdateScore(0);
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void UpdateScore(int scoreToAdd)
    {
        {
            if (scoreText == null) // Correct comparison operator
            {
                // Find the TextMeshProUGUI object by tag
                scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
            }

            // Add the score
            score += scoreToAdd;

            // Update the TextMeshProUGUI text
            scoreText.text = "Score: " + score.ToString(); // Corrected concatenation and ToString() call
        }
    }
}
