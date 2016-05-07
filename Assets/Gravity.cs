using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{

    [SerializeField]
    private float gravity = 1f;

    private bool isGrounded = false;



    void Update()
    {

        if (!isGrounded)
            transform.position -= new Vector3(0, gravity * Time.deltaTime, 0);

    }

    void OnTriggerEnter()
    {



    }

}
