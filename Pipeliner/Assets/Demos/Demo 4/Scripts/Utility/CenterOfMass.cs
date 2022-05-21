using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class CenterOfMass : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Vector3 CoM;
    
        private void OnValidate()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Rigidbody.centerOfMass = CoM;
        }
    }
}