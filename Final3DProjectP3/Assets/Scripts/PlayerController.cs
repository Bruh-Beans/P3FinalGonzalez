using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

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
    private GameManager gameManager;
    private LockCursor lockCursor;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lockCursor = GameObject.Find("LockCursorManager").GetComponent<LockCursor>();
    }
        // Initialize the game manager or other necessary components here
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))  //  obstacles have the tag "Obstacle"
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            lockCursor.Unlock();
        }
        else if (other.CompareTag("Win"))  // Assuming win trigger has the tag "Win"
        {
            gameManager.Win();
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("Win");
        }
    }

    // Update is called once per frame
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
