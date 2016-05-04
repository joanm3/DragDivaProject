using UnityEngine;
using System.Collections;

public class moveForward : MonoBehaviour
{

    [SerializeField]
    private float velocity = 0.1f;

    [SerializeField]
    private bool xAxis = true;



    // Update is called once per frame
    void Update()
    {

        if (xAxis)
            transform.position += Vector3.right * velocity * Time.deltaTime;
        else
            transform.position += Vector3.forward * velocity * Time.deltaTime;


    }
}
