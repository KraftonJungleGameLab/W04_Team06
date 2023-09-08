using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> vCam = new List<CinemachineVirtualCamera>();

    void Start()
    {
        AddCamera();
    }

    void AddCamera()
    {
        CinemachineVirtualCamera[] childCameras = GetComponentsInChildren<CinemachineVirtualCamera>();

        foreach (CinemachineVirtualCamera childCamera in childCameras)
        {
            vCam.Add(childCamera);
        }
    }

    public void SwitchCamera(int i)
    {
        if (i >= 0 && i < vCam.Count)
        {
            // 모든 카메라의 Priority를 0으로 설정
            foreach (CinemachineVirtualCamera camera in vCam)
            {
                camera.Priority = 0;
            }
            // 선택한 카메라의 Priority를 1로 설정
            vCam[i].Priority = 1;
        }
    }
}
