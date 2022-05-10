using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public VehicleController VehiclePrefab;
    
    public VehicleController Vehicle { get; private set; }
    
    private void Awake()
    {
        var spawn = FindObjectOfType<Start>();
        spawn.GetSpawn(out var position, out var rotation);
        Vehicle = Instantiate(VehiclePrefab, position, rotation);
    }
}
