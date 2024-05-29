using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;


    public ParticleSystem explosionParticle;
    public int pointValue;



    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();



    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        // Delete GameOver method
        gameManager.UpdateLives();
        //

    }



}