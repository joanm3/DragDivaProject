using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float maxDistanceToDestroy = 150f; 

    [SerializeField]
    private float velocity = 0.1f;

    [SerializeField]
    private bool xAxis = true;

    [SerializeField]
    private bool bDebug = false; 

    private float direction = 1;
    private Rigidbody rg;
    private GameObject player; 

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.Log("Player couldnt be found for" + name); 

    }

    void OnEnable()
    {


    }

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (player != null && distanceFromPlayer >= maxDistanceToDestroy)
            Destroy(); 

    }

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
        OnMirrorTriggerEnter(collider);
        OnEnemyTriggerEnter(collider); 
        
    }


    public void Destroy()
    {
        gameObject.layer = LayerMask.NameToLayer("World");
        gameObject.SetActive(false);
    }

    private void OnMirrorTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Projection") || collider.gameObject.tag == "Mirror")
        {
            if (gameObject.layer == LayerMask.NameToLayer("World"))
                gameObject.layer = LayerMask.NameToLayer("Mirror");
            else if (gameObject.layer == LayerMask.NameToLayer("Mirror"))
                gameObject.layer = LayerMask.NameToLayer("World");

            if(bDebug)
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
                if(bDebug)
                Debug.Log(normal);
            }
        }
    }

    private void OnEnemyTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {

            //Code to hurt the enemy

            Destroy(); 

        }

    }


}
