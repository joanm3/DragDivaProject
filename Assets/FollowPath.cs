using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {

    //https://cgcookie.com/archive/noob-to-pro-shader-writing-for-unity-4-beginner/ shaders unity

    [SerializeField]
    private Paths path;
    [SerializeField]
    private float speed = 1f; 
    [SerializeField]
    private float distanceToChange = 1f;
    private int currentWaypoint = 0; 


	void Update () {


        float distanceFromWayPoint = Vector3.Distance(path.ObjectGetPosition(currentWaypoint), transform.position);
        transform.LookAt(path.ObjectGetPosition(currentWaypoint));
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (distanceFromWayPoint < distanceToChange)
        {


            if (currentWaypoint >= path.getLength() - 1)
                currentWaypoint = 0;
            else
                currentWaypoint++; 
        } 


	
	}
}
