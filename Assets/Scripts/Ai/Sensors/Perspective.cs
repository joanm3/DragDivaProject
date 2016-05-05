using UnityEngine;
using System.Collections;

public class Perspective : Sense
{
    public int fieldOfView = 45;
    public int viewDistance = 100;
    public bool tarViewed = false;

    private Transform playerTrans;
    private Vector3 rayDirection;
    internal Animator animator; 




    protected override void Initialize()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>(); 
        tarViewed = false;
}

    // Update is called once per frame
    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
            DetectAspect();
    }

    //Detect perspective field of view for the AI Character
    void DetectAspect()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; 
        RaycastHit hit;
        rayDirection = playerPosition - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectName == aspectName)
                    {
                        tarViewed = true;
                        animator.SetBool("viewingTarget", tarViewed); 
                        return;

                    } 
                }
            }
        }
        
            tarViewed = false;
        
    }

    /// <summary>
    /// Show Debug Grids and obstacles inside the editor
    /// </summary>
    void OnDrawGizmos()
    {
        if (playerTrans == null)
            return;
        if (!wander.bDebug)
            return;

        Debug.DrawLine(transform.position, playerTrans.position, Color.yellow);

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);

        //Approximate perspective visualization

        Vector3 leftRayDirection = Quaternion.AngleAxis(fieldOfView, new Vector3(0,1,0)) * transform.forward;
        Vector3 rightRayDirection = Quaternion.AngleAxis(-fieldOfView, new Vector3(0, 1, 0)) * transform.forward;
        //Vector3 leftRayPoint = frontRayPoint;
        //leftRayPoint.x += FieldOfView * 0.5f;

        Vector3 leftRayPoint = transform.position + (leftRayDirection * viewDistance);
        Vector3 rightRayPoint = transform.position + (rightRayDirection * viewDistance);
        //Vector3 rightRayPoint = frontRayPoint;
        // rightRayPoint.x -= FieldOfView * 0.5f;

        Gizmos.DrawSphere(playerTrans.position, 1f); 
        Debug.DrawLine(transform.position, frontRayPoint, Color.red);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.blue);
    }
}
