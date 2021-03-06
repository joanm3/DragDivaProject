﻿using UnityEngine;
using System.Collections;

public class GameOverBehaviour : StateMachineBehaviour {

   // GameObject redScreen;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject[] wanders;
        Wander thisWander; 
        RestartPosition restartPlayer;


        wanders = GameObject.FindGameObjectsWithTag("Enemy");
        thisWander = animator.gameObject.GetComponent<Wander>(); 
        restartPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<RestartPosition>();
        //   redScreen = GameObject.FindGameObjectWithTag("GameOver"); 

           restartPlayer.ResetPosition();

        if (wanders != null)
        {
            foreach (GameObject wander in wanders)
            {
                RestartPosition restartWander = wander.GetComponent<RestartPosition>();
                if(restartWander != null)
                restartWander.ResetPosition();
            }
        }


        thisWander.m_countdown.ResetTimer(0);


    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("levelReset");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      //  redScreen.SetActive(false);
    }


}
