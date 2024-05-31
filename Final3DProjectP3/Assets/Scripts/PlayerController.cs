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
    public GameObject rotatingProjectilePrefab; // Add rotating sword projectile prefab
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 20f;  // Speed of the projectile
    public AudioClip fireSound;  // Reference to the audio clip for firing
    private AudioSource audioSource;  // Reference to the AudioSource component
    private GameManager gameManager;
    private LockCursor lockCursor;
    public float minX = 2.12f;
    public float maxX = 256f;
    public float minZ = 18f;
    public float maxZ = 37f;
    public float projectileRotationSpeed = 100f; // Rotation speed of the projectile


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
        //keep player inbounds
        ClampPlayerPosition();
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
            // Launch a normal projectile from the player
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
            // Trigger the throwing animation
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Launch a rotating sword projectile from the player
            GameObject rotatingProjectile = Instantiate(rotatingProjectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody rb = rotatingProjectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = projectileSpawnPoint.forward * projectileSpeed;
            }

            // Play the firing sound
            if (fireSound != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
            // Trigger the throwing animation
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Launch a projectile from the player
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = projectileSpawnPoint.forward * projectileSpeed;
            }

            // Apply rotation to the projectile
            projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.up * projectileRotationSpeed;

            // Play the firing sound
            if (fireSound != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
            // Trigger the throwing animation
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
    
    void ClampPlayerPosition()
    {
        Vector3 position = transform.position;

        // Clamp the player's x position
        if (position.x < minX)
        {
            position.x = minX;
        }
        else if (position.x > maxX)
        {
            position.x = maxX;
        }

        // Clamp the player's z position
        if (position.z < minZ)
        {
            position.z = minZ;
        }
        else if (position.z > maxZ)
        {
            position.z = maxZ;
        }

        // Apply the clamped position back to the player
        transform.position = position;
    }
}
