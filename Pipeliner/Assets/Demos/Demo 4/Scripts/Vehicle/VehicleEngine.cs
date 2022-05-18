using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEngine : MonoBehaviour
{
    public Vehicle Vehicle;

    [Space]
    public float MinRPM = 1000f;
    public float MaxRPM = 8000f;
    [Tooltip("Rev limiter cut off time when max RPM is exceeded.")]
    public float CutOffTime = 0.1f;
    
    [Space]
    [Tooltip("How fast the engine accelerates.")]
    public float Acceleration = 5000f;
    [Tooltip("How fast the engine slows down when no throttle is applied.")]
    public float Friction = 0.5f;

    private float _limiterTimer;
    
    public float CurrentRPM { get; private set; }

    private float _angularVelocity;

    private const float RADS_TO_RPM = 60.0f / (2.0f * Mathf.PI);
    private const float RPM_TO_RADS = 2.0f * Mathf.PI / 60.0f;

    private void Start()
    {
        CurrentRPM = MinRPM;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        
        if (_limiterTimer > 0f)
        {
            _limiterTimer -= deltaTime;
        }
        else
        {
            if (Vehicle.Controller.GroundData.HasGround)
            {
                var targetAngularVelocity = Mathf.Lerp(MinRPM, MaxRPM, Vehicle.Controller.Rigidbody.velocity.magnitude / Vehicle.Controller.Drive.Speed) * RPM_TO_RADS;
                _angularVelocity = targetAngularVelocity;
            }
            else
            {
                var angularAcceleration = Vehicle.Controller.Input.Inputs.Throttle * Acceleration * deltaTime;
                _angularVelocity += angularAcceleration;
            }
        }
        
        // apply rotational friction if no angular velocity is added.
        _angularVelocity *= 1f / (1f + Friction * deltaTime);

        CurrentRPM = Mathf.Max(_angularVelocity * RADS_TO_RPM, MinRPM);
        
        if (CurrentRPM > MaxRPM)
            _limiterTimer = CutOffTime;
    }
}
