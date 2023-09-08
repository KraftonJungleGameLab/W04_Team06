using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquipIdleState : BaseState
{
    private EquippableObject equippableObject;

    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public EquipIdleState(PlayerController controller) : base(controller)
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
        equippableObject = (EquippableObject)Controller.player.interactableObject;
        equippableObject.transform.parent = Controller.animator.transform;
        equippableObject.transform.localPosition = equippableObject.equipLocalPosition;
    }

    public override void OnUpdateState()
    {
        if(CanEquipMove()
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

    private bool CanEquipMove()
    {
        if (Controller.isGrounded
            && Controller.input.direction != Vector3.zero)
        {
            Controller.player.stateMachine.ChangeState(StateName.EquipMove);
            return true;
        }
        return false;
    }

    private bool CanIdle()
    {
        if (InputData.IsButtonOn(Controller.input.buttonsDown, InputData.INTERACTIONBUTTON))
        {
            Controller.player.interactableObject = null;
            equippableObject.transform.parent = equippableObject.defaultParent;
            Controller.player.stateMachine.ChangeState(StateName.Idle);
            return true;
        }
        return false;
    }
}
