using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public int cameraIndex = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("CameraManager").GetComponent<CameraManager>().SwitchCamera(cameraIndex);
            if(cameraIndex == 3)
            {
                GameObject.Find("LightManager").GetComponent<LightManager>().LightBlend();
            }
        }
    }
}
