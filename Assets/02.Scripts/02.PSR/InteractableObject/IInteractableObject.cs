using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableObject
{
    public StateName GetInteractState();
    public void InitObject();
}
