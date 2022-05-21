using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demos.Demo4
{
    public class RigidbodyConstrainer : MonoBehaviour
    {
        public Rigidbody Rigidbody;

        private Quaternion _initialRotation;

        private Quaternion asd;

        private void OnValidate()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _initialRotation = Rigidbody.rotation;
        }

        private void FixedUpdate()
        {
            var currentRotation = Rigidbody.rotation;
            var newForward = Vector3.Cross(currentRotation * Vector3.up, _initialRotation * -Vector3.right);
            var newUp = Vector3.Cross(newForward, _initialRotation * Vector3.right);
            var newRot = Quaternion.LookRotation(newForward, newUp);
            Rigidbody.MoveRotation(newRot);

            var angularVelocity = Rigidbody.transform.InverseTransformVector(Rigidbody.angularVelocity);
            angularVelocity.y = 0f;
            angularVelocity.z = 0f;
            Rigidbody.angularVelocity = Rigidbody.transform.TransformVector(angularVelocity);
        }
    }
}