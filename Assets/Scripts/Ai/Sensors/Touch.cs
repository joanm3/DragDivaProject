using UnityEngine;
using System.Collections;

public class Touch : Sense
{

    public bool tarTouched = false;

    //touch colliders
    public float playerIdleTouch = 1;
    public float playerWalkingTouch = 5;
    public float playerRunningTouch = 15;

    private BoxCollider touchCollider;
    [HideInInspector]
    public Vector3 soundPosition; 


    void Start()
    {
        touchCollider = GetComponent<BoxCollider>();

    }

    void Update()
    {
        UpdateColliders(); 
    }

    void UpdateColliders()
    {
        if (PlayerIdle())
            touchCollider.size = new Vector3(playerIdleTouch, 3, playerIdleTouch);
        if (PlayerWalking())
            touchCollider.size = new Vector3(playerWalkingTouch, 3, playerWalkingTouch);
        if (PlayerRunning())
            touchCollider.size = new Vector3(playerRunningTouch, 3, playerRunningTouch); 
    }

    void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            //check the aspect
            if (aspect.aspectName == aspectName)
            {
                soundPosition = other.transform.position; 
                tarTouched = true;
            }
        }
    }

    void OnTriggerUpdate(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            //check the aspect
            if (aspect.aspectName == aspectName)
            {
                soundPosition = other.transform.position;
                tarTouched = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            //check the aspect
            if (aspect.aspectName == aspectName)
            {
                tarTouched = false;
            }
        }
    }

    private bool PlayerIdle()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            return true;
        }
        return false; 
    }

    private bool PlayerWalking()
    {
        if (PlayerIdle())
            return false;
        if (!Input.GetButton("Fire3"))
        {
            return true;
        }
        return false; 
    }

    private bool PlayerRunning()
    {
        if (PlayerIdle() || PlayerWalking())
            return false;
        return true; 
    }



    void OnDrawGizmos()
    {
       // if (!wander.bDebug)
        //    return;

        if (touchCollider)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(
                (touchCollider.size.x * gameObject.transform.localScale.x), 
                (touchCollider.size.y * gameObject.transform.localScale.y), 
                (touchCollider.size.z * gameObject.transform.localScale.z)));
            Gizmos.color = Color.yellow; 
            Gizmos.DrawWireCube(soundPosition, new Vector3 (3,3,3)); 
        }
    }


}




