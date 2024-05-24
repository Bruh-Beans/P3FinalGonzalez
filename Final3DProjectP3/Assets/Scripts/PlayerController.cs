using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public float turnSpeed = 45.0f;
    public float horizontalInput;
    public float forwardInput;
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Launch a projectile from the player
                Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
            }

        }
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        // Move the vehicle forward based on verticle input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Rotates the car based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
            if (Input.GetKeyDown(switchKey))
            {
                mainCamera.enabled = !mainCamera.enabled;
                hoodCamera.enabled = !hoodCamera.enabled;
            }

        }
    }
}

