using UnityEngine;
using System.Collections;

public class RestartPosition : MonoBehaviour {

    private Vector3 startPosition; 

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position; 
	
	}
	

    public void ResetPosition()
    {
        gameObject.SetActive(true); 

        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent == null)
            transform.position = startPosition;
        else
            agent.Warp(startPosition); 
    }
}
