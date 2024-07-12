using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstacleSpawner : MonoBehaviour
{
  
    
    GameManager manager;

    [SerializeField] float spawnInterval = 2.5f;
    [SerializeField] private ObjectPool objectPool = null;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        objectPool = GameObject.FindObjectOfType<ObjectPool>();

        StartCoroutine(SpawnRoutine());
    }


    IEnumerator SpawnRoutine()
    {
        while (!manager.gameOver)
        {
            objectPool.GetPooledObject();
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void Update()
    {
       
    }

   
}
