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
    private BulletType m_thisType;

    [SerializeField]
    private float m_maxDistanceToDestroy = 150f;

    [SerializeField]
    private float m_velocity = 0.1f;

    [SerializeField]
    private bool m_xAxis = true;

    [SerializeField]
    private bool m_bDebug = false;

    [SerializeField]
    private float m_hurtValue = 10f;

    [SerializeField]
    private bool bDebug = false;

    private GameObject m_player;
    private HealthbarScript m_healthBar;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_healthBar = GameObject.FindGameObjectWithTag("HealthController").GetComponent<HealthbarScript>();
        if (m_player == null)
            Debug.Log("Player couldnt be found for" + name);

    }

    void OnEnable()
    {
        gameObject.layer = LayerMask.NameToLayer("World");
    }

    void Update()
    {
        if (m_thisType == BulletController.BulletType.player)
        {
            if (m_player != null)
            {
                float distanceFromPlayer = Vector3.Distance(m_player.transform.position, transform.position);
                if (distanceFromPlayer >= m_maxDistanceToDestroy)
                    Destroy();
            }
        }
        else if (m_thisType == BulletController.BulletType.enemy)
        {
            if (shooterTransform != null)
            {
                float distanceFromEnemy = Vector3.Distance(shooterTransform.position, transform.position);
                if (distanceFromEnemy >= m_maxDistanceToDestroy)
                    Destroy();
            }
        }
    }

    void LateUpdate()
    {
        if (m_xAxis)
            transform.Translate(Vector3.right * m_velocity);
        else
            transform.Translate(Vector3.forward * m_velocity);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (bDebug) ;
        Debug.Log("Bullet Collision Entered");
        OnMirrorTriggerEnter(collider);

        if (m_thisType == BulletType.player)
            OnEnemyTriggerEnter(collider);
        else if (m_thisType == BulletType.enemy)
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

            if (m_bDebug)
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
                if (m_xAxis)
                {
                    transform.right = Vector3.Reflect(transform.right, normal) * Vector3.Magnitude(transform.right);
                }
                else
                    transform.forward = Vector3.Reflect(transform.forward, normal) * Vector3.Magnitude(transform.forward);
                if (m_bDebug)
                    Debug.Log(normal);
            }
        }
    }

    private void OnEnemyTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {

            //Code to hurt the enemy
            if (bDebug)
                Debug.Log("bullet enemy touched");
            Wander wander = collider.GetComponentInParent<Wander>();
            if (wander != null)
            {
                if (wander.RemoveEnemyHealth(m_hurtValue))
                    collider.gameObject.GetComponentInParent<Animator>().SetTrigger("wandererDied");

            }
            Destroy();

        }

    }

    private void OnPlayerTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

            //Code to hurt the player
            if (bDebug)
                Debug.Log("player touched");
            m_healthBar.RemoveHealth(m_hurtValue);
            Destroy();

        }

    }


}
