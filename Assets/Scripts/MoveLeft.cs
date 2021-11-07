using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 20;
    private PlayerController playerController;
    public float leftBound=-15.0f;
    public SpawnManager spawnManager;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isGameOver)
        {

            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
         if(transform.position.x<leftBound && gameObject.CompareTag("Obstacle")){
            spawnManager.obstacleInstaces.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
