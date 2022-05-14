using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Collider Trigger;
    public Action<Vehicle> onFinish;

    private void OnValidate()
    {
        if (Trigger == null)
            Trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody.TryGetComponent<Vehicle>(out var vehicle))
            return;
        
        onFinish?.Invoke(vehicle);
    }
    
    private void OnDrawGizmos()
    {
        var color = Color.green;
        color.a *= 0.5f;
        Gizmos.color = color;

        Gizmos.matrix = Trigger.transform.localToWorldMatrix;

        var trigger = Trigger as BoxCollider;

        Gizmos.DrawCube(trigger.center, trigger.size);
    }
}
