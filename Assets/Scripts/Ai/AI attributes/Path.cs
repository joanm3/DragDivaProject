using UnityEngine;
using System.Collections;
using System;

public class Path : MonoBehaviour {

    [SerializeField]
    private bool m_bDebug = true;
    public GameObject[] points;
   

    public int objectLength
    {
        get
        {
            return points.Length;
        }

    }

    public GameObject ObjectGetPoint(int index)
    {
        return points[index];

    }

    public Vector3 ObjectGetPosition(int index)
    {
        return points[index].transform.position; 
    }

    internal int getLength()
    {
        return points.Length;
    }

    void OnDrawGizmos()
    {
        if (!m_bDebug) return;
        if (points == null || points.Length <= 1)
            return; 


        for (int i = 0; i < points.Length -1; i++)
        {
            if (points[i] == null)
                return; 
            Debug.DrawLine(points[i].transform.position, points[i + 1].transform.position, Color.red);

        }
        

    }


}
