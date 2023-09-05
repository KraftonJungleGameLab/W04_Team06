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
        Controller.anim.SetInteger("AnimationPar", 1);
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

    }

    public override void OnExitState()
    {
    }

    private bool CanIdle()
    {
        return false;
    }

    private bool CanJump()
    {
        return false;
    }
}
