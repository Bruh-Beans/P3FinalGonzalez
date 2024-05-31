using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // Destroy the obstacle
            Destroy(other.gameObject);
        }
    }
}
