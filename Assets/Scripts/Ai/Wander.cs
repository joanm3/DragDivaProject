using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour {

    #region STANDARD PARAMETERS
    public float movementSpeed = 5;
    public float chaseSpeed = 7;
    public Paths patrollingPath;
    //debug mode
    public bool bDebug = true;
    #endregion



    #region SENSES COMPONENTS AND PARAMETERS
    public Perspective perspective;
    public Touch touch;
    public bool isChasing = false;
    #endregion  

    #region ANIMATOR COMPONENTS
    //navmeshagent and animator loads
    internal NavMeshAgent agent;
    private Animator animator;
    internal Transform playerTransform;
    //positionofplayerlastseen 
    internal int currentPatrollingIndex = 0;
    internal Vector3 playerLastSeen = new Vector3(100,100,100);
    internal Vector3 randomSearchDest = new Vector3(100, 100, 100);
    private float distanceFromPlayer = 100;
    private float distanceFromPlayerLastSeen = 100;
    //time remaining to stop looking; 
    internal CountdownTimer countdown;
    //restart Perspective
    private int startFieldOfView;
    private int startViewDistance;
    #endregion

    #region AUDIOCLIPS
    internal AudioSource audio;
    public AudioClip soundHeard;
    public AudioClip playerDetected;
    private Vector3 lastPosition;
    private float lastTime;
    private float timeSinceLastMovement;

    #endregion


    void Start()
    {
        WanderGetComponents(); 
        agent.speed = movementSpeed;
        lastPosition = transform.position;
        lastTime = Time.time;
    }

    void Update()
    {
        UpdateAnimator(); 
    }

    void WanderGetComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>(); 
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        countdown = GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>();
        if (!countdown)
            Debug.Log("No CountdownTimer component found for" + gameObject);

        startFieldOfView = perspective.fieldOfView;
        startViewDistance = perspective.viewDistance;
}


    public void RestartPerspective()
    {
        perspective.fieldOfView = startFieldOfView;
        perspective.viewDistance = startViewDistance;
    }

    void UpdateAnimator()
    {
        float timeOfUpdate = Time.time;

        animator.SetBool("viewingTarget", perspective.tarViewed);
        animator.SetBool("touchingTarget", touch.tarTouched);
        distanceFromPlayer = Vector3.Distance(playerTransform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", distanceFromPlayer);
        float distanceFromWayPoint = Vector3.Distance(patrollingPath.ObjectGetPosition(currentPatrollingIndex), transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromWayPoint);
        if (playerLastSeen != null)
        {
            distanceFromPlayerLastSeen = Vector3.Distance(playerLastSeen, transform.position);
            animator.SetFloat("distanceFromLastPlayerSeen", distanceFromPlayerLastSeen);
        }

        if (randomSearchDest != null)
        {
            float distanceFromSearchRandom = Vector3.Distance(randomSearchDest, transform.position);
            animator.SetFloat("distanceFromSearchDest", distanceFromSearchRandom);
        }

        float distanceFromSound = Vector3.Distance(touch.soundPosition, transform.position);
        animator.SetFloat("distanceFromSound", distanceFromSound);

        float deltaTime = timeOfUpdate - lastTime;
        float velocity = Vector3.Magnitude(transform.position - lastPosition) / deltaTime;
        animator.SetFloat("velocity", velocity); 

        if (velocity>0.1)
        {
            timeSinceLastMovement = 0.0f;
        } else {
            timeSinceLastMovement += deltaTime;
        }

        if (timeSinceLastMovement > 5.0f)
        {
            animator.SetTrigger("WanderStagnating");
        }


        lastPosition = transform.position;
        lastTime = timeOfUpdate;
        

    }

    void OnDrawGizmos()
    {
        if (!bDebug) return;
        //randomposition
        Gizmos.color = Color.grey; 
        //playerlastseenposition; 
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(playerLastSeen, 3);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(randomSearchDest, 3);
    }


}
