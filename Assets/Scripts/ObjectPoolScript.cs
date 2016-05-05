using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolScript : MonoBehaviour
{

    public GameObject[] objectsPooled;
    public int pooledAmount = 20;
    public bool willGrow = false;

    public List<GameObject> pooledObjects;


    void Start()
    {

        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {

                int randomObject = Random.Range(0, objectsPooled.Length);
                GameObject obj = (GameObject)Instantiate(objectsPooled[randomObject]);
                obj.transform.parent = gameObject.transform;
                pooledObjects.Add(obj);
                obj.SetActive(false);


        }

    }

    public GameObject GetPooledObject()
    {

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            int randomObject = Random.Range(0, objectsPooled.Length);
            GameObject obj = (GameObject)Instantiate(objectsPooled[randomObject]);
            obj.transform.parent = gameObject.transform;
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }


}
