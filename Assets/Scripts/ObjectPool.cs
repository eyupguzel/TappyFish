using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> poolObjects;
    [SerializeField] GameObject objectPrefab;
    [SerializeField] int poolSize;

    [SerializeField] float minY;
    [SerializeField] float maxY;

    private void Awake()
    {
        poolObjects = new Queue<GameObject>();
        
        for (int i = 0; i < poolSize; i++)
        {
            
            GameObject obj = Instantiate(objectPrefab);

            obj.SetActive(false);

            poolObjects.Enqueue(obj);
        }
    }
    
    public GameObject GetPooledObject()
    {
        GameObject obj = poolObjects.Dequeue();

        float randomY = Random.Range(minY, maxY);
        Vector2 randomPos = randomPos = new Vector2(transform.position.x, randomY);

        obj.transform.position = randomPos;

        obj.SetActive(true);

        poolObjects.Enqueue(obj);

        return obj;
    }
}
