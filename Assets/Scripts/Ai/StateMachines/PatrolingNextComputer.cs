using UnityEngine;
using System.Collections;
using System;

public class PatrolingNextComputer : FindingNewTargetBehaviour
{

    public override Vector3 computeNextDestination(Animator animator)
    {

        Wander wander = animator.gameObject.GetComponent<Wander>();
        Path path = wander.patrollingPath;
        int nextIndex = wander.m_currentPatrollingIndex+1;
        if (nextIndex >= path.getLength())
        {
            nextIndex = 0;
        }
        if (!PathExists(path, animator.gameObject))
            return new Vector3(0, 0, 0);
        Vector3 vector3 = path.ObjectGetPosition(nextIndex);
        wander.m_currentPatrollingIndex = nextIndex;
        return vector3;
    }

    private bool PathExists(Path path, GameObject gameObject)
    {
        if (path == null)
        {
            Debug.Log("Path component not attached to" + gameObject);
            return false;
        }
        return true;
    }


}
