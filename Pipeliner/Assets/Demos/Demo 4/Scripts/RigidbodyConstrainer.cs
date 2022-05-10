using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RigidbodyConstrainer : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public float Stiffness;
    public float Damping;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private Quaternion asd;

    private void OnValidate()
    {
        if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _initialPosition = Rigidbody.position;
        _initialRotation = Rigidbody.rotation;

        asd = transform.localRotation;
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
        return;
        
        Quaternion desiredRotation = newRot;
        
        var kp = (6f*Stiffness)*(6f*Stiffness)* 0.25f;
        var kd = 4.5f*Stiffness*Damping;
        float dt = Time.deltaTime;
        float g = 1 / (1 + kd * dt + kp * dt * dt);
        float ksg = kp * g;
        float kdg = (kd + kp * dt) * g;
        Vector3 x;
        float xMag;
        Quaternion q = desiredRotation * Quaternion.Inverse(currentRotation);
// Q can be the-long-rotation-around-the-sphere eg. 350 degrees
// We want the equivalant short rotation eg. -10 degrees
// Check if rotation is greater than 190 degees == q.w is negative
        if (q.w < 0)
        {
            // Convert the quaterion to eqivalent "short way around" quaterion
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        q.ToAngleAxis (out xMag, out x);
        x.Normalize ();
        x *= Mathf.Deg2Rad;
        Vector3 pidv = kp * x * xMag - kd * Rigidbody.angularVelocity;
        Quaternion rotInertia2World = Rigidbody.inertiaTensorRotation * currentRotation;
        pidv = Quaternion.Inverse(rotInertia2World) * pidv;
        pidv.Scale(Rigidbody.inertiaTensor);
        pidv = rotInertia2World * pidv;
        Rigidbody.AddTorque (pidv);
    }
}