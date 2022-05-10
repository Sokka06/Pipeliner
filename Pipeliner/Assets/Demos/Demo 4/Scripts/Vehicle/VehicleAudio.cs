using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAudio : MonoBehaviour
{
    public VehicleController Vehicle;

    [Space] 
    public AudioSource EngineSource;

    private VehicleEngine _engine;

    private void Awake()
    {
        _engine = Vehicle.GetComponentInChildren<VehicleEngine>();
    }

    private void LateUpdate()
    {
        var engineNormalized = Mathf.InverseLerp(_engine.MinRPM, _engine.MaxRPM, _engine.CurrentRPM);
        EngineSource.volume = Mathf.Lerp(0.25f, 1f, engineNormalized);
        EngineSource.pitch = Mathf.Lerp(0.5f, 1.5f, engineNormalized);
    }
}
