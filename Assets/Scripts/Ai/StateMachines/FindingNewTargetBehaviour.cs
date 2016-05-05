using UnityEngine;
using System.Collections;

public abstract class FindingNewTargetBehaviour : StateMachineBehaviour
{


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NavMeshAgent agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Vector3 vector3 = computeNextDestination(animator);
        agent.SetDestination(vector3) ;
        
        animator.SetTrigger("targetFound");

    }



    public abstract Vector3 computeNextDestination(Animator animator);


}
