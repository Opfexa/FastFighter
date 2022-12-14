using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBehaviour : StateMachineBehaviour
{
    private PlayerController player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
        animator.ResetTrigger("punch");
        player.pCombo = 0;
        animator.SetBool("change",false);
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("StraightRight"))
        animator.ResetTrigger("kick");
        player.punch = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player.pCombo == 1)
        {
            animator.SetBool("pCombo",true);
        }
        else if(player.pCombo == 0)
        {
            animator.SetBool("pCombo",false);
        }
        player.canAnimation = false;
        animator.ResetTrigger("slowKick");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("punch");   
        animator.SetBool("body",false);
        animator.SetBool("head",false);
        animator.SetBool("block",false);
        if(player.kick) animator.Play("MmaKick",-1,0f);
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
