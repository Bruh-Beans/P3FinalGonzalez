using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float speed = 20f;
    public float turnSpeed = 45.0f;
    public float horizontalInput;
    public float forwardInput;
    public Camera hoodCamera;
    public Camera mainCamera;
    public KeyCode switchKey;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 20f;  // Speed of the projectile
    public AudioClip fireSound;  // Reference to the audio clip for firing
    private AudioSource audioSource;  // Reference to the AudioSource component
    private GameManager gameManager;
    private LockCursor lockCursor;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lockCursor = GameObject.Find("LockCursorManager").GetComponent<LockCursor>();
        audioSource = GetComponent<AudioSource>();  // Initialize the AudioSource component
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))  // Obstacles have the tag "Obstacle"
        {
            // Decrease player's lives
            gameManager.UpdateLives();
            // Remove the obstacle
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Win"))  // Assuming win trigger has the tag "Win"
        {
            gameManager.Win();
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("Win");
        }
    }


    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleCameraSwitch();
    }

    void HandleMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move the Player forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Rotates the Player based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Launch a projectile from the player
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = projectileSpawnPoint.forward * projectileSpeed;
            }

            // Play the firing sound
            if (fireSound != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
        }
    }

    void HandleCameraSwitch()
    {
        if (Input.GetKeyDown(switchKey))
        {
            hoodCamera.enabled = !hoodCamera.enabled;
            mainCamera.enabled = !mainCamera.enabled;
        }
    }
}
