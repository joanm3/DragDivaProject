using UnityEngine;
using System.Collections;
using System;

public class Paths : MonoBehaviour {

    public bool bDebug = true;
    public float Radius = 2.0f;
    public GameObject[] objectPointA;
   

    public int objectLength
    {
        get
        {
            return objectPointA.Length;
        }

    }

    public GameObject ObjectGetPoint(int index)
    {
        return objectPointA[index];

    }

    public Vector3 ObjectGetPosition(int index)
    {
        return objectPointA[index].transform.position; 
    }

    internal int getLength()
    {
        return objectPointA.Length;
    }

    void OnDrawGizmos()
    {
        if (!bDebug) return;

        for (int i = 0; i < objectPointA.Length -1; i++)
        {
            Debug.DrawLine(objectPointA[i].transform.position, objectPointA[i + 1].transform.position, Color.red);

        }
        

    }


}
