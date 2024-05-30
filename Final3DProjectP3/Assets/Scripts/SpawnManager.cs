using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 20;
    private float spawnRangeZ = 20;
    private float startDelay = 1;
    // the shorter the interval, the fster the spawn rate
    private float spawnInterval;
    // BOTH OF THESE VARIABLES WILL CHANGE WITH THE DIFFICULTY SCRIPT
    // Variable for animal speed
    private float animalSpeed;

    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    void Update()
    {
    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        GameObject animal = Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        Rigidbody animalRb = animal.GetComponent<Rigidbody>();
        if (animalRb != null)
        {
            animalRb.velocity = animal.transform.forward * animalSpeed; // Ensure animals move forward relative to their orientation
        }
    }

    public void SetSpawnRate(float newSpawnRate)
    {
        CancelInvoke("SpawnRandomAnimal");
        spawnInterval = newSpawnRate;
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    public void SetAnimalSpeed(float newAnimalSpeed)
    {
        animalSpeed = newAnimalSpeed;
    }
}
