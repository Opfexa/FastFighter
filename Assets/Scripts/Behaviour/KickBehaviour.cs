using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBehaviour : StateMachineBehaviour
{
    private PlayerController player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
        animator.ResetTrigger("kick");
        player.combo = 0;
        animator.SetBool("Fighting",true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player.combo == 1)
        {
            animator.SetBool("combo",true);
        }
        else if(player.combo == 0)
        {
            animator.SetBool("combo",false);
        }
        player.canAnimation = false;
        animator.ResetTrigger("slowKick");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("kick");
        animator.SetBool("body",false);
        animator.SetBool("head",false);
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
