using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    public enum BulletType
    {
        player, enemy
    }

    [HideInInspector]
    public Transform shooterTransform;

    [SerializeField]
    private BulletType thisType;

    [SerializeField]
    private float maxDistanceToDestroy = 150f;

    [SerializeField]
    private float velocity = 0.1f;

    [SerializeField]
    private bool xAxis = true;

    [SerializeField]
    private bool bDebug = false;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.Log("Player couldnt be found for" + name);

    }

    void OnEnable()
    {
        gameObject.layer = LayerMask.NameToLayer("World");
    }

    void Update()
    {
        if (thisType == BulletController.BulletType.player)
        {
            if (player != null)
            {
                float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
                if (distanceFromPlayer >= maxDistanceToDestroy)
                    Destroy();
            }
        }
        else if (thisType == BulletController.BulletType.enemy)
        {
            if (shooterTransform != null)
            {
                float distanceFromEnemy = Vector3.Distance(shooterTransform.position, transform.position);
                if (distanceFromEnemy >= maxDistanceToDestroy)
                    Destroy();
            }
        }
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

        if (thisType == BulletType.player)
            OnEnemyTriggerEnter(collider);
        else if (thisType == BulletType.enemy)
            OnPlayerTriggerEnter(collider);

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

            if (bDebug)
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
                if (bDebug)
                    Debug.Log(normal);
            }
        }
    }

    private void OnEnemyTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {

            //Code to hurt the enemy
            Debug.Log("enemy touched");
            Destroy();

        }

    }

    private void OnPlayerTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

            //Code to hurt the enemy
            Debug.Log("player touched");
            Destroy();

        }

    }


}
