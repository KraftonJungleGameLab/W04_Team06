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
        Controller.animator.SetBool("Move", true); // Move 트리거 파라미터를 true로 설정
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
        Vector3 moveDirection = new Vector3(Controller.input.direction.x, 0.0f, Controller.input.direction.z).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(Controller.transform.eulerAngles.y, targetAngle, Time.fixedDeltaTime * 10.0f);
            Controller.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            Controller.moveVelocity = moveDirection * Controller.moveSpeed * Time.fixedDeltaTime;
        }
    }

    public override void OnExitState()
    {
        Controller.animator.SetBool("Move", false); // Move 트리거 파라미터를 false로 설정
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
