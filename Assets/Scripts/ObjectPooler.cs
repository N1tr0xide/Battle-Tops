using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject[] objectsToPool;
    public int amountToPool;

    [SerializeField] private float spawnRangeX = 9f;
    [SerializeField] private float spawnRangeZ = 9f;
    
    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectsToPool[Random.Range(0,objectsToPool.Length)], this.transform, true);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetObjectsActive(3);
        }
    }

    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }

    /// <summary>
    /// set all inactive objects active
    /// </summary>
    void SetObjectsActive()
    {
        while (GetPooledObject() != null)
        {
            ActivateObject();
        }
    }
    
    /// <summary>
    /// set a number of inactive objects active
    /// </summary>
    /// <param name="amount"></param>
    void SetObjectsActive(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (GetPooledObject() != null)
            {
                ActivateObject();
            }
            else
            {
                Debug.Log("There is no inactive object to set Active");
            }
        }
    }

    /// <summary>
    /// manage activation of an inactive object
    /// </summary>
    void ActivateObject()
    {
        GameObject obj = GetPooledObject();
        obj.transform.position = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.SetActive(true);
    }
}
