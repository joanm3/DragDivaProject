using UnityEngine;
using System.Collections;
using System;

public class Wander : MonoBehaviour {

    #region STANDARD PARAMETERS
    public float movementSpeed = 5;
    public float chaseSpeed = 7;
    public Path patrollingPath;
    [SerializeField]
    private float m_health = 100f; 
    //debug mode
    public bool bDebug = true;
    [SerializeField]
    private float m_fireRate = 1f;
    private float m_nextFire;
    #endregion



    #region SENSES COMPONENTS AND PARAMETERS
    public Perspective perspective;
    public Touch touch;
    public bool isChasing = false;
    public ObjectPoolScript enemyBulletsPool;

    #endregion

    #region ANIMATOR COMPONENTS
    //navmeshagent and animator loads
    internal NavMeshAgent m_agent;
    private Animator m_animator;
    internal Transform m_playerTransform;
    //positionofplayerlastseen 
    internal int m_currentPatrollingIndex = 0;
    internal Vector3 m_playerLastSeen = new Vector3(100,100,100);
    internal Vector3 m_randomSearchDest = new Vector3(100, 100, 100);
    private float m_distanceFromPlayer = 100;
    private float m_distanceFromPlayerLastSeen = 100;
    //time remaining to stop looking; 
    internal CountdownTimer m_countdown;
    //restart Perspective
    private int m_startFieldOfView;
    private int m_startViewDistance;
    #endregion

    #region AUDIOCLIPS
    internal AudioSource m_audio;
    public AudioClip soundHeard;
    public AudioClip playerDetected;
    private Vector3 m_lastPosition;
    private float m_lastTime;
    private float m_timeSinceLastMovement;
    #endregion


    void Start()
    {
        WanderGetComponents(); 
        m_agent.speed = movementSpeed;
        m_lastPosition = transform.position;
        m_lastTime = Time.time;
    }

    void Update()
    {
        UpdateAnimator(); 
    }

    void WanderGetComponents()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>(); 
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_countdown = GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>();
        if (!m_countdown)
            Debug.Log("No CountdownTimer component found for" + gameObject);

        m_startFieldOfView = perspective.fieldOfView;
        m_startViewDistance = perspective.viewDistance;
}


    public void RestartPerspective()
    {
        perspective.fieldOfView = m_startFieldOfView;
        perspective.viewDistance = m_startViewDistance;
    }

    void UpdateAnimator()
    {
        float timeOfUpdate = Time.time;

        m_animator.SetBool("viewingTarget", perspective.tarViewed);
        m_animator.SetBool("touchingTarget", touch.tarTouched);
        m_distanceFromPlayer = Vector3.Distance(m_playerTransform.position, transform.position);
        m_animator.SetFloat("distanceFromPlayer", m_distanceFromPlayer);
        float distanceFromWayPoint = Vector3.Distance(patrollingPath.ObjectGetPosition(m_currentPatrollingIndex), transform.position);
        m_animator.SetFloat("distanceFromWaypoint", distanceFromWayPoint);
        if (m_playerLastSeen != null)
        {
            m_distanceFromPlayerLastSeen = Vector3.Distance(m_playerLastSeen, transform.position);
            m_animator.SetFloat("distanceFromLastPlayerSeen", m_distanceFromPlayerLastSeen);
        }

        if (m_randomSearchDest != null)
        {
            float distanceFromSearchRandom = Vector3.Distance(m_randomSearchDest, transform.position);
            m_animator.SetFloat("distanceFromSearchDest", distanceFromSearchRandom);
        }

        float distanceFromSound = Vector3.Distance(touch.soundPosition, transform.position);
        m_animator.SetFloat("distanceFromSound", distanceFromSound);

        float deltaTime = timeOfUpdate - m_lastTime;
        float velocity = Vector3.Magnitude(transform.position - m_lastPosition) / deltaTime;
        m_animator.SetFloat("velocity", velocity); 

        if (velocity>0.1)
        {
            m_timeSinceLastMovement = 0.0f;
        } else {
            m_timeSinceLastMovement += deltaTime;
        }

        if (m_timeSinceLastMovement > 5.0f)
        {
            m_animator.SetTrigger("WanderStagnating");
        }


        m_lastPosition = transform.position;
        m_lastTime = timeOfUpdate;
        

    }

    public bool RemoveEnemyHealth(float value)
    {
        m_health -= value;
        Material material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        float red = material.color.r;
        red += value;
        material.color = new Color(red, material.color.g, material.color.b); 

        if(bDebug)
        Debug.Log("enemy life attacked = " + m_health);
        if (m_health <= 0)
        {
            m_health = 0;
            return true;
        }
        return false;
    }

    internal bool Shoot()
    {

        if (Time.time > m_nextFire)
        {
            m_nextFire = Time.time + m_fireRate;


        transform.LookAt(m_playerTransform);

        GameObject obj = enemyBulletsPool.GetPooledObject();
        if (obj == null)
            return false;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        BulletController bc = obj.GetComponent<BulletController>();
        if (bc != null)
            bc.shooterTransform = this.transform;

        obj.SetActive(true);
        return true;
        }
        return false; 
    }

    void OnDrawGizmos()
    {
        if (!bDebug) return;
        //randomposition
        Gizmos.color = Color.grey; 
        //playerlastseenposition; 
        Gizmos.color = Color.magenta;
      //  Gizmos.DrawSphere(playerTransform.position, 1f); 
        Gizmos.DrawWireSphere(m_playerLastSeen, 3);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_randomSearchDest, 3);
    }


}
