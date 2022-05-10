using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleView : MonoBehaviour
{
    public VehicleController Controller;
    public Transform Root;

    private float _currentAngle;

    private void Update()
    {
        var forward = Vector2.Perpendicular(-Controller.GroundData.Normal);
        var velocity = Controller.Rigidbody.velocity.normalized;
        
        var targetAngle = 0f;
        if (Controller.GroundData.HasGround)
        {
            targetAngle = Mathf.Atan2(forward.y, forward.x);
        }
        else
        {
            targetAngle = Mathf.Atan2(velocity.y, velocity.x);
            targetAngle = Mathf.Clamp(targetAngle, -60 * Mathf.Deg2Rad, 60 * Mathf.Deg2Rad);
        }

        _currentAngle = Mathf.Lerp(_currentAngle, targetAngle, 25f * Time.deltaTime);
        
        //Root.rotation = Quaternion.identity * Quaternion.AngleAxis(Mathf.Round(_currentAngle * Mathf.Rad2Deg), Vector3.forward);
    }
}
