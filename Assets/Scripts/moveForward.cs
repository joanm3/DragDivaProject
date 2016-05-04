using UnityEngine;
using System.Collections;

public class moveForward : MonoBehaviour
{

    [SerializeField]
    private float velocity = 0.1f;

    [SerializeField]
    private bool xAxis = true;

    private float direction = 1;
    private Rigidbody rg;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        //  if (xAxis)
        //     rg.AddRelativeForce(Vector3.right * velocity, ForceMode.Impulse); 
        //   else
        //       rg.AddRelativeForce(Vector3.forward * velocity, ForceMode.Impulse);


    }


    // Update is called once per frame
    void LateUpdate()
    {

        if (xAxis)
            transform.Translate(Vector3.right * velocity);
        else
            transform.Translate(Vector3.forward * velocity);


    }


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision Entered");

        if (collider.gameObject.layer == LayerMask.NameToLayer("Projection") || collider.gameObject.tag == "Mirror")
        {
            if (gameObject.layer == LayerMask.NameToLayer("World"))
                gameObject.layer = LayerMask.NameToLayer("Mirror");
            else if (gameObject.layer == LayerMask.NameToLayer("Mirror"))
                gameObject.layer = LayerMask.NameToLayer("World");

            Debug.Log("layer Projection Entered");

            Vector3 normal = Vector3.zero;
            if (collider.gameObject.layer == LayerMask.NameToLayer("Projection"))
                normal = collider.gameObject.GetComponentInChildren<ControlMirrorCamera>().Normal;

            else if (collider.gameObject.tag == "Mirror")
            {
                normal = collider.gameObject.transform.TransformDirection(collider.gameObject.GetComponent<MeshFilter>().mesh.normals[0]);
            }

            if (normal != null)
            {
                if (xAxis)
                {
                    transform.right = Vector3.Reflect(transform.right, normal) * Vector3.Magnitude(transform.right);
                }
                else
                    transform.forward = Vector3.Reflect(transform.forward, normal) * Vector3.Magnitude(transform.forward);

                Debug.Log(normal);
            }
        }
    }

}
