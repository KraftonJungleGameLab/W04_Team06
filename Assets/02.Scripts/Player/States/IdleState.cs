using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class IdleState : BaseState
{
    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public IdleState(PlayerController controller) : base(controller)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        Controller.moveVelocity = Vector3.zero;
    }

    public override void OnUpdateState()
    {
        if (CanInteract()
            || CanMove()
            || CanJump())
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

    private bool CanMove()
    {
        if (Controller.isGrounded && Controller.isControllable
            && Controller.input.direction != Vector3.zero)
        {
            Controller.player.stateMachine.ChangeState(StateName.Move);
            return true;
        }
        return false;
    }

    private bool CanJump()
    {
        if (Controller.isGrounded && Controller.isControllable
            && InputData.IsButtonOn(Controller.input.buttonsDown, InputData.JUMPBUTTON))
        {
            Controller.player.stateMachine.ChangeState(StateName.Jump);
            return true;
        }

        return false;
    }

    private bool CanInteract()
    {
        if (Controller.isGrounded && Controller.isControllable
            && Controller.player.interactableObject != null
            && InputData.IsButtonOn(Controller.input.buttonsDown, InputData.INTERACTIONBUTTON))
        {
            Controller.player.stateMachine.ChangeState(Controller.player.interactableObject.GetInteractState());
            return true;
        }

        return false;
    }
}
