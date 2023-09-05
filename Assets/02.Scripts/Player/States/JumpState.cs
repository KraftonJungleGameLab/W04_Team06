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

    }

    public override void OnUpdateState()
    {
        if (CanIdle())
        {
            return;
        }
    }

    public override void OnFixedUpdateState()
    {

    }

    public override void OnExitState()
    {
        Controller.superJumpTimer = 0.0f;
    }

    private bool CanIdle()
    {
        return false;
    }
}
