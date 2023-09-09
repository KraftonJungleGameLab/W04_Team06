using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveState : BaseState
{
    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public MoveState(PlayerController controller) : base(controller)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        //Controller.animator.SetBool("Move", true); // Move 트리거 파라미터를 true로 설정
    }

    public override void OnUpdateState()
    {
        if(CanJump()
            || CanIdle())
        {
            return;
        }
    }

    public override void OnFixedUpdateState()
    {
        Controller.RotateFixedUpdate();

        if(Controller.isGrounded)
            Controller.MoveFixedUpdate();
    }

    public override void OnExitState()
    {
        //Controller.animator.SetBool("Move", false); // Move 트리거 파라미터를 false로 설정
    }

    private bool CanIdle()
    {
        if(Controller.isGrounded
            && Controller.input.direction == Vector3.zero)
        {
            Controller.player.stateMachine.ChangeState(StateName.Idle);
            return true;
        }
        return false;
    }

    private bool CanJump()
    {
        if (Controller.isGrounded
            && InputData.IsButtonOn(Controller.input.buttonsDown, InputData.JUMPBUTTON))
        {
            Controller.player.stateMachine.ChangeState(StateName.Jump);
            return true;
        }
        return false;
    }
}
