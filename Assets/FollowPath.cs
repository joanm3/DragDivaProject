using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {

    //https://cgcookie.com/archive/noob-to-pro-shader-writing-for-unity-4-beginner/ shaders unity

    [SerializeField]
    private Path m_path;
    [SerializeField]
    private float m_speed = 1f; 
    [SerializeField]
    private float m_distanceToChange = 1f;
    private int m_currentWaypoint = 0; 


	void Update () {


        float distanceFromWayPoint = Vector3.Distance(m_path.ObjectGetPosition(m_currentWaypoint), transform.position);
        transform.LookAt(m_path.ObjectGetPosition(m_currentWaypoint));
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
        if (distanceFromWayPoint < m_distanceToChange)
        {


            if (m_currentWaypoint >= m_path.getLength() - 1)
                m_currentWaypoint = 0;
            else
                m_currentWaypoint++; 
        } 


	
	}
}
