using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour, IInteractableObject
{
    public Vector3 minMovableRange;
    public Vector3 maxMovableRange;
    public bool isHeavy = true;

    [HideInInspector] public Vector3 defaultPosition;
    [HideInInspector] public Transform defaultParent;

    private StateName interactState = StateName.GrabIdle;

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
            if ((MovableObject)playerController.player.interactableObject == this)
            {
                playerController.player.interactableObject = null;
            }
        }
    }

    public StateName GetInteractState()
    {
        return interactState;
    }
}
