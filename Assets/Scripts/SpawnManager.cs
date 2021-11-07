using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs=new List<GameObject>();
    public List<GameObject> obstacleInstaces=new List<GameObject>();
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;
    private PlayerController playerController;
    
  
    void Start()
    {   
           foreach (var obj in obstaclePrefabs)
        {
           
            obj.GetComponent<MoveLeft>().speed =20;
            

        }
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnObstacle()
    {   
        int index=Random.Range(0,obstaclePrefabs.Count);
        if (!playerController.isGameOver)
        {
            GameObject spawnObj=obstaclePrefabs[index];
            if(spawnObj.CompareTag("MediumObstacle")){
                playerController.doubleJump=true;
            }
           GameObject instance= Instantiate(spawnObj, spawnPos, spawnObj.transform.rotation);
           obstacleInstaces.Add(instance);
          
        }
       

    }
}
