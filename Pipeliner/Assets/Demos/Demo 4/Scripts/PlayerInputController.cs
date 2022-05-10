using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Keyboard Keyboard { get; private set; }
    
    public VehicleController Vehicle { get; private set; }

    private void Start()
    {
        Vehicle = FindObjectOfType<VehicleController>();
    }

    private void OnEnable()
    {
        InputSystem.onAfterUpdate += OnAfterUpdate;
    }

    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= OnAfterUpdate;
    }

    private void OnAfterUpdate()
    {
        Keyboard = Keyboard.current;

        var throttle = 0f;
        var brake = 0f;
        var lean = 0f;
            
        if (Keyboard != null)
        {
            //Throttle input
            if (Keyboard.wKey.isPressed || Keyboard.upArrowKey.isPressed)
                throttle += 1f;
            //Brake input
            if (Keyboard.sKey.isPressed || Keyboard.downArrowKey.isPressed)
                brake += 1f;
            //Lean input
            if (Keyboard.dKey.isPressed || Keyboard.rightArrowKey.isPressed)
                lean += 1f;
            if (Keyboard.aKey.isPressed || Keyboard.leftArrowKey.isPressed)
                lean -= 1f;
        }

        var vehicleInputs = new VehicleInputs
        {
            Throttle = throttle,
            Brake = brake,
            Lean = lean
        };
        
        Vehicle.SetInputs(ref vehicleInputs);
    }
}
