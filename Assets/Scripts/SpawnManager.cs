using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;
    private PlayerController playerController;
  public float leftBound=-15.0f;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnObstacle()
    {
        if (!playerController.isGameOver)
        {

            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
        if(transform.position.x<leftBound && gameObject.CompareTag("Obstacle")){
           
            Destroy(gameObject);
        }

    }
}
