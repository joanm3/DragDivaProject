using UnityEngine;
using System.Collections;
using System;


public class SearchNextComputer : FindingNewTargetBehaviour
{
    private const int distanceNextPoint = 5;

    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

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
        Wander wander = animator.gameObject.GetComponent<Wander>();


        Vector3 forward = wander.transform.forward;
        Vector3 position = wander.transform.position;

        int randomAngle = UnityEngine.Random.Range(-90, 90);
        Vector3 vector = Quaternion.Euler(0, randomAngle, 0) * forward * distanceNextPoint;

        Vector3 newDest = position + vector;
        newDest.y = position.y;
        wander.randomSearchDest = newDest;

        if (Physics.Raycast(position, vector, vector.magnitude, 1 << LayerMask.NameToLayer("Wall")))
        {
            randomAngle = UnityEngine.Random.Range(-180, 180);
            vector = Quaternion.Euler(0, randomAngle, 0) * forward * distanceNextPoint;
            newDest = position + vector;
            wander.randomSearchDest = newDest;
        }

        return newDest;

    }
}
