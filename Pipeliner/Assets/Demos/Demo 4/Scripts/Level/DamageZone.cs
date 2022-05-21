using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using UnityEngine;

namespace Demos.Demo4
{
    public class DamageZone : MonoBehaviour
    {
        public Collider Trigger;
        public int Amount;
    
        private void OnValidate()
        {
            if (Trigger == null)
                Trigger = GetComponent<Collider>();
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.attachedRigidbody.TryGetComponent<Health>(out var health))
                return;
        
            health.SetHealth(health.CurrentHealth - Amount);
        }
    
        private void OnDrawGizmos()
        {
            var color = Color.red;
            color.a *= 0.5f;
            Gizmos.color = color;

            Gizmos.matrix = Trigger.transform.localToWorldMatrix;

            var trigger = Trigger as BoxCollider;

            Gizmos.DrawCube(trigger.center, trigger.size);
        }
    }
}