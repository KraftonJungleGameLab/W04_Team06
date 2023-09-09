using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabIdleState : BaseState
{
    private MovableObject movableObject;

    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public GrabIdleState(PlayerController controller) : base(controller)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        Controller.animator.SetBool("Grab", true);
        Controller.moveVelocity = Vector3.zero;
        movableObject = (MovableObject)Controller.player.interactableObject;
        movableObject.transform.parent = Controller.transform;

        Controller.animator.SetFloat("GrabHorizontal", 0.0f);
        Controller.animator.SetFloat("GrabVertical", 0.0f);
    }

    public override void OnUpdateState()
    {
        if(CanGrabMove()
            || CanIdle())
        {
            return;
        }
    }

    public override void OnFixedUpdateState()
    {

    }

    public override void OnExitState()
    {
        
    }

    private bool CanGrabMove()
    {
        if (Controller.isGrounded && Controller.isControllable
            && Controller.input.direction != Vector3.zero)
        {
            Controller.player.stateMachine.ChangeState(StateName.GrabMove);
            return true;
        }
        return false;
    }

    private bool CanIdle()
    {
        if (InputData.IsButtonOn(Controller.input.buttonsUp, InputData.INTERACTIONBUTTON)
            || !InputData.IsButtonOn(Controller.input.buttons, InputData.INTERACTIONBUTTON))
        {
            Controller.animator.SetBool("Grab", false);
            movableObject.transform.parent = movableObject.defaultParent;
            Controller.player.stateMachine.ChangeState(StateName.Idle);
            return true;
        }
        return false;
    }
}
