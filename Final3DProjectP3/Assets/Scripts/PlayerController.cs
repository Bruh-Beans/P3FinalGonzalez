using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float speed = 20f;
    public float turnSpeed = 45.0f;
    public float horizontalInput;
    public float forwardInput;
    public Camera hoodCamera;
    public KeyCode switchKey;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);



    }



    // Update is called once per frame
    void Update()
    {
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Launch a projectile from the player
                Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);

            }

            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");
            // Move the Player forward based on verticle input
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            // Rotates the Player based on horizontal input
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
            if (Input.GetKeyDown(switchKey))
            {
              
                hoodCamera.enabled = !hoodCamera.enabled;
                if (Input.GetKeyDown(switchKey))
                {
                    hoodCamera.enabled = !hoodCamera.enabled;
                }

            }
        }
    }
}

