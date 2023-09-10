using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabMoveState : BaseState
{
    private MovableObject movableObject;

    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public GrabMoveState(PlayerController controller) : base(controller)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        movableObject = (MovableObject)Controller.player.interactableObject;
    }

    public override void OnUpdateState()
    {
        if(CanGrabIdle()
            || CanIdle())
        {
            return;
        }
    }

    public override void OnFixedUpdateState()
    {
        Controller.MoveFixedUpdate();
        Controller.moveVelocity *= movableObject.slowRate;

        Vector3 distance = movableObject.transform.position - movableObject.defaultPosition;
        if (distance.x < movableObject.minMovableRange.x)
        {
            if(Controller.moveVelocity.x < 0f)
            {
                Controller.moveVelocity.x = 0f;
            }
        }

        if (distance.x > movableObject.maxMovableRange.x)
        {
            if(Controller.moveVelocity.x > 0f)
            {
                Controller.moveVelocity.x = 0f;
            }
        }

        if (distance.y < movableObject.minMovableRange.y)
        {
            if (Controller.moveVelocity.y < 0f)
            {
                Controller.moveVelocity.y = 0f;
            }
        }

        if (distance.y > movableObject.maxMovableRange.y)
        {
            if (Controller.moveVelocity.y > 0f)
            {
                Controller.moveVelocity.y = 0f;
            }
        }

        if (distance.z < movableObject.minMovableRange.z)
        {
            if (Controller.moveVelocity.z < 0f)
            {
                Controller.moveVelocity.z = 0f;
            }
        }

        if (distance.z > movableObject.maxMovableRange.z)
        {
            if (Controller.moveVelocity.z > 0f)
            {
                Controller.moveVelocity.z = 0f;
            }
        }

        if(Controller.inputDirection.magnitude > 0.1f)
        {
            
            Vector3 localDirection = Controller.animator.transform.forward + Controller.animator.transform.right;
            Vector3 direction = Quaternion.Euler(Controller.animator.transform.localRotation.eulerAngles) * Controller.moveDirection;
            float rotationY = Controller.animator.transform.localRotation.eulerAngles.y;
            int reverseValue = 1;
            if ((45.0f < rotationY && rotationY <= 135.0f) 
                || (225.0f <= rotationY && rotationY < 315.0f))
            {
                reverseValue = -1;
            }

            Controller.animator.SetFloat("GrabHorizontal", direction.x * reverseValue);
            Controller.animator.SetFloat("GrabVertical", direction.z * reverseValue);
        }
    }

    public override void OnExitState()
    {
        
    }

    private bool CanGrabIdle()
    {
        if (Controller.isGrounded
            && Controller.input.direction == Vector3.zero)
        {
            Controller.player.stateMachine.ChangeState(StateName.GrabIdle);
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
            movableObject.transform.parent.parent = movableObject.defaultParent;
            Controller.player.stateMachine.ChangeState(StateName.Idle);
            return true;
        }
        return false;
    }
}
