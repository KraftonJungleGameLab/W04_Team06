using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLB;

public class InteractableSwitch : MonoBehaviour
{
    private PlayerController controller;
    [SerializeField] private GameObject lightObject;

    private void Start()
    {
        controller = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (InputData.IsButtonOn(controller.input.buttons, InputData.INTERACTIONBUTTON))
            {
                lightObject.GetComponent<VolumetricLightBeamHD>().enabled = false;
                lightObject.GetComponent<VolumetricShadowHD>().enabled = false;
                lightObject.GetComponent<LightDetect>().enabled = false;
            }
        }
    }
}
