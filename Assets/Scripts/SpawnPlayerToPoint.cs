using UnityEngine;
using System.Collections;

public class SpawnPlayerToPoint : MonoBehaviour
{

    public Transform spawnPoint; 

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.position = spawnPoint.position; 
        }

    }
}
