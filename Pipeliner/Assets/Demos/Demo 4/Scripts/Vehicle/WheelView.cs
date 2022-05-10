using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelView : MonoBehaviour
{
    public WheelCollider Collider;
    public Transform Root;
    
    private Vector3 _offset;
    
    private void OnValidate()
    {
        if (Root == null)
            Root = transform.GetChild(0);
    }

    private void Awake()
    {
        _offset = Root.localPosition - Collider.transform.localPosition;
    }

    private void Update()
    {
        Collider.GetWorldPose(out var position, out var rotation);
        Root.SetPositionAndRotation(position + transform.TransformVector(_offset), rotation);
    }
}
