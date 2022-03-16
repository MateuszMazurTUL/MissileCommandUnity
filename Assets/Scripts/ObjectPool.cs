using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects; //list of obj in pool

    [SerializeField] GameObject objectToPool; //prefab
    [SerializeField] int amountToPool; //initial sieze of pool

    void Awake()
    {
        pooledObjects = new List<GameObject>();
        
        for (int i = 0; i < amountToPool; i++)
        {
            pooledObjects.Add(createInstantiate());
        } 
    
    }

    //create [amountToPool]*objectToPool default deactive
    GameObject createInstantiate()
    {
        GameObject tmp;
        tmp = Instantiate(objectToPool, this.transform);
        tmp.SetActive(false);
        return tmp;
    }

    //return free object
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }

        //if pool is empty add new object to pool
        GameObject temp = createInstantiate();
        pooledObjects.Add(temp);
        return temp;
    }
}
