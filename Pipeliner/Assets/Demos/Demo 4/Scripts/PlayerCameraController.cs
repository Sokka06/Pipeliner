using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    
    public Player Player { get; private set; }

    private void OnValidate()
    {
        if (VirtualCamera == null)
            VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        SetTarget(Player.Vehicle.transform);
    }

    public void SetTarget(Transform target)
    {
        VirtualCamera.Follow = target;
        VirtualCamera.LookAt = target;
    }
}
