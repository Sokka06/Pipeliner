using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    
    public Transform Vehicle { get; private set; }

    private void OnValidate()
    {
        if (VirtualCamera == null)
            VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Vehicle = FindObjectOfType<VehicleController>().transform;
        VirtualCamera.Follow = Vehicle;
        VirtualCamera.LookAt = Vehicle;
    }
}
