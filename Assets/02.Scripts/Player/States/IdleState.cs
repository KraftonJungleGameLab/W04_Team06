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

    }

    public override void OnUpdateState()
    {
        if (CanMove()
            || CanJump())
        {
            return;
        }
    }

    public override void OnFixedUpdateState()
    {
        // MoveInput을 이용하여 입력을 받음
        Vector3 moveDirection = new Vector3(Controller.input.direction.x, 0.0f, Controller.input.direction.z).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(Controller.transform.eulerAngles.y, targetAngle, Time.deltaTime * 10.0f);
            Controller.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Controller.controller.SimpleMove(moveDirection * Controller.moveSpeed);
            Controller.animator.SetBool("Move", true); // Move 트리거 파라미터를 true로 설정
        }
        else
        {
            Controller.controller.SimpleMove(Vector3.zero);
            Controller.animator.SetBool("Move", false); // Move 트리거 파라미터를 false로 설정
        }
    }

    public override void OnExitState()
    {
    }

    private bool CanMove()
    {

        return false;
    }

    private bool CanJump()
    {
        return false;
    }
}
