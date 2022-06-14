using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController
{
    private CinemachineVirtualCamera _camera;
    
    public CameraController(CinemachineVirtualCamera camera)
    {
        _camera = camera;
    }

    public void ChangeCameraFollower(Transform target)
    {
        _camera.Follow = target;
        _camera.LookAt = target;
    }
}
