using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public int ScenesIndex = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("EndSwitcher").GetComponent<EndSwitcher>().NextScenses(ScenesIndex);
        }
    }
}
