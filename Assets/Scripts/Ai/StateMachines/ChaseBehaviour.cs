using UnityEngine;
using System.Collections;

public class ChaseBehaviour : StateMachineBehaviour {

    Wander wander;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wander = animator.gameObject.GetComponent<Wander>();
        wander.isChasing = true;
        wander.m_agent.speed = wander.chaseSpeed;
        wander.m_audio.clip = wander.playerDetected;
        wander.m_audio.Play();
        animator.SetBool("finishedTime", false); 
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.gameObject.GetComponent<NavMeshAgent>().SetDestination(wander.m_playerTransform.position);
        wander.m_countdown.ResetTimer(11);
        animator.SetBool("finishedTime", false);
    }


	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wander.m_agent.speed = wander.movementSpeed;
        wander.m_playerLastSeen = wander.m_playerTransform.position;

	}

	
}
