using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableObject : MonoBehaviour, IInteractableObject
{
    public Vector3 equipLocalPosition;
    [HideInInspector] public Vector3 defaultPosition;
    [HideInInspector] public Transform defaultParent;

    private StateName interactState = StateName.EquipIdle;

    private void Awake()
    {
        GameManager.Instance.InitAction += InitObject;
    }

    private void Start()
    {
        defaultPosition = transform.position;
        defaultParent = transform.parent;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController.player.interactableObject == null)
            {
                playerController.player.interactableObject = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController.player.interactableObject == (IInteractableObject)this
                && (playerController.player.stateMachine.CurrentState.GetType() != typeof(EquipIdleState) || playerController.player.stateMachine.CurrentState.GetType() != typeof(EquipMoveState)))
            {
                playerController.player.interactableObject = null;
            }
        }
    }

    public StateName GetInteractState()
    {
        return interactState;
    }

    public void InitObject()
    {
        transform.parent.parent = defaultParent;
        transform.parent.position = defaultPosition;
    }
}
