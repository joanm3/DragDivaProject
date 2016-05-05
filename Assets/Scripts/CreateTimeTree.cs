using UnityEngine;
using System.Collections;

public class CreateTimeTree : MonoBehaviour
{

    public float minLocalX = -10;
    public float maxLocalX = 10;

    public bool LeftOfTheRoad; 


    public bool invokeRepetition = false;


    public ObjectPoolScript thisPool;

    void Start()
    {
        if (thisPool == null)
            thisPool = GetComponent<ObjectPoolScript>();

        if (invokeRepetition)
            InvokeRepeating("Fire", 2, 2);
        else
        {

            StartCoroutine("ContinueFire"); 

        }


    }

    IEnumerator ContinueFire()
    {
        while(!Fire())
        {

            yield return new WaitForSeconds(1); 
        }
        
    }



    public bool Fire()
    {
        GameObject obj = thisPool.GetPooledObject();
      //  Debug.Log(name + " " + obj);
        if (obj == null)
            return false;

            float randomPosition = Random.Range(minLocalX, maxLocalX);
            obj.transform.position = new Vector3(transform.position.x + randomPosition, obj.transform.position.y, obj.transform.position.z);

        obj.SetActive(true);
        return true;
    }

}
