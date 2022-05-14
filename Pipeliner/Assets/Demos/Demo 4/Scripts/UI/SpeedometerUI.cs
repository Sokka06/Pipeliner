using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedometerUI : MonoBehaviour
{
    public Slider Slider;

    private VehicleEngine _engine;
    
    public Player Player { get; private set; }

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        _engine = Player.Vehicle.GetComponentInChildren<VehicleEngine>();
    }

    private void LateUpdate()
    {
        Slider.value = Mathf.InverseLerp(_engine.MinRPM, _engine.MaxRPM, _engine.CurrentRPM) * Slider.maxValue;
    }
}
