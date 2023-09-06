using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public JumpState(PlayerController controller) : base(controller)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        Controller.gravityVelocity = Vector3.up * Controller.jumpImpulse;
    }

    public override void OnUpdateState()
    {
        if (CanMove()
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

    private bool CanMove()
    {
        if(Controller.isGrounded
            && Controller.input.direction == Vector3.zero)
        {
            Controller.player.stateMachine.ChangeState(StateName.Move);
            return true;
        }

        return false;
    }

    private bool CanIdle()
    {
        if(Controller.isGrounded
            && Controller.gravityVelocity.y < 0.1f)
        {
            Controller.player.stateMachine.ChangeState(StateName.Idle);
            return true;
        }

        return false;
    }
}
