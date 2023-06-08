using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject[] objectsToPool;
    public int amountToPool;
    private int _amountToActivate;

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
        
        _amountToActivate++;
        SetObjectsActive(_amountToActivate);
    }

    private void Update()
    {
        if(CheckActiveObjects())
        {
            _amountToActivate++;
            SetObjectsActive(_amountToActivate);
        }
    }

    private GameObject GetPooledObject()
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
    /// if any object is active returns false, otherwise returns true
    /// </summary>
    private bool CheckActiveObjects()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                return false;
            }
        }
        
        return true;
    }
    
    /// <summary>
    /// set a number of inactive objects active
    /// </summary>
    /// <param name="amount"></param>
    private void SetObjectsActive(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (GetPooledObject() != null)
            {
                ActivateObject();
            }
            else
            {
                Debug.Log("There is no more inactive object to set Active");
            }
        }
    }
    
    /// <summary>
    /// manage activation of an inactive object
    /// </summary>
    private void ActivateObject()
    {
        GameObject obj = GetPooledObject();
        obj.SetActive(true);
        obj.transform.position = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        obj.transform.localScale = new Vector3(1, 1, 1);
        Rigidbody objRb = obj.GetComponent<Rigidbody>();
        objRb.velocity = Vector3.zero;
        objRb.maxLinearVelocity = 15;
    }
}
