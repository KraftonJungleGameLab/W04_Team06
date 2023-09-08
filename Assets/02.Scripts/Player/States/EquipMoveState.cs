using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquipMoveState : BaseState
{
    private EquippableObject equippableObject;

    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public EquipMoveState(PlayerController controller) : base(controller)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        Controller.animator.SetBool("Move", true);
        equippableObject = (EquippableObject)Controller.player.interactableObject;
    }

    public override void OnUpdateState()
    {
        if(CanEquipIdle()
            || CanIdle())
        {
            return;
        }
    }

    public override void OnFixedUpdateState()
    {
        Controller.RotateFixedUpdate();
        Controller.MoveFixedUpdate();
    }

    public override void OnExitState()
    {
        Controller.animator.SetBool("Move", false);
    }

    private bool CanEquipIdle()
    {
        if (Controller.isGrounded
            && Controller.input.direction == Vector3.zero)
        {
            Controller.player.stateMachine.ChangeState(StateName.EquipIdle);
            return true;
        }
        return false;
    }

    private bool CanIdle()
    {
        if (InputData.IsButtonOn(Controller.input.buttonsDown, InputData.INTERACTIONBUTTON))
        {
            equippableObject.transform.parent = equippableObject.defaultParent;
            Controller.player.stateMachine.ChangeState(StateName.Idle);
            return true;
        }
        return false;
    }
}
