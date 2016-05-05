using UnityEngine;
using System.Collections;

public class SoundHeardBehaviour : StateMachineBehaviour {

    Wander wander;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wander = animator.gameObject.GetComponent<Wander>();
        wander.audio.clip = wander.soundHeard;
        wander.audio.Play();
        wander.isChasing = false;
        animator.gameObject.GetComponent<NavMeshAgent>().SetDestination(wander.touch.soundPosition);
        

        
    }
    
	
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
 //   {
        
 //       float distanceFromSound = Vector3.Distance(wander.touch.soundPosition, animator.gameObject.transform.position);
 //       animator.SetFloat("distanceFromSound", distanceFromSound);
 //       if (wander.distanceTravelledInLastFiveSeconds < 0.1)
 //           animator.SetTrigger("timeFinished"); 

 //       //waitforseconds(5) -> setTrigger TimeFinished
 //   }


	
}
