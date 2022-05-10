using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int Amount;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody.TryGetComponent<Health>(out var health))
            return;
        
        health.SetHealth(health.CurrentHealth - Amount);
    }
}
