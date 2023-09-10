using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : StateMachineBehaviour
{
    private Vector3 playerGrabPosition;
    private Quaternion playerGrabRotation;
    private MovableObject movableObject;
    private PlayerController playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = animator.GetComponentInParent<PlayerController>();
        playerController.SetIsControllable(false);

        movableObject = (MovableObject)playerController.player.interactableObject;
        playerGrabPosition = movableObject.playerGrabPosition + movableObject.transform.position;
        playerGrabPosition.y = playerController.transform.position.y;
        playerGrabRotation = movableObject.playerGrabRotation;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.position = Vector3.Lerp(animator.transform.position, playerGrabPosition, 5.0f * Time.deltaTime);
        Physics.SyncTransforms();
        animator.transform.localRotation = Quaternion.Lerp(animator.transform.localRotation, playerGrabRotation, 5.0f * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("Grab"))
        {
            animator.GetComponentInParent<PlayerController>().SetIsControllable(true);
            animator.transform.parent.position = playerGrabPosition;
            Physics.SyncTransforms();
            animator.transform.localRotation = playerGrabRotation;
            movableObject.transform.parent.parent = playerController.transform;
        }
            
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
