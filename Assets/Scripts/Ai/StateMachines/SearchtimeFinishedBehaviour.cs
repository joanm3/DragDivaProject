using UnityEngine;
using System.Collections;

public class SearchtimeFinishedBehaviour : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    private bool timeWasTriggered = false; 

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Wander wander;
        wander = animator.gameObject.GetComponent<Wander>();
        if (wander.countdown.GetFractionSecondsRemaining() > 5)
            timeWasTriggered = false; 
        if (wander.countdown.GetFractionSecondsRemaining() <= 0.01 && !timeWasTriggered)
        {
            animator.SetTrigger("timeFinished");
            timeWasTriggered = true;
            animator.SetBool("finishedTime", true); 
        }
        if(wander.perspective.tarViewed)
        {
            animator.SetBool("viewingTarget", wander.perspective.tarViewed); 
        }

    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
