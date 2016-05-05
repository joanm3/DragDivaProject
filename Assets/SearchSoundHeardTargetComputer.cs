using UnityEngine;
using System.Collections;
using System;

public class SearchSoundHeardTargetComputer : FindingNewTargetBehaviour
{
    Wander wander;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wander = animator.gameObject.GetComponent<Wander>();
        base.OnStateEnter(animator, stateInfo, layerIndex);
               
        wander.audio.clip = wander.soundHeard;
        wander.audio.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

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
    public override Vector3 computeNextDestination(Animator animator)
    {
        Vector3 soundPos = animator.gameObject.GetComponent<Wander>().touch.soundPosition;
        wander.randomSearchDest = soundPos;
        return soundPos; 
    }
}
