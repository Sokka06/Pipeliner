using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class Player : MonoBehaviour
    {
        public Vehicle VehiclePrefab;
    
        public Vehicle Vehicle { get; private set; }
    
        private void Awake()
        {
            var spawn = FindObjectOfType<Start>();
            spawn.GetSpawn(out var position, out var rotation);
            Vehicle = Instantiate(VehiclePrefab, position, rotation);
        }
    }
}