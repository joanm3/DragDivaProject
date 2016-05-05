using UnityEngine;
using System.Collections;

public class ChaseBehaviour : StateMachineBehaviour {

    Wander wander;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wander = animator.gameObject.GetComponent<Wander>();
        wander.isChasing = true;
        wander.agent.speed = wander.chaseSpeed;
        wander.audio.clip = wander.playerDetected;
        wander.audio.Play();
        animator.SetBool("finishedTime", false); 
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.gameObject.GetComponent<NavMeshAgent>().SetDestination(wander.playerTransform.position);
        wander.countdown.ResetTimer(11);
        animator.SetBool("finishedTime", false);
    }


	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wander.agent.speed = wander.movementSpeed;
        wander.playerLastSeen = wander.playerTransform.position;

	}

	
}
